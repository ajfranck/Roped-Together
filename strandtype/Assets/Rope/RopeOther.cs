using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeOther : MonoBehaviour
{

	protected List<Point> points = new List<Point>();
	protected List<Stick> sticks = new List<Stick>();
	protected List<GameObject> pointObjects = new List<GameObject>();
	public GameObject endPosition;
	public GameObject startPosition;

	public int Frequency = 10;
	public GameObject pointObject;
	public LineRenderer lr;
	public bool simulating = false;

	public int solveIterations = 5;

	public float gravity = 10f;
	int[] order;
	public bool constrainStickMinLength = true;


	void Start()
    {
		
    }

	void Update()
    {
        if (Input.GetKeyDown("r"))
        {
			InstantiateSections();
        }

		int i = 0;
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
		foreach(GameObject point in pointObjects)
        {
			if (i <10 )
			{
				point.transform.position = points[i].position;
				Debug.Log("points: " + points[i]);
				Debug.Log(" i = " + i);
			}
			i++;
        }
    }

	private Vector3 GetDistance()
	{

		float frequency = 10f;

	
		float segmentLengthX = (endPosition.transform.position.x - startPosition.transform.position.x) / frequency;
		float segmentLengthY = (endPosition.transform.position.y - startPosition.transform.position.y) / frequency;
		float segmentLengthZ = (endPosition.transform.position.z - startPosition.transform.position.z) / frequency;

		Vector3 segmentLength = new Vector3(segmentLengthX, segmentLengthY, segmentLengthZ);
		return segmentLength;
	}



	private void InstantiateSections()
    {

		int frequency = 10;

		Point OldPoint = new Point() { position = startPosition.transform.position, prevPosition = startPosition.transform.position, locked = true };
		Debug.Log(OldPoint.position);
		Vector3 GetDistanceBetweenPoints = GetDistance();
		Vector3 toEnd = (endPosition.transform.position - startPosition.transform.position);
		Quaternion toEndQuad = Quaternion.Euler(toEnd);

		GameObject OldPointObject;
		GameObject NewPointObject;

		OldPointObject = Instantiate(pointObject, OldPoint.prevPosition, toEndQuad);

		Debug.Log("OldPoint" + OldPointObject.transform.position);

		Debug.Log("runs 1 " + Frequency);

		points.Add(OldPoint);
		pointObjects.Add(OldPointObject);
		for (int i = 1; i<frequency; i++)
        {
			Debug.Log("runs");
			Point NewPoint = new Point() { position = OldPoint.position+GetDistanceBetweenPoints, prevPosition = OldPoint.position + GetDistanceBetweenPoints };
			points.Add(NewPoint);
			sticks.Add(new Stick(OldPoint, NewPoint));
		
			NewPointObject = Instantiate(pointObject, NewPoint.position, new Quaternion(0f, 0f, 0f, 0f));
			pointObjects.Add(NewPointObject);
			OldPoint = NewPoint;
		}

    }




	public class Point
	{
		public Vector3 position, prevPosition;
		public bool locked;
	}


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
		foreach (Point p in points)
		{
			if (!p.locked)
			{
				Vector3 positionBeforeUpdate = p.position;
				p.position += p.position - p.prevPosition;
				p.position += Vector3.down * gravity * Time.deltaTime * Time.deltaTime;
				p.prevPosition = positionBeforeUpdate;
			}
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
		GameObject New;
		for (int i = 0; i < pointObjects.Count; i++)
		{
			//Debug.Log("POintlist count; " + PointList.Count);
			GameObject old = pointObjects[i];

			if (i < pointObjects.Count - 1)
			{
				New = pointObjects[i + 1];
			}
            else
            {
				New = endPosition;
            }


			lr = New.GetComponent<LineRenderer>();
			lr.SetPosition(0, old.transform.position);
			lr.SetPosition(1, New.transform.position);

			if (i == pointObjects.Count - 1)
			{
				lr.SetPosition(0, old.transform.position);
				lr.SetPosition(0, endPosition.transform.position);
			}
			if (i == 0)
			{
				lr.SetPosition(0, startPosition.transform.position);
				lr.SetPosition(0, pointObjects[0].transform.position);
			}

		}
	}


	void OnTriggerEnter(Collider other)
    {
		for (int i = 0; i < pointObjects.Count; i++)
		{
			if (pointObjects[i].gameObject.CompareTag("Player"))
			{
				Debug.Log("Collides");
				points[i].prevPosition += new Vector3(200f, 200f, 200f);
			}
		}
    }


}




