using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float stoppingDistance; //distance at which enemy stops as it gets to the player
    [SerializeField] float retreatDistance; //distance at which enemy starts retreating as player approaches enemy. ...
                                            //...THIS SHOULD ALWAYS BE LESSER THAN stoppingDistance!

    public GameObject player;

    private void Start()
    {
        if (stoppingDistance < retreatDistance)
        {
            Debug.LogError("retreatDistance cannot be greater than stopping distance!");
        }

        player = FindObjectOfType<PlayerParent>().gameObject;
    }

    private void Update()
    {
        var distanceBetweenPlayerAndEnemy = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distanceBetweenPlayerAndEnemy > stoppingDistance)
        {
            FollowPlayer();
        }

        else if (distanceBetweenPlayerAndEnemy < stoppingDistance && distanceBetweenPlayerAndEnemy > retreatDistance)
        {

        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    
}
