using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_OBJECT : MonoBehaviour
{
    [SerializeField] float timeScale = 1f;

    private void Update()
    {
        Time.timeScale = timeScale;
    }
}
