using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;
    public WarmthBar warmthbar;
    new Vector3 firePosition;

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {

        if(warmthbar.isInteracting)
        {
            Zoom();
        }
        if(!warmthbar.isInteracting)
        {
            Move();
        }
        

    }

    void Zoom()
    {
        // +0.7x, +4.39y, -4.5z for all fire positions
        if(warmthbar.fire1)
        {
            // +0.7x, +4.39y, -4.5z
            firePosition = new Vector3(19.05f, 31.53f, 90.41f);
        }
        else if(warmthbar.fire2)
        {
            firePosition = new Vector3(-14.5f,-7.625f,-21.93f);
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