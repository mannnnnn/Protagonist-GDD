using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/**
 * Plays one-shot sound effects.
 */
public class SFXPlayer : MonoBehaviour
{
    public List<KeyedAudioClip> soundEffects;
    static Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();
    static AudioSource source;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        foreach (KeyedAudioClip clip in soundEffects)
        {
            sfx[clip.key] = clip.clip;
        }
    }

    public static void Play(string key)
    {
        if (!sfx.ContainsKey(key))
        {
            throw new InvalidOperationException("Sound effect " + key + " does not exist.");
        }
        source.PlayOneShot(sfx[key]);
    }
}
