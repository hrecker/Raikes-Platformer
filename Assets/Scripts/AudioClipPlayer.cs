using UnityEngine;
using System;

[Serializable]
public struct MessageAudio
{
    public Message message;
    public AudioClip clip;
}

public class AudioClipPlayer : MonoBehaviour
{    
    public MessageAudio[] audioClips;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(Message message)
    {
        if(audioClips != null && source != null)
        {
            foreach(MessageAudio messageAudio in audioClips)
            {
                if(messageAudio.message == message)
                {
                    source.clip = messageAudio.clip;
                    source.Play();
                }
            }
        }
    }
}
