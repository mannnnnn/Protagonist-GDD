using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedMusicPlayer : MonoBehaviour
{
    public List<SyncedAudioClip> clips = new List<SyncedAudioClip>();
    Dictionary<string, SyncedAudioSource> sources = new Dictionary<string, SyncedAudioSource>();

    string current = null;

    public void Initialize(List<SyncedAudioClip> clips)
    {
        this.clips = clips;
    }

    // Use this for initialization
    void Start ()
    {
        // create audio source pool
		foreach (SyncedAudioClip clip in clips)
        {
            sources[clip.key] = new SyncedAudioSource(clip.key, clip.clip, gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // update all audio sources to fade
		foreach (SyncedAudioSource source in sources.Values)
        {
            source.Update();
        }
	}

    public void Play(string key)
    {
        // if it's already playing, do nothing
        if (key == null || key == current)
        {
            return;
        }
        var target = sources[key];
        // play first thing
        if (current == null)
        {
            target.Play(0);
            current = key;
            return;
        }
        // if interrupting the old one
        var currentSource = sources[current];
        target.Play(currentSource.Stop());
        current = key;
    }

    public void Stop()
    {
        if (current == null)
        {
            return;
        }
        sources[current].Stop();
    }

    class SyncedAudioSource
    {
        public string key;
        public AudioClip clip;
        public AudioSource source;

        bool active = false;
        float fadeSpd = 2f;

        public SyncedAudioSource(string key, AudioClip clip, GameObject gameObject)
        {
            this.key = key;
            this.clip = clip;
            source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = true;
        }

        public void Play(int timeSamples)
        {
            active = true;
            if (!source.isPlaying)
            {
                source.Play();
                source.timeSamples = timeSamples;
            }
        }
        public int Stop()
        {
            active = false;
            return source.timeSamples;
        }

        public void Update()
        {
            // fade in/out depending on active or not
            source.volume = Mathf.MoveTowards(source.volume, active ? 1 : 0, fadeSpd * Time.deltaTime);
            if (source.volume == 0 && source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}

[Serializable]
public class SyncedAudioClip
{
    public string key;
    public AudioClip clip;
    public SyncedAudioClip(string key, AudioClip clip)
    {
        this.key = key;
        this.clip = clip;
    }
}