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
        firePosition = new Vector3(-21,7,-10);
        transform.position = Vector3.SmoothDamp(transform.position, firePosition, ref velocity, smoothTime);
    }

    void Move()
    {
        Vector3 centerPoint = targets.position;
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    
   
}
