using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;

    public float helath = 100f;
    public bool is_player, is_boar, is_cannibal;
    private bool is_Dead;

    private EnemyAudio enemyAudio;

    private PlayerStats playerStats;

   



    private void Awake()
    {
        if(is_boar || is_cannibal)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            //Get enemy audio

            enemyAudio = GetComponentInChildren<EnemyAudio>();

        }

        if(is_player)
        {
            playerStats = GetComponent<PlayerStats>();

        }
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;
        helath -= damage;

        if(is_player)
        {
            playerStats.Display_HealthStats(helath);
            //display the health ui value
        }

        if(is_boar || is_cannibal)
        {
            if(enemyController.Enemy_State==EnemyState.PATROL)
            {
                enemyController.chase_Distance = 50f;
            }
        }
        if(helath<=0)
        {
            playerDied();
            is_Dead = true;

        }
    }



    void playerDied()
    {
        if(is_cannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);

            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;

            StartCoroutine(DeadSound());
            //Enemy manager spwan more enemys

            EnemyManager.instance.EnemyDied(true);
            

        }

        if(is_boar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemyController.enabled = false;

            enemyAnim.Dead();

            StartCoroutine(DeadSound());
            //Enemy manager spwan more enemys

            EnemyManager.instance.EnemyDied(false);

        }

        if(is_player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for(int i=0; i<enemies.Length;i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            //call enemy manager to stop spwan enemys
            EnemyManager.instance.StopSpwning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrrentSelectedWeapon().gameObject.SetActive(false);
            

            
           

        }

        if(tag==Tags.PLAYER_TAG)
        {

            //gameObject.SetActive(false);
           
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOfGameObject", 3f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void TurnOfGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }



}
