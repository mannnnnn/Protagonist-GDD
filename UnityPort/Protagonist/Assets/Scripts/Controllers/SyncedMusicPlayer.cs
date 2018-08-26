using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used for playing layered tracks.
 * Pass the layered tracks in the inspector, each with a string key.
 * Then, you can set a track to be the one played using Play(key)
 * The tracks will automatically be synchronized on playtime.
 */
public class SyncedMusicPlayer : MonoBehaviour
{
    public List<KeyedAudioClip> clips = new List<KeyedAudioClip>();
    Dictionary<string, SyncedAudioSource> sources = new Dictionary<string, SyncedAudioSource>();
    int timeSamples = 0;

    string current = null;

    public void Initialize(List<KeyedAudioClip> clips)
    {
        this.clips = clips;
    }

    // Use this for initialization
    void Start ()
    {
        // create audio source pool
		foreach (KeyedAudioClip clip in clips)
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

    public void Play(string key, float fadeSpd = 1f)
    {
        // if we want to stop the current one
        if (key == null && current != null)
        {
            var source = sources[current];
            source.fadeSpd = fadeSpd;
            timeSamples = source.Stop();
            current = null;
            return;
        }
        // if it's already playing, do nothing
        if (key == null || key == current)
        {
            return;
        }
        var target = sources[key];
        target.fadeSpd = fadeSpd;
        // play first thing
        if (current == null)
        {
            target.Play(timeSamples);
            current = key;
            return;
        }
        // if interrupting the old one
        var currentSource = sources[current];
        currentSource.fadeSpd = fadeSpd;
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
        public float fadeSpd { get; set; } = 2f;

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
                source.timeSamples = Math.Min(timeSamples, source.clip.samples - 1);
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
            source.volume = Mathf.MoveTowards(source.volume, active ? 1 : 0, fadeSpd * UITime.deltaTime);
            if (source.volume == 0 && source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}

[Serializable]
public class KeyedAudioClip
{
    public string key;
    public AudioClip clip;
    public KeyedAudioClip(string key, AudioClip clip)
    {
        this.key = key;
        this.clip = clip;
    }
}