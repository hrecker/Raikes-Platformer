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
    private AudioPoolController audioPool;

    void Awake()
    {
        audioPool = GetComponent<AudioPoolController>();
    }

    public void PlayClip(Message message)
    {
        if(audioClips != null && audioPool != null)
        {
            foreach(MessageAudio messageAudio in audioClips)
            {
                if(messageAudio.message == message)
                {
                    audioPool.PlayClip(messageAudio.clip);
                }
            }
        }
    }
}
