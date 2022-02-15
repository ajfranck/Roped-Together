using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class P2CameraController : MonoBehaviour
{
    public Transform targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;
    public P2WarmthBar warmthbar;
    new Vector3 firePosition;

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {

        if(warmthbar.p2isInteracting)
        {
            Zoom();
        }
        if(!warmthbar.p2isInteracting)
        {
            Move();
        }
        

    }

    void Zoom()
    {
        if(warmthbar.fire1)
        {
            firePosition = new Vector3(-14.5f,-10f,-44.5f);
        }
        else if(warmthbar.fire2)
        {

        }
        
        transform.position = Vector3.SmoothDamp(transform.position, firePosition, ref velocity, smoothTime);
    }

    void Move()
    {
        Vector3 centerPoint = targets.position;
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    
   
}
