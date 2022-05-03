using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeOther : MonoBehaviour
{

	public WallBar wallbar;

	public WallBar wallbarFollower;

	[SerializeField]
	public List<Point> points = new List<Point>();
	[SerializeField]
	public List<Stick> sticks = new List<Stick>();

	[SerializeField]
	public bool[] lockedPoints = new bool[10];
	[SerializeField]
	public GameObject[] pinnedList = new GameObject[10];
	[SerializeField]
	public GameObject[] pinnedBeforeFallList = new GameObject[100];


	public GameObject ledgePrompt;
	public GameObject p2ledgePrompt;

	[SerializeField]
	public GameObject P1DragInBelt1;
	[SerializeField]
	public GameObject P1DragInBelt2;
	[SerializeField]
	public GameObject P2DragInBelt1;
	[SerializeField]
	public GameObject P2DragInBelt2;



	[HideInInspector]
	public GameObject TheBelt1 = null;

	[HideInInspector]
	public GameObject TheBelt2 = null;


	[HideInInspector]
	public GameObject P2TheBelt1 = null;

	[HideInInspector]
	public GameObject P2TheBelt2 = null;


	[SerializeField]
	public GameObject InitialCoil1;

	[SerializeField]
	public GameObject InitialCoil2;

	//coiling, says which points to coil to belt one and two
	public int whichToCoil1;
	public int whichToCoil2;


	protected List<GameObject> pointObjects = new List<GameObject>();
	public GameObject endPosition;
	public GameObject startPosition;

	public GameObject theLeader;
	public GameObject theFollower;
	

	public int Frequency = 10;



	public GameObject pointObject;
	//public GameObject pinnedTo;
	public LineRenderer lr;

	public bool simulating = false;
	public bool Coil;

	Color c1 = Color.white;
	Color c2 = new Color(1, 1, 1, 0);

	public int solveIterations = 5;



	public float gravity = 10f;
	int[] order;
	public bool constrainStickMinLength = true;

	public bool HasOrigin = true;



	public GameObject UnravelStartObject;
	public Vector3 UnravelStartPosition;
	public Vector3 RecoilStartPosition;
	public int UnravelIndex; 
	public int RecoilIndex;
	//public int RecoilAnchorIndex = 0;
	public float UnravelDistanceLet;
	public float distanceFromAnchor;
	public float followerDistanceFromAnchor;

	//what the total length of the rope should be
	public float TotalRopeLength;
	//what the total distance of rope unraveled should be. This should be >= the total rope length
	public float TotalDistanceUnraveled;

	public bool hasSetBeforeFallList = false;
	public bool hasReattached = false;
	public bool pickedUp = false;
	public bool UnraveledAtClimbStart = false;
	public bool setRecoilStart = false;


	public bool recoilBelt;

	void Start()
	{
		// added to auto do on start
		InstantiateSections(Frequency);
		TotalRopeLength = Mathf.Abs(GetDistanceFloat(endPosition.transform.position, startPosition.transform.position));

		if (Coil)
		{
			CoilRope(0, whichToCoil1, whichToCoil2, InitialCoil1, InitialCoil2);
			Coil = false;
		}
		simulating = true;
	}

	void Update()
	{
		if (simulating)
		{
			Simulate();
		}

		DrawSticks();
		if (pickedUp)
		{
			HandleClimbing();
			HandleFollowing();
		}
	}

	void HandleFollowing()
    {
         if (wallbarFollower.FollowRope && wallbar.theAnchor != null)
         {
            if (!setRecoilStart)
            {
				RecoilStartPosition = theFollower.transform.position;
				setRecoilStart = true; 
            }
			Debug.Log("wallBarFollower" + wallbarFollower.FollowRope);

			if (wallbarFollower.hitAnchorFollower)
			{
				RecoilOnFollow();
				wallbarFollower.hitAnchorFollower = false;
			}
         }
    }


	void RecoilOnFollow()
	{
		if(wallbarFollower.theFollowerAnchor != null)
        {
			for(int i = pinnedList.Length-1; i > 0 ; i--)
            {
				if (pinnedList[i] != null && wallbarFollower.theFollowerAnchor == pinnedList[i])
                {
					Debug.Log("pinned list at position " + i + "is " + pinnedList[i] + "wallbarFollower i is " + wallbarFollower.theFollowerAnchor);
					for(int ii = i; ii < pinnedList.Length; ii+=whichToCoil1)
                    {
						if (recoilBelt)
						{
							pinnedList[ii] = P2TheBelt1; // need a reference to each players belt automatically! COULD JUST USE IF STATEMENT
							recoilBelt = false;
						}

						else if (!recoilBelt)
						{
							pinnedList[ii] = P2TheBelt2;
							recoilBelt = true;
						}
					}
                }
            }
        }	
	}


	void UnpinOnFall()
    {
		Debug.Log("FALLING " + wallbar.isFalling);
		wallbar.P1currentStamina = 1f;
		int c = 0;
		for(int i = 0; i<pinnedList.Length; i++)
        {
            if (pinnedList[i] != null && !pinnedList[i].name.Contains("AnchorPoint") && pinnedList[i] != P2TheBelt1 && pinnedList[i] != P2TheBelt1)
            {
				pinnedBeforeFallList[i] = pinnedList[i];
				pinnedList[i] = null;
				Debug.Log("Pinned before fall list at position " + i + "is" + pinnedBeforeFallList[i]);
				points[i].pinnedTo = pinnedList[i];
            }

			else if (pinnedList[i] != null && pinnedList[i].name.Contains("AnchorPoint"))
            {
				pinnedBeforeFallList[i] = pinnedList[i];
			}
        }
		Debug.Log("Pinned before fall list 2 at position" + c + "is " + pinnedBeforeFallList[0]);
		hasSetBeforeFallList = true;
		hasReattached = false;
		
	}

	void HandleClimbing()
    {

		if (wallbar.theAnchor != null)
		{
			distanceFromAnchor = GetDistanceFloat(theLeader.transform.position, wallbar.theAnchor.transform.position);
		}
		if (wallbar.ClimbRope)
		{
			if (!UnraveledAtClimbStart)
			{
				UnravelAction(distanceFromAnchor);
				UnraveledAtClimbStart = true;
			}
			Debug.Log("Unravel Index" + points[UnravelIndex].position);
			if (UnravelIndex > 0)
			{
				Unravel();
			}
			if (wallbar.toAnchor)
			{
				ConnectToAnchor();
			}
		}
		else if (wallbar.isFalling && !hasSetBeforeFallList)
		{
			UnpinOnFall();
		}

		else if (wallbar.isFalling)
		{
			theLeader.transform.position = points[UnravelIndex].position;
		}

		if (wallbar.fallCoil && !hasReattached)
		{
			RepinOnReattach();
		}
		
		if(wallbar.onLedge && wallbarFollower.onLedge)
        {
			Debug.Log("both are on ledge");
			//ledgePrompt.SetActive(true);
			//p2ledgePrompt.SetActive(true);

           // if (Input.GetKeyDown("e"))
           // {
				//SwitchClimberAndFollower();
            //}
		}

	}

	public void SwitchClimberAndFollower()
    {
		WallBar LeaderSwitch = wallbar;
		WallBar FollowerSwitch = wallbarFollower;
		wallbarFollower = LeaderSwitch;
		wallbar = FollowerSwitch;

		wallbarFollower.isFollower = false;
		wallbarFollower.FollowRope = false;
		wallbarFollower.ClimbRope = true;
		wallbarFollower.isLeader = true;

		wallbar.isLeader = false;
		wallbar.ClimbRope = false;
		wallbar.isFollower = true;
		wallbar.FollowRope = true;
		Debug.Log("Changes guys, wallbar isFollower " + wallbar.FollowRope + "wallbar follower is leader " + wallbarFollower.ClimbRope);
	}

	void RepinOnReattach()
    {
		Debug.Log("runs fallcoil");
		
		for(int i = 0; i<pinnedList.Length; i++)
        {
			if(pinnedBeforeFallList[i]!= null)
            {
				pinnedList[i] = pinnedBeforeFallList[i];
            }
			points[i].pinnedTo = pinnedList[i];
			pinnedBeforeFallList[i] = null;
        }
		hasReattached = true;
		hasSetBeforeFallList = false;
		wallbar.fallCoil = false;
	}

	void ConnectToAnchor()
    {
		if (!pinnedList[UnravelIndex].name.Contains("AnchorPoint"))
		{
			ConnectToAnchorAction();
			Debug.Log("UNRAVELS connect to anchor not anchorpoint");
		}

        if(pinnedList[UnravelIndex].name.Contains("AnchorPoint"))
        {
			Debug.Log("UNRAVELS connect to anchor yes anchorpoint");
			UnravelIndex -= whichToCoil1;
			UnravelAction(distanceFromAnchor);
			//RecoilAnchorIndex += 1;
			//ConnectToAnchorAction();
		}
	}

	void ConnectToAnchorAction()
    {
		Debug.Log("UNRAVELS Connect to anchor action thinks index is: " + UnravelIndex);
		pinnedList[UnravelIndex] = wallbar.theAnchor;
		points[UnravelIndex].pinnedTo = pinnedList[UnravelIndex];
		wallbar.toAnchor = false;
		UnravelStartPosition = wallbar.theAnchor.transform.position;
		TotalDistanceUnraveled = 0f;
	}

	void Unravel()
	{
		/*
		float DistanceFromOrigin = GetDistanceFloat(theLeader.transform.position, UnravelStartPosition);

		if (Mathf.Abs(DistanceFromOrigin) >= UnravelDistanceLet)
		{
			if (pinnedList[UnravelIndex] != null && pinnedList[UnravelIndex].name.Contains("AnchorPointStart"))
			{
				UnravelIndex -= whichToCoil1;
				Debug.Log("UNRAVELS -=");
			}
			else
			{
				pinnedList[UnravelIndex] = null;
				points[UnravelIndex].pinnedTo = pinnedList[UnravelIndex];
				UnravelIndex -= whichToCoil1;
				UnravelStartPosition = theLeader.transform.position;
				Debug.Log("UNRAVELS!");
				TotalDistanceUnraveled += DistanceFromOrigin;
			}
		}
		*/
		if (wallbar.theAnchor != null)
		{
			if(distanceFromAnchor >= UnravelDistanceLet && distanceFromAnchor>=TotalDistanceUnraveled)
            {
				if (pinnedList[UnravelIndex] != null && pinnedList[UnravelIndex].name.Contains("AnchorPoint"))
				{
					UnravelIndex -= whichToCoil1;
					Debug.Log("UNRAVELS -=");
				}
				else
				{
					UnravelAction(distanceFromAnchor);
				}
			}	
		}


	}


	void UnravelAction(float distance)
    {
		

		pinnedList[UnravelIndex] = null;
		points[UnravelIndex].pinnedTo = pinnedList[UnravelIndex];
		UnravelIndex -= whichToCoil1;
		UnravelStartPosition = theLeader.transform.position;
		Debug.Log("UNRAVELS! distance from anchor " + distance);
		Debug.Log("UNRAVELS! distance total unraveled " + TotalDistanceUnraveled);
		TotalDistanceUnraveled += UnravelDistanceLet;
		Debug.Log("UNRAVELS Index is  " + UnravelIndex);

	}

	void FixedUpdate()
	{
		for (int i = 1; i < points.Count; i++)
		{
			IsPinnedOrLocked(i, points[i]);
		}
	}

	float GetDistanceFloat(Vector3 end, Vector3 start)
	{
		float xLength = end.x - start.x;
		float yLength = end.y - start.y;
		float zLength = end.z - start.z;
		float theDistance = (Mathf.Sqrt((xLength * xLength) + (yLength * yLength) + (zLength * zLength)));
		return theDistance;
	}

	private void CoilRope(int startingPoint, int whichToCoil1, int lockedFrequency, GameObject Belt1, GameObject Belt2)
	{

		bool belt = true;
		for (int i = startingPoint; i < Frequency; i += whichToCoil1)
		{
			if (belt)
			{
				pinnedList[i] = Belt1;
				belt = false;
			}

			else if (!belt)
			{
				pinnedList[i] = Belt2;
				belt = true;
			}
		}
	}

	private Vector3 GetDistance(int frequency)
	{


		float segmentLengthX = (endPosition.transform.position.x - startPosition.transform.position.x) / frequency;
		float segmentLengthY = (endPosition.transform.position.y - startPosition.transform.position.y) / frequency;
		float segmentLengthZ = (endPosition.transform.position.z - startPosition.transform.position.z) / frequency;

		Vector3 segmentLength = new Vector3(segmentLengthX, segmentLengthY, segmentLengthZ);
		return segmentLength;
	}


	Vector3 GetPlayerPosition(GameObject player)
	{
		Vector3 thePosition = player.transform.position;
		return thePosition;
	}

	private void InstantiateSections(int frequency)
	{

		Point OldPoint = new Point() { position = startPosition.transform.position, prevPosition = startPosition.transform.position, locked = false, pinnedTo = null };

		IsPinnedOrLocked(0, OldPoint);


		Vector3 GetDistanceBetweenPoints = GetDistance(frequency);
		Vector3 toEnd = (endPosition.transform.position - startPosition.transform.position);
		Quaternion toEndQuad = Quaternion.Euler(toEnd);

		GameObject OldPointObject;
		GameObject NewPointObject;

		OldPointObject = Instantiate(pointObject, new Vector3(0f, 0f, 0f), toEndQuad);
		points.Add(OldPoint);

		pointObjects.Add(OldPointObject);

		for (int i = 1; i < frequency; i++)
		{
			Point NewPoint = new Point() { position = OldPoint.position + GetDistanceBetweenPoints, prevPosition = OldPoint.position + GetDistanceBetweenPoints };


			points.Add(NewPoint);
			sticks.Add(new Stick(OldPoint, NewPoint));

			NewPointObject = Instantiate(pointObject, new Vector3(0f, 0f, 0f), toEndQuad);
			pointObjects.Add(NewPointObject);
			OldPoint = NewPoint;

		}

	}

	private void PickedUp()  
	{
		CoilRope(0, whichToCoil1, whichToCoil2, TheBelt1, TheBelt2);

	}


	private void IsPinnedOrLocked(int i, Point point)
	{
		if (lockedPoints[i] != null)
		{
			point.locked = lockedPoints[i];

		}

		if (pinnedList[i] != null)
		{
			point.pinnedTo = pinnedList[i];
		}
	}


	[System.Serializable]
	public class Point
	{
		public Vector3 position, prevPosition;
		[SerializeField]
		public bool locked;

		public GameObject pinnedTo;

	}

	[System.Serializable]
	public class Stick
	{
		public Point pointA, pointB;
		public float length;
		public bool dead;

		public Stick(Point pointA, Point pointB)
		{
			this.pointA = pointA;
			this.pointB = pointB;
			length = Vector3.Distance(pointA.position, pointB.position);
		}
	}

	void Simulate()
	{
		int c = 0;
		foreach (Point p in points)
		{
			if (!p.locked)
			{
				Vector3 positionBeforeUpdate = p.position;
				p.position += p.position - p.prevPosition;
				p.position += Vector3.down * gravity * Time.deltaTime * Time.deltaTime;
				p.prevPosition = positionBeforeUpdate;
			}

			if (p.pinnedTo)
			{
				p.position = p.pinnedTo.transform.position;
				p.prevPosition = p.pinnedTo.transform.position;
			}
			c++;
		}

		for (int i = 0; i < solveIterations; i++)
		{
			for (int s = 0; s < sticks.Count; s++)
			{
				Stick stick = sticks[s];

				Vector3 stickCentre = (stick.pointA.position + stick.pointB.position) / 2;
				Vector3 stickDir = (stick.pointA.position - stick.pointB.position).normalized;
				float length = (stick.pointA.position - stick.pointB.position).magnitude;

				if (length > stick.length || constrainStickMinLength)
				{
					if (!stick.pointA.locked)
					{
						stick.pointA.position = stickCentre + stickDir * stick.length / 2;
					}
					if (!stick.pointB.locked)
					{
						stick.pointB.position = stickCentre - stickDir * stick.length / 2;
					}
				}

			}
		}

	}


	void DrawSticks()
	{
		Point New = new Point();
		GameObject thePoint;
		for (int i = 0; i < points.Count; i++)
		{
			Point old = points[i];
			thePoint = pointObjects[i];

			if (i < points.Count - 1)
			{
				New = points[i + 1];
			}
			lr = thePoint.GetComponent<LineRenderer>();
			lr.SetPosition(0, old.position);
			lr.SetPosition(1, New.position);
		}
	}


	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag.Contains("Player"))
		{
			if (Input.GetKey("e"))
			{
				PickedUp();
				theLeader = other.gameObject;
				wallbar = theLeader.GetComponent<WallBar>();
				pickedUp = true;
				wallbar.isLeader = true;
				GameObject[] FindFollower = GameObject.FindGameObjectsWithTag("Player");
				foreach(GameObject o in FindFollower)
                {
					if(o.name != theLeader.name)
                    {
						theFollower = o;
						wallbarFollower = theFollower.GetComponent<WallBar>();
						wallbarFollower.isFollower = true;
                    }
                }

				if(theLeader.name == "Player1 Animated")
                {
					TheBelt1 = P1DragInBelt1;
					TheBelt2 = P1DragInBelt2;
					P2TheBelt1 = P2DragInBelt1;
					P2TheBelt2 = P2DragInBelt2;
                }
                else
                {
					TheBelt1 = P2DragInBelt1;
					TheBelt2 = P2DragInBelt2;
					P2TheBelt1 = P1DragInBelt1;
					P2TheBelt2 = P1DragInBelt2;
				}
				Debug.Log("climber leader is " + theLeader.name);
				Debug.Log("climber follower is " + theFollower.name);					
			}		
		}
	}
}
