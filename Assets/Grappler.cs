using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public GameObject grappleBlock = null;
    public GameObject thePlayer = null;
    public bool localGrapple;
    public float localGrappleDist;
    public bool withinRange = false;

    void Start()
    {
        _distanceJoint.enabled = false;
        localGrapple = GetComponent<HeroBehavior>().isGrappling;
    }

    void Update()
    {
        /*
        if(GetComponent<Movement>().grappleDist > Vector3.Distance(grappleBlock.transform.position, transform.position)){
            withinRange = true;
            if (Input.GetKey(KeyCode.Space)){
                localGrapple = true;
                _lineRenderer.SetPosition(0, grappleBlock.transform.position);
                _lineRenderer.SetPosition(1, transform.position);
                _distanceJoint.connectedAnchor = grappleBlock.transform.position;
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;
            } else {
                _distanceJoint.enabled = false;
                _lineRenderer.enabled = false;
                localGrapple = false;
            }

            if (_distanceJoint.enabled)
            {
                _lineRenderer.SetPosition(1, transform.position);
            }
        }else{
            withinRange = false;
        }
        */

    }
    private void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Grapple")){
            grappleBlock = col.gameObject;
            //////////////////////////////////////////////////////////////////
            Color tmp = grappleBlock.GetComponent<SpriteRenderer>().color;
            tmp.r = 0;
            tmp.g = 150;
            tmp.b = 0;
            grappleBlock.GetComponent<SpriteRenderer>().color = tmp;
            //////////////////////////////////////////////////////////////////
        }
    }
    private void OnTriggerStay2D(Collider2D col){
        if (col.CompareTag("Grapple")){

            if (Input.GetKey(KeyCode.Space)){
                localGrapple = true;
                _lineRenderer.SetPosition(0, grappleBlock.transform.position);
                _lineRenderer.SetPosition(1, transform.position);
                _distanceJoint.connectedAnchor = grappleBlock.transform.position;
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;
            } else {
                _distanceJoint.enabled = false;
                _lineRenderer.enabled = false;
                localGrapple = false;
            }

            if (_distanceJoint.enabled)
            {
                _lineRenderer.SetPosition(1, transform.position);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Grapple")){
            //////////////////////////////////////////////////////////////////
            Color tmp = grappleBlock.GetComponent<SpriteRenderer>().color;
            tmp.r = 150;
            tmp.g = 0;
            tmp.b = 0;
            grappleBlock.GetComponent<SpriteRenderer>().color = tmp;
            //////////////////////////////////////////////////////////////////
            grappleBlock = null;
        }
    }
    
}
