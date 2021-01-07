using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_Speed=10f;
    public float move_Speed=5f;
    public float crouch_Speed=2f;

    private Transform look_Root;
    private float stand_Hight=1.6f;
    private float crouch_Hight=1f;

    private bool is_Couching;

    private PlayerFootSteps player_footStep;

    private float sprint_volume=1f;
    private float crouch_volume = 0.1f;
    private float walkVolume_min = 0.2f, walkVolume_max = 0.6f;
    private float walk_step_Distance = 0.4f;
    private float sprint_step_distance = 0.25f;
    private float crouch_step_distance = 0.5f;

    private PlayerStats playerStats;
    private float sprint_value = 100f;
    public float sprint_treshold = 10f;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);

        player_footStep = GetComponentInChildren<PlayerFootSteps>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        player_footStep.volume_min = walkVolume_min;
        player_footStep.volume_max = walkVolume_max;
        player_footStep.step_Distance = walk_step_Distance;
    }
    private void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        //if we have stamina we can sprint

       if(sprint_value>0f)
        {
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !is_Couching)
            {
                playerMovement.speed = sprint_Speed;

                player_footStep.step_Distance = sprint_step_distance;
                player_footStep.volume_min = sprint_volume;
                player_footStep.volume_max = sprint_volume;
            }
        }
        if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) && !is_Couching )
        {
            playerMovement.speed = move_Speed;

            player_footStep.step_Distance = walk_step_Distance;
            player_footStep.volume_min = walkVolume_min;
            player_footStep.volume_max = walkVolume_max;
        }


        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !is_Couching)
        {
            sprint_value -= sprint_treshold * Time.deltaTime;
            if(sprint_value <=0f)
            {
                sprint_value = 0f;

                //reset the speed sound

                playerMovement.speed = move_Speed;
                player_footStep.step_Distance = walk_step_Distance;
                player_footStep.volume_min = walkVolume_min;
                player_footStep.volume_max = walkVolume_max;
            }
            playerStats.Display_StaminaStats(sprint_value);
        }

        else
        {
            if(sprint_value!=100f)
            {
                sprint_value += (sprint_treshold / 2f) * Time.deltaTime;
                playerStats.Display_StaminaStats(sprint_value);

                if(sprint_value>100f)
                {
                    sprint_value = 100f;
                }
            }
        }


    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(is_Couching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Hight, 0f);
                playerMovement.speed = move_Speed;

                is_Couching = false;


                player_footStep.step_Distance = walk_step_Distance;
                player_footStep.volume_min = walkVolume_min;
                player_footStep.volume_max = walkVolume_max;

            }
            else
            {
                look_Root.localPosition = new Vector3(0f, crouch_Hight, 0f);
                playerMovement.speed = crouch_Speed;

                is_Couching = true;

                player_footStep.step_Distance = crouch_step_distance;
                player_footStep.volume_min = crouch_volume;
                player_footStep.volume_max = crouch_volume;
            }
        }
    }



}
