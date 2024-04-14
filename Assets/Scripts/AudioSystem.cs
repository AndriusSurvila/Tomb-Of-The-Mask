using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;

    public AudioClip coin;
    public AudioClip move;
    public AudioClip star;
    public AudioClip bg;

    private void Start()
    {
        musicSource.clip = bg;
        musicSource.Play();
    }

    public void PLaySounds(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
}