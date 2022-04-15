using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeOther : MonoBehaviour
{

	public WallBar wallbar;


	[SerializeField]
	public List<Point> points = new List<Point>();
	[SerializeField]
	public List<Stick> sticks = new List<Stick>();

	[SerializeField]
	public bool[] lockedPoints = new bool[10];
	[SerializeField]
	public GameObject[] pinnedList = new GameObject[10];

	[SerializeField]
	public GameObject Belt1;

	//[SerializeField]
	//public GameObject Belt2;

	//coiling, says which points to coil to belt one and two
	public int whichToCoil1;
	//public int whichToCoil2;


	protected List<GameObject> pointObjects = new List<GameObject>();
	public GameObject endPosition;
	public GameObject startPosition;

	public GameObject thePlayer;

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



	[HideInInspector]
	public Vector3 UnravelStartPosition;

	
	public int UnravelIndex;

	public float UnravelDistanceLet;

	void Start()
    {
    }

	void Update()
    {
		
        if (Input.GetKeyDown("r"))
        {
			InstantiateSections(Frequency);
			//UnravelIndex = points.Count-10;

			if (Coil)
			{
				CoilRope(whichToCoil1/*, whichToCoil2*/);
				Coil = false;
			}
		}

        if (Input.GetKey("space"))
        {
			simulating = true;
			Debug.Log("simulating = " + simulating);
        }
        if (Input.GetKeyDown("y"))
		{
			simulating = false;
        }
		if (simulating)
		{
			Simulate();
		}
		DrawSticks();

        if (wallbar.ClimbRope)
        {


			if (!HasOrigin)
			{
				UnravelStartPosition = thePlayer.transform.position;
				Debug.Log("UnravelStartPosition" + UnravelStartPosition);
				HasOrigin = true;

				//for (int i = points.Count - 10; i >= 0; i -= 10)
				//{
				//	Debug.Log("INDEX " + i);
				//	Debug.Log("UNPINNING" + points[i].pinnedTo.name + "INDEX" + i);
				//	pinnedList[i] = null;
				//	points[i].pinnedTo = pinnedList[i];

				//}

			}
			if (UnravelIndex > 0)
			{
				Unravel(whichToCoil1);
			}

			
        }


    }



	void Unravel(int pinnedFrequency)
    {
		//float DistanceFromOrigin = thePlayer.transform.position - UnravelStartPosition;

		float DistanceFromOrigin = GetDistanceFloat(thePlayer.transform.position, UnravelStartPosition);
		Debug.Log("DistanceFromOrigin" + DistanceFromOrigin);

        if (Mathf.Abs(DistanceFromOrigin) >= UnravelDistanceLet)
        {
			Debug.Log("it thinks to unravel");
			pinnedList[UnravelIndex] = null;
			points[UnravelIndex].pinnedTo = pinnedList[UnravelIndex];
			Debug.Log("Unravel Index " + UnravelIndex);
			UnravelIndex -= pinnedFrequency;
			UnravelStartPosition = thePlayer.transform.position;
        }
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
		Debug.Log("StartX " + start.y + "EndX " + end.y);
		float xLength = end.x - start.x;
		Debug.Log("Xlength " + xLength);
		float yLength = end.y - start.y;
		Debug.Log("Ylength " + yLength);
		float zLength = end.z - start.z;
		Debug.Log("Zlength " + zLength);
		float theDistance = (Mathf.Sqrt((xLength * xLength) + (yLength * yLength) + (zLength * zLength)));
		Debug.Log("getDistanceFloat" + theDistance);
		return theDistance;
	}

	private void CoilRope(int pinnedFrequency/*, int lockedFrequency*/)
    {

		//bool belt = true;
		for(int i = 0; i<Frequency; i += pinnedFrequency)
        {
			//if (belt)
			//{
				pinnedList[i] = Belt1;
				//belt = false;
			//}	

			//else if (!belt)
           // {
			//	pinnedList[i] = Belt2;
			//	belt = true;
           // }
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

		Point OldPoint = new Point() { position = startPosition.transform.position, prevPosition = startPosition.transform.position, locked = false, pinnedTo = null};

		IsPinnedOrLocked(0, OldPoint);


		Vector3 GetDistanceBetweenPoints = GetDistance(frequency);
		Vector3 toEnd = (endPosition.transform.position - startPosition.transform.position);
		Quaternion toEndQuad = Quaternion.Euler(toEnd);

		GameObject OldPointObject;
		GameObject NewPointObject;

		OldPointObject = Instantiate(pointObject, new Vector3(0f, 0f, 0f) , toEndQuad);

		Debug.Log("OldPoint" + OldPointObject.transform.position);

		Debug.Log("runs 1 " + Frequency);

		points.Add(OldPoint);

		
		pointObjects.Add(OldPointObject);

		for (int i = 1; i<frequency; i++)
        {
			Debug.Log("runs");
			Point NewPoint = new Point() { position = OldPoint.position + GetDistanceBetweenPoints, prevPosition = OldPoint.position + GetDistanceBetweenPoints };


			//IsPinnedOrLocked(i, NewPoint);


			points.Add(NewPoint);
			sticks.Add(new Stick(OldPoint, NewPoint));

			NewPointObject = Instantiate(pointObject, new Vector3(0f, 0f, 0f), toEndQuad);
			pointObjects.Add(NewPointObject);
			OldPoint = NewPoint;
           		
		}

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
	public  class Point
	{
		public Vector3 position, prevPosition;
		[SerializeField]
		public bool locked;
		//public bool pinned;

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
            //else
            //{
			//	New.position = endPosition.transform.position;
           // }


			lr = thePoint.GetComponent<LineRenderer>();
			lr.SetPosition(0, old.position);
			lr.SetPosition(1, New.position);

			//if (i == pointObjects.Count - 1)
			//{
				//lr.SetPosition(0, old.position);
				//lr.SetPosition(0, endPosition.transform.position);
			//}
			//if (i == 0)
			//{
				//lr.SetPosition(0, startPosition.transform.position);
				//lr.SetPosition(0, points[0].position);
			//}


		}
	}



	



}




