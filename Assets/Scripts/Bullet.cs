using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        myRigidbody.velocity = new Vector2(transform.up.normalized.x * speed, transform.up.normalized.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
