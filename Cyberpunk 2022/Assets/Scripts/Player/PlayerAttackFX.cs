using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFX : MonoBehaviour
{
    private AudioSource _audio;
    [SerializeField] AudioClip[] _audioClip;


    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void PistolAudio() {
        _audio.PlayOneShot(_audioClip[0]);
    }

    public void AutomaticGunAudio() {
        _audio.PlayOneShot(_audioClip[1]);
    }



}
