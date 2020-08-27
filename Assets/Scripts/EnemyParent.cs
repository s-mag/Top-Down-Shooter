﻿using System;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    //Serialize Fields
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float stoppingDistance; //distance at which enemy stops as it gets to the player
    [SerializeField] float retreatDistance; //distance at which enemy starts retreating as player approaches enemy. ...
                                            //...THIS SHOULD ALWAYS BE LESSER THAN stoppingDistance!
    [SerializeField] GameObject player;



    //declarations & cache
    Coroutine shootRifleCoroutine;
    EnemyRaycast enemyRaycast;
    Vector2 vectorFromEnemyToPlayer;
    WeaponRifle myWeaponRifle;
    float distanceBetweenPlayerAndEnemy;


    private void Start()
    {
        //BUG Coroutine error comes in console work on it.

        //DEVCODE START
        if (stoppingDistance < retreatDistance)
        {
            Debug.LogError("retreatDistance cannot be greater than stopping distance!");
        }
        //DEVCODE END

        myWeaponRifle = transform.GetChild(0).gameObject.GetComponent<WeaponRifle>();
        enemyRaycast = GetComponent<EnemyRaycast>();

    }

    private void Update()
    {   
        UpdateParams();
        Movement();
        LookTowardsPlayer();
        Shoot();
    }


    
    private void Shoot()
    {
        

        if (enemyRaycast.shouldBeShooting && !myWeaponRifle.isShootRifleCoroutineRunning)
        {
            shootRifleCoroutine = StartCoroutine(myWeaponRifle.ShootRifleCoroutine());
        }


        else if (!enemyRaycast.shouldBeShooting)
        {
            StopCoroutine(shootRifleCoroutine);
            myWeaponRifle.isShootRifleCoroutineRunning = false;
        }
    }

    private void UpdateParams()
    {
        vectorFromEnemyToPlayer = new Vector2(player.transform.position.x - transform.position.x,
                                                     player.transform.position.y - transform.position.y);

        distanceBetweenPlayerAndEnemy = vectorFromEnemyToPlayer.magnitude;
    }


    //private void RaycastCheck()
    //{
    //    RaycastHit2D rayFromMuzzle = Physics2D.Raycast(myMuzzle.transform.position, transform.up,
    //                                                   shootRaycastDistance, ~layermasksToIgnore); //~ is used to exclude enemy 

    //    //DEVCODE 
    //    Debug.DrawLine(myMuzzle.transform.position, myMuzzle.transform.position + (transform.up * shootRaycastDistance), Color.red);

    //    //Debug.Log(rayFromMuzzle.collider.gameObject);

    //    if (rayFromMuzzle.collider.gameObject.tag == "Player")
    //    {
    //        shouldBeShooting = true;
    //    }

    //    else if (rayFromMuzzle.collider.gameObject.tag != "Player")
    //    {
    //        shouldBeShooting = false;
    //    }


    //}

    private void LookTowardsPlayer()
    {
        transform.up = vectorFromEnemyToPlayer.normalized;
    }

    private void Movement()
    {
        if (distanceBetweenPlayerAndEnemy > stoppingDistance)
        {
            FollowPlayer();
        }

        else if (distanceBetweenPlayerAndEnemy < stoppingDistance && distanceBetweenPlayerAndEnemy > retreatDistance)
        {
            StayWhereYouAre();
        }

        else if (distanceBetweenPlayerAndEnemy < retreatDistance)
        {
            MoveAwayFromPlayer();
        }
    }

    private void StayWhereYouAre()
    {
        transform.position = this.transform.position;
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void MoveAwayFromPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed *-1f * Time.deltaTime);
    }
    
}
