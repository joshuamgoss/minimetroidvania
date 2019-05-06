using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCriticalEnemies : MonoBehaviour {

    public GameObject[] enemyList;
    public bool allDead;
    private int length;
    private int aliveSum;

	void Start ()
    {
        allDead = false;
	}
	

	void Update ()
    {
        length =enemyList.Length;
        aliveSum = length;
        for (int a = 0; a< length; a++)
        {
            if (enemyList[a].activeSelf == false)
                aliveSum -= 1;
        }
        allDead = aliveSum == 0;

        if (allDead == true)
            gameObject.SetActive(false);

	}
}
