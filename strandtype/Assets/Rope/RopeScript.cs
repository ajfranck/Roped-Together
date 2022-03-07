using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{


   
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject Player1;


    public GameObject section;
   

    [HideInInspector]
    public GameObject NewPoint = null;
    [HideInInspector]
    public GameObject OldPoint = null;
    

    public LineRenderer lr;

    public List<GameObject> PointList = new List<GameObject>();
    private bool simulate = false;

    private Vector3 totalDistance;
    public int Frequency = 10;

    

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            InstantiateSections();
            simulate = true;
        }

        if (simulate)
        {
            RenderLine();
        }
    }
    
    private Vector3 GetDistance()
    {

        float frequency = 10f;

        float segmentLengthX = (endPoint.transform.position.x - startPoint.transform.position.x)/frequency;
        float segmentLengthY = (endPoint.transform.position.y - startPoint.transform.position.y) / frequency;
        float segmentLengthZ = (endPoint.transform.position.z - startPoint.transform.position.z) / frequency;

        Vector3 segmentLength = new Vector3(segmentLengthX, segmentLengthY, segmentLengthZ);
        return segmentLength;
    }

    private void InstantiateSections()
    {


        Vector3 toEnd = (endPoint.transform.position - startPoint.transform.position).normalized;
        Quaternion toEndQuad = Quaternion.Euler(toEnd);
        


        Vector3 SectionLengthToAdd = GetDistance();

        Vector3 SectionPoint = startPoint.transform.position + SectionLengthToAdd;
       
        GameObject NewNewPoint = section;

        Vector3 NewPointPosition;

        Vector3 OldPointPosition;



        OldPointPosition = startPoint.transform.position;
        OldPoint = startPoint;

        for(float i = 0; i<=Frequency; i++)
        {
            NewPointPosition = OldPointPosition + SectionLengthToAdd;


            NewPoint = Instantiate(section, NewPointPosition, toEndQuad);

            //NewStick = Instantiate(Stick, OldPointPosition, new Quaternion(0f,90f,0f,0f));

            //NewStick.transform.SetParent(NewPoint.transform);


            Debug.Log("Newpoint position is: " + NewPoint.transform.position);

            NewPoint.GetComponent<HingeJoint>().connectedBody = OldPoint.GetComponent<Rigidbody>();

            if(i == Frequency)
            {
                NewPoint.GetComponent<Rigidbody>().useGravity = false;
                NewPoint.GetComponent<Rigidbody>().isKinematic = true;
                NewPoint.transform.SetParent(Player1.transform);
            }
            OldPoint = NewPoint;

            PointList.Add(NewPoint);

          

            OldPointPosition = NewPointPosition;

        }
    }

    private void RenderLine()
    {
        GameObject New;
        for (int i = 0; i < PointList.Count; i++)
        {
            Debug.Log("POintlist count; " + PointList.Count);
            GameObject old = PointList[i];

            if (i < PointList.Count - 1)
            {
                New = PointList[i + 1];
            }
            else
            {
                New = endPoint;
                
            }
           


            lr = New.GetComponent<LineRenderer>();
            lr.SetPosition(0, old.transform.position);
            lr.SetPosition(1, New.transform.position);

            if(i == PointList.Count - 1)
            {
                lr.SetPosition(0, old.transform.position);
                lr.SetPosition(0, endPoint.transform.position);
            }
            if(i == 0)
            {
                lr.SetPosition(0, startPoint.transform.position);
                lr.SetPosition(0, PointList[0].transform.position);
            }
          

       
        }
    }
    

}
