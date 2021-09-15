using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip scoreHighlight;

    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        _audioSource.PlayOneShot(jump);
    }

    public void PlayScoreHighlight()
    {
        _audioSource.PlayOneShot(scoreHighlight);
    }
}
