using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    [SerializeField] int shootRaycastDistance = 10; //Line of sight distance
    [SerializeField] GameObject myMuzzle;
    [SerializeField] LayerMask layermasksToIgnore;
    public bool shouldBeShooting = false;

    private void Update()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        RaycastHit2D rayFromMuzzle = Physics2D.Raycast(myMuzzle.transform.position, transform.up,
                                                       shootRaycastDistance, ~layermasksToIgnore); //~ is used to exclude selected 

        //DEVCODE 
        Debug.DrawLine(myMuzzle.transform.position, myMuzzle.transform.position + (transform.up * shootRaycastDistance), Color.red);

        //Debug.Log(rayFromMuzzle.collider.gameObject);

        if (rayFromMuzzle.collider.gameObject.tag == "Player")
        {
            shouldBeShooting = true;
        }

        else if (rayFromMuzzle.collider.gameObject.tag != "Player")
        {
            shouldBeShooting = false;
        }


    }
}
