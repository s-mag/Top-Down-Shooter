﻿using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //serialize fields
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;



    //declarations & Cache
    Rigidbody2D myRigidbody;
    Animator myAnimator;



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator =  GetComponentInChildren<Animator>();
    }

    
    void Update()
    {

        Movement();
        LookAtMouse();
        //Shoot();
    }

    private void LookAtMouse()
    {
        
        var mainMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(mainMousePos);
        
        var gameObjectPos = transform.position;
        var directionToLookIn = new Vector2(mainMousePos.x - gameObjectPos.x, mainMousePos.y - gameObjectPos.y).normalized;
        transform.up = directionToLookIn;
        
    }

    private void Movement()
    {
        var controlThrowX = Input.GetAxisRaw("Horizontal");
        var controlThrowY = Input.GetAxisRaw("Vertical");
        bool isMoving = Mathf.Abs(controlThrowX) > 0f || Mathf.Abs(controlThrowY) > 0f;
        float moveSpeedX = 0f, moveSpeedY = 0f;

        if (!isMoving)
        {
            myAnimator.SetTrigger("Idling");
        }

        else if (Input.GetKey(KeyCode.LeftShift)) //run
        {
            moveSpeedX = controlThrowX * runSpeed;
            moveSpeedY = controlThrowY * runSpeed;
            myAnimator.SetTrigger("Moving");
        }

        else 
        {
            moveSpeedX = controlThrowX * walkSpeed;
            moveSpeedY = controlThrowY * walkSpeed;
            myAnimator.SetTrigger("Moving");
        }
        Vector2 velocity = new Vector2(moveSpeedX, moveSpeedY);
        myRigidbody.velocity = velocity;
    }

    
}