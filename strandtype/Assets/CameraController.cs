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
    Quaternion fireRotation;

    private Vector3 velocity;
    private Camera cam;

    public bool camera1;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {

        if(warmthbar.isInteracting && camera1)
        {
            Zoom();
        }
        if(warmthbar.p2isInteracting && !camera1)
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
            firePosition = new Vector3(19.05f, 76.53f, 88.41f);
        }
        else if(warmthbar.fire2)
        {
            firePosition = new Vector3(-14.5f,-7.625f,-21.93f);
        }
        else if(warmthbar.fire3)
        {
            firePosition = new Vector3(99.65477f,7.28326797f,9.95687675f);
        }
        else if(warmthbar.fire4)
        {
            firePosition = new Vector3(-12.5218239f,57.6039925f,101.274681f);
        }
        
        transform.position = Vector3.SmoothDamp(transform.position, firePosition, ref velocity, smoothTime);

    }

    void Move()
    {
        Quaternion target = Quaternion.Euler(35f,0f,0f);
        transform.rotation = target;
        Vector3 centerPoint = targets.position;
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    
   
}