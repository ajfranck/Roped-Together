using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource Footsteps;
    public AudioClip[] FootClips;
    public AudioSource RockBreak;
    public AudioClip[] RockClips;

    void FootStep()
    {
       int i = Random.Range(0, FootClips.Length);

        Footsteps.clip = FootClips[i];
        Debug.Log("Footstep " + i);
        Footsteps.Play();
    }

    void RockMine()
    {
        int i = Random.Range(0, RockClips.Length);

        RockBreak.clip = RockClips[i];
        RockBreak.Play();
    }

}


