using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //serialize fields
    [SerializeField] float speed;
    [SerializeField] GameObject playerParent;


    //class-member declarations and cache
    Rigidbody2D myRigidbody;
    Collider2D myCollider;
    Vector2 prevPos;
    LayerMask wallLayerMask;

    private void Start()
    {
        wallLayerMask = LayerMask.GetMask("Walls");
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        prevPos = transform.position;
        MoveBullet();
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(prevPos, currentPos, (prevPos - currentPos).magnitude);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            Debug.Log(hit.collider.name);

            if (hit.collider.gameObject.tag == "Wall")
            {
                
                Debug.Log("TOUCHHHHHHHH");
                //Destroy(gameObject);
            }
           
             


            //Debug.Log(hit.collider.gameObject.name);
        }
    }



    private void MoveBullet()
    {
        myRigidbody.velocity = new Vector2(transform.up.normalized.x * speed, transform.up.normalized.y * speed);
    }

    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    */
}
