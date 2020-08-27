using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] EnemyPathSOMaker pathSO;


    
    List<Transform> enemyPathList = new List<Transform>();


    int currentTransformIndex = 0;

    private void Start()
    {
        EnemyPathTransformListMaker();
        StartCoroutine(Patrol());
    }
    private void Update()
    {
        //Patrol();
    }


    private void EnemyPathTransformListMaker()
    {
        foreach (Transform child in pathSO.enemyPathPrefab.gameObject.transform)
        {
            enemyPathList.Add(child);
            Debug.Log(child.transform.position);
        }

        Debug.Log(enemyPathList.Count);
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            foreach(Transform currentTransform in enemyPathList)
            {
                currentTransformIndex = enemyPathList.IndexOf(currentTransform);
                Debug.Log(currentTransformIndex);
                yield return new WaitForSeconds(2);
                MoveTowardsNextPoint(currentTransformIndex + 1);
            }
        }
    }


    private void MoveTowardsNextPoint(int nextTransformIndex)
    {
        Debug.Log(enemyPathList[nextTransformIndex].transform.position);
        Vector2.MoveTowards(transform.position, enemyPathList[nextTransformIndex].transform.position, 4);
    }

}
