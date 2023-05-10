using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSequence : MonoBehaviour
{

    BoxCollider2D bc;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Piston"))
        {
            Debug.Log("TESTING Collided with piston, die now");
            DeathAnimation();
        }
    }

    private void DeathAnimation(){
        GetComponent<HeroBehavior>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().following = false;
        bc.enabled = false;
        GameObject.Find("Piston").GetComponent<PistonMovement>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<CameraShake>().CallShake();
        rb.velocity = new Vector2(1500.0f, 500.0f);
    }
}
