using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Path")]
public class EnemyPathSOMaker : ScriptableObject
{
    [SerializeField] public GameObject enemyPathPrefab;
                     
    [SerializeField] public List<float> enemyPathTransitionTimeGapList;

    //validate the sizes in some monobehavior script

}
