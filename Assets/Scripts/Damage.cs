using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 15;

    public int GetDamage() { return damage; }
}
