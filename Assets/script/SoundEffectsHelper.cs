using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour
{
    public static SoundEffectsHelper Instance;

    public AudioClip crashSound;
    public AudioClip splatSound;
    public AudioClip itemGetSound;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakeCrashSound()
    {
        MakeSound(crashSound);
    }

    public void MakeSplatSound()
    {
        MakeSound(splatSound);
    }

    public void MakeItemGetSound()
    {
        MakeSound(itemGetSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}