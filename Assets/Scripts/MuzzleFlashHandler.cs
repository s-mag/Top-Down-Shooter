using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashHandler : MonoBehaviour
{
    [SerializeField] GameObject muzzleFlash;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Bullet")
        {
            var insantiatedMuzzleFlash = Instantiate(muzzleFlash, transform);
            Destroy(insantiatedMuzzleFlash, 0.05f);
        }
    }
}
