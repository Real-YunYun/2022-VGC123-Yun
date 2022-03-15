using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    List<AudioSource> currentAudioSources = new List<AudioSource>();
    bool didPlay;

    // Start is called before the first frame update
    void Start()
    {
        currentAudioSources.Add(gameObject.GetComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(AudioClip clip)
    {
        foreach(AudioSource source in currentAudioSources)
        {
            if (source.isPlaying) continue;
            didPlay = true;
            source.PlayOneShot(clip);
            //source.outputAudioMixerGroup = group;
            break;
        }
        if (!didPlay)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            currentAudioSources.Add(temp);
            temp.PlayOneShot(clip);
        }

        didPlay = false;
    }

}
