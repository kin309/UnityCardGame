using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audios;
    public List<AudioClip> audioList = new List<AudioClip>();

    void Start()
    {
        audios = gameObject.GetComponent<AudioSource>();
        audios.clip = SelectMusic();
        audios.Play();
    }

    AudioClip SelectMusic()
    {
        int musicIndex = Random.Range(0, audioList.Count);
        return audioList[musicIndex];
    }
}
