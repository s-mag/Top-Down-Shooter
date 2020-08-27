using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


/*
 * Using Delegates and Events to raise en event each time a bullet is shot 
 * for the muzzle flash to occur
*/



public class WeaponRifle : MonoBehaviour
{
    //Serialize Fields
    [Header("Assignments")]
    [SerializeField] public GameObject muzzle;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject bullet;

    [Header("Tweakable Parameters")]
    [SerializeField] bool isMuzzleFlashOn = true;
    [SerializeField] float rifleFiringTimePeriod;
    [SerializeField] float bulletDestroyTime = 1f;
    [SerializeField] float maxClampedSpreadVal = 2f;  //conversionFactor * maxClampedSpreadVal...
                                                      //... = total angular deflection on either side
    [SerializeField] float initialMaxSpreadVal = 0f;  //0 means first shot is fully accurate;
    [SerializeField] float deltaMaxSpreadVal = 0.1f;
    [SerializeField] float conversionFactor = 5;



    //cache and declarations
    AudioSource myAudioSource;
    public bool aBulletWasJustShot = false;
    public bool isShootRifleCoroutineRunning = false;
    float currentMaxSpreadVal;


    float randomSpreadVal;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public IEnumerator ShootRifleCoroutine()
    {
        isShootRifleCoroutineRunning = true;
        currentMaxSpreadVal = initialMaxSpreadVal;

        while (true)
        { 
            ShootOneBullet();
            yield return new WaitForSeconds(rifleFiringTimePeriod);
        }
    }


    private void ShootOneBullet()
    {
        var effectiveQuaternionicRotation = RecoilGenerator(ref currentMaxSpreadVal);
        myAudioSource.PlayOneShot(myAudioSource.clip);
        GameObject shotBullet = Instantiate(bullet, muzzle.transform.position, effectiveQuaternionicRotation);
        if (isMuzzleFlashOn)
        {
            var insantiatedMuzzleFlash = Instantiate(muzzleFlash, muzzle.transform);
            Destroy(insantiatedMuzzleFlash, 0.05f);
        }
        Destroy(shotBullet, bulletDestroyTime);
    }


    private Quaternion RecoilGenerator(ref float currentMaxSpreadVal)
    {
        randomSpreadVal = UnityEngine.Random.Range(-currentMaxSpreadVal, currentMaxSpreadVal);

        //DEVCODE START
        if (currentMaxSpreadVal >= maxClampedSpreadVal) { Debug.Log(currentMaxSpreadVal); }
        //DEVCODE END

        currentMaxSpreadVal += deltaMaxSpreadVal;
        currentMaxSpreadVal = Mathf.Clamp(currentMaxSpreadVal, -maxClampedSpreadVal, maxClampedSpreadVal);
        var randomSpreadAngle = randomSpreadVal * conversionFactor;
        var effectiveRotation = new Vector3(
            muzzle.transform.rotation.eulerAngles.x,
            muzzle.transform.rotation.eulerAngles.y,
            muzzle.transform.rotation.eulerAngles.z + randomSpreadAngle);

        var effectiveQuaternionicRotation = Quaternion.Euler(effectiveRotation);
        return effectiveQuaternionicRotation;
    }

    public float GetRifleFiringTimePeriod()
    {
        return rifleFiringTimePeriod;
    }

}
