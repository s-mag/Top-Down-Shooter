﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    private void Update()
    {
        DeathOnCheck();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Damage myBullet = other.gameObject.GetComponent<Damage>();
            ReduceHealthBy(myBullet.GetDamage());
            Destroy(myBullet);
        }
    }

    public int GetHealth() { return health; }

    public void DeathOnCheck() //run each frame
    {
        if (health <= 0) { Destroy(gameObject); }
    }

    public void ReduceHealthBy(int inflictedDamage)
    {
        health -= inflictedDamage;
    }






}
