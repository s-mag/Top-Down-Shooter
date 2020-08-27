using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Serialize fields
    [SerializeField] int health = 50;

    private void Update()
    {
        DeathOnCheck();
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player Bullet")
        {
            Damage myBullet = other.gameObject.GetComponent<Damage>();
            ReduceHealthBy(myBullet.GetDamage());
            Destroy(myBullet);
        }

    }
    */


    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player Bullet")
        {
            Damage myBullet = other.gameObject.GetComponent<Damage>();
            ReduceHealthBy(myBullet.GetDamage());
            Destroy(myBullet);
        }
    }
    */

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
