using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{

    private AudioSource footStep_Sound;

    [SerializeField]
    private AudioClip[] footStape_clip;

    private CharacterController characterController;
    [HideInInspector]
    public float volume_min, volume_max;
    private float accumulated_Distance;
    [HideInInspector]
    public float step_Distance;




    // Start is called before the first frame update
    void Awake()
    {
        footStep_Sound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootStepSound();
    }

    void CheckToPlayFootStepSound()
    {
        if(!characterController.isGrounded)
        {
            return;
        }

        if(characterController.velocity.magnitude>0)
        {
            accumulated_Distance += Time.deltaTime;

            if(accumulated_Distance>step_Distance)
            {
                footStep_Sound.volume = Random.Range(volume_min, volume_max);
                footStep_Sound.clip = footStape_clip[Random.Range(0, footStape_clip.Length)];
                footStep_Sound.Play();
                accumulated_Distance = 0f;
            }
        }
        else
        {
            accumulated_Distance = 0f;
        }


    }
}
