using System.Collections;
using UnityEngine;

public class WeaponRifle : MonoBehaviour
{
    //Serialize Fields
    [Header("Assignments")]
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;

    [Header("Tweakable Parameters")]
    [SerializeField] float rifleFiringTimePeriod;
    [SerializeField] float bulletDestroyTime = 1f;
    [SerializeField] float maxClampedSpreadVal = 2f; //conversionFactor * maxClampedSpreadVal...
                                                     //... = total angular deflection on either side
    [SerializeField] float initialMaxSpreadVal = 0f; //0 means first shot is fully accurate;
    [SerializeField] float deltaMaxSpreadVal = 0.1f;
    [SerializeField] float conversionFactor = 5;



    //Declarations and Cache
    Coroutine shootRilfeCoroutine;
    AudioSource myAudioSource;
    float currentMaxSpreadVal;


    float randomSpreadVal;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ShootRifle();
    }
    private void ShootRifle()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))   { shootRilfeCoroutine = StartCoroutine(ShootRifleCoroutine()); }
        if (Input.GetKeyUp(KeyCode.Mouse0))     { StopCoroutine(shootRilfeCoroutine); }
        //Spread is reset everytime u start spraying again.

    }

    private IEnumerator ShootRifleCoroutine()
    {
        currentMaxSpreadVal = initialMaxSpreadVal;

        while (true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var effectiveQuaternionicRotation = RecoilGenerator(ref currentMaxSpreadVal);
                myAudioSource.PlayOneShot(myAudioSource.clip);
                GameObject shotBullet = Instantiate(bullet, muzzle.transform.position, effectiveQuaternionicRotation);
                Destroy(shotBullet, bulletDestroyTime);
                yield return new WaitForSeconds(rifleFiringTimePeriod);
            }
        }
    }

    private Quaternion RecoilGenerator(ref float currentMaxSpreadVal)
    {
        randomSpreadVal = Random.Range(-currentMaxSpreadVal, currentMaxSpreadVal);

        //Auxiliary code for debugging START
        if (currentMaxSpreadVal >= maxClampedSpreadVal) { Debug.Log(currentMaxSpreadVal); }
        //Auxiliary code for debugging END

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
