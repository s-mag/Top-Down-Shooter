using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] GameObject rifle;




    float muzzleFlashTime;
    Light2D muzzleFlashLightComponent;
    Coroutine muzzleFlashCoroutine;
    WeaponRifle rifleScript;
    bool isCoroutineRunning = false;

    private void Start()
    {
        rifleScript = rifle.GetComponent<WeaponRifle>();
        muzzleFlashTime = rifleScript.GetRifleFiringTimePeriod();
        
        muzzleFlashLightComponent = transform.GetChild(0).gameObject.GetComponent<Light2D>();
    }

    private void Update()
    {
        MuzzleFlashMethod();
    }

    private void MuzzleFlashMethod()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isCoroutineRunning)
        {
            
            muzzleFlashCoroutine = StartCoroutine(MuzzleFlashCoroutine());

        }
        
    }

    private IEnumerator MuzzleFlashCoroutine()
    {
        isCoroutineRunning = true;
        muzzleFlashLightComponent.enabled = true;
        yield return new WaitForSeconds(0.01f);
        muzzleFlashLightComponent.enabled = false;
        isCoroutineRunning = false;
        yield break;
    }
}
