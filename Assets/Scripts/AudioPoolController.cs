using UnityEngine;

public class AudioPoolController : MonoBehaviour
{
    public float bufferTime; // how much time between the same clip
    private AudioSource[] sources;

    void Awake()
    {
        sources = GetComponents<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        foreach (AudioSource source in sources)
        {
            if(source.isPlaying && source.clip == clip && source.time <= bufferTime)
            {
                Debug.Log("canceling audio!");
                return;
            }
        }
        foreach (AudioSource source in sources)
        {
            if(!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                break;
            }
        }
    }
}
