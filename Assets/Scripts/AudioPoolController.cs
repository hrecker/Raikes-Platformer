using UnityEngine;

public class AudioPoolController : MonoBehaviour
{
    private AudioSource[] sources;

    void Awake()
    {
        sources = GetComponents<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        foreach(AudioSource source in sources)
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
