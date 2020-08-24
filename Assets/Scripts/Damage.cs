using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    //cache and declarations
    [SerializeField] int damage = 15;

    public int GetDamage() { return damage; }
}
