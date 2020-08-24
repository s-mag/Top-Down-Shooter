using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerParent : MonoBehaviour
{
    //serialize fields
    [SerializeField] int health = 100;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    //cache and declarations
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    WeaponRifle myWeaponRifle;
    Coroutine shootRilfeCoroutine;

    void Start()
    {
        myWeaponRifle = transform.GetChild(0).gameObject.GetComponent<WeaponRifle>(); //WeaponRifle.cs in the child
        //REFACTOR the above code
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator =  GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        Movement();
        LookAtMouse();
        ShootRifle();
    }
    private void ShootRifle()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0)) { shootRilfeCoroutine = StartCoroutine(myWeaponRifle.ShootRifleCoroutine()); }
        if (Input.GetKeyUp(KeyCode.Mouse0)) { StopCoroutine(shootRilfeCoroutine); }
        //Spread is reset everytime u start spraying again.

    }


    private void LookAtMouse()  
    {
        
        var mainMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        
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
