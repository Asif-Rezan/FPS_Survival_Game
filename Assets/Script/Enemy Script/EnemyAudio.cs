using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip scream_Clip, die_Clip;

    [SerializeField]
    private AudioClip[] attack_Clip;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayScreamSound()
    {
        audioSource.clip = scream_Clip;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attack_Clip[Random.Range(0, attack_Clip.Length)];
        audioSource.Play();
    }

    public void PlayDeadSound()
    {
        audioSource.clip = die_Clip;
        audioSource.Play();
    }
}
