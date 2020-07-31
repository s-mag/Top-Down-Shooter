using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //serialize fields
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;



    //declarations & Cache
    Rigidbody2D myRigidbody;
    Camera mainCamera;



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        Movement();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        var mainMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var gameObjectPos = transform.position;
        var directionToLookIn = new Vector2(mainMousePos.x - gameObjectPos.x, mainMousePos.y - gameObjectPos.y);
        transform.up = directionToLookIn;
    }

    private void Movement()
    {
        
        var controlThrowX = Input.GetAxisRaw("Horizontal");
        var controlThrowY = Input.GetAxisRaw("Vertical");
        float moveSpeedX, moveSpeedY;
        if (Input.GetKey(KeyCode.LeftShift)) //run
        {
            moveSpeedX = controlThrowX * runSpeed;
            moveSpeedY = controlThrowY * runSpeed;
            //animation
        }

        else
        {
            moveSpeedX = controlThrowX * walkSpeed;
            moveSpeedY = controlThrowY * walkSpeed;
            //animation
        }
        Vector2 velocity = new Vector2(moveSpeedX, moveSpeedY);
        myRigidbody.velocity = velocity;
    }

    
}
