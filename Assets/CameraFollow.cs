using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offsetx = 0;
    public float offsety = 20;
    public bool following = true;
    public float followSpeed = 0.125f;
    //public Vector3 offset;

    void Update()
    {
        if (following){
            Vector3 newPos = new Vector3(target.position.x + offsetx, target.position.y + offsety, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed*Time.deltaTime);
        }
        // Vector3 desiredPosition = target.position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // transform.position = smoothedPosition;

        //transform.LookAt(target);
    }
}
