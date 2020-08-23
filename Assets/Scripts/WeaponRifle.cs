using System.Collections;
using UnityEngine;

public class WeaponRifle : MonoBehaviour
{
    //Serialize Fields
    [Header("Assignments")]
    [SerializeField] public GameObject muzzle;
    [SerializeField] GameObject bullet;

    [Header("Tweakable Parameters")]
    [SerializeField] float rifleFiringTimePeriod;
    [SerializeField] float bulletDestroyTime = 1f;
    [SerializeField] float maxClampedSpreadVal = 2f;  //conversionFactor * maxClampedSpreadVal...
                                                      //... = total angular deflection on either side
    [SerializeField] float initialMaxSpreadVal = 0f;  //0 means first shot is fully accurate;
    [SerializeField] float deltaMaxSpreadVal = 0.1f;
    [SerializeField] float conversionFactor = 5;



    //Declarations and Cache

    AudioSource myAudioSource;
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
            var effectiveQuaternionicRotation = RecoilGenerator(ref currentMaxSpreadVal);
            myAudioSource.PlayOneShot(myAudioSource.clip);
            GameObject shotBullet = Instantiate(bullet, muzzle.transform.position, effectiveQuaternionicRotation);
            Destroy(shotBullet, bulletDestroyTime);
            yield return new WaitForSeconds(rifleFiringTimePeriod);
        }
    }

    private Quaternion RecoilGenerator(ref float currentMaxSpreadVal)
    {
        randomSpreadVal = Random.Range(-currentMaxSpreadVal, currentMaxSpreadVal);

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

}
