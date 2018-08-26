using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TriMusicPlayer : MonoBehaviour
{
    public List<KeyedTriAudioClip> triMusicClips;
    static Dictionary<string, TriMusicSource> sources = new Dictionary<string, TriMusicSource>();

    void Start()
    {
        // create audio source pool
        foreach (KeyedTriAudioClip clip in triMusicClips)
        {
            sources[clip.key] = gameObject.AddComponent<TriMusicSource>();
            sources[clip.key].Initialize(clip.intro, clip.loop, clip.outro);
        }
    }

    public static TriMusicSource Get(string key)
    {
        return sources[key];
    }
}

[Serializable]
public class KeyedTriAudioClip
{
    public string key;
    public AudioClip intro;
    public AudioClip loop;
    public AudioClip outro;
    public KeyedTriAudioClip(string key, AudioClip intro, AudioClip loop, AudioClip outro)
    {
        this.key = key;
        this.intro = intro;
        this.loop = loop;
        this.outro = outro;
    }
}
