using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject boar_prefab, cannibal_prefab;

    public Transform[] cannibal_SpwanPoint, boar_SpwanPoint;

    [SerializeField]
    private int cannibal_Enemy_count, boar_Enemy_count;
    private int initial_cannibal_count, initial_Boar_count;
    public float wait_before_spwan_enemies_time = 10f;



    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        initial_cannibal_count = cannibal_Enemy_count;
        initial_Boar_count = boar_Enemy_count;

        SpwanEnemies();
        StartCoroutine("CheckToSpwanEnemies");
    }

    void MakeInstance()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    void SpwanEnemies()
    {
        SpwanCannibals();
        SpwanBoars();

    }

    void SpwanCannibals()
    {
        int index = 0;

        for(int i=0;i<cannibal_Enemy_count;i++)
        {
            if(index>=cannibal_SpwanPoint.Length)
            {
                index = 0;
            }

            Instantiate(cannibal_prefab, cannibal_SpwanPoint[index].position, Quaternion.identity);
            index++;
        }
        cannibal_Enemy_count = 0;

    }
    void SpwanBoars()
    {

        int index = 0;

        for (int i = 0; i < boar_Enemy_count; i++)
        {
            if (index >= boar_SpwanPoint.Length)
            {
                index = 0;
            }

            Instantiate(boar_prefab, boar_SpwanPoint[index].position, Quaternion.identity);
            index++;
        }
        boar_Enemy_count = 0;

    }

    IEnumerator CheckToSpwanEnemies()
    {
        yield return new WaitForSeconds(wait_before_spwan_enemies_time);

        SpwanCannibals();
        SpwanEnemies();

        StartCoroutine("CheckToSpwanEnemies");
    }

    public void EnemyDied(bool cannibal)
    {
        if(cannibal)
        {
            cannibal_Enemy_count++;

            if(cannibal_Enemy_count>initial_cannibal_count)
            {
                cannibal_Enemy_count = initial_cannibal_count;
            }
        }

        else
        {
            boar_Enemy_count++;
            if(boar_Enemy_count>initial_Boar_count)
            {
                boar_Enemy_count = initial_Boar_count;
            }
        }
    }


    public void StopSpwning()
    {
        StopCoroutine("CheckToSpwanEnemies");
    }
}
