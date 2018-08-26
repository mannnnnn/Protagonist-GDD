using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A Tri-Music source is a reusable collection of an intro, loop, and an outro.
 * It can be played and stopped.
 * Any number of the intro, loop, and outro can be null to skip it.
 */
public class TriMusicSource : MonoBehaviour
{
    enum State
    {
        INTRO, LOOP, OUTRO, INACTIVE
    }
    State state = State.INACTIVE;
    public bool IsPlaying => state != State.INACTIVE;

    AudioSource source;
    bool introPlayed = false;
    bool loopPlayed = false;
    bool outroPlayed = false;

    // whether or not to loop the middle section
    bool loopMiddle = true;

    AudioClip intro;
    AudioClip loop;
    AudioClip outro;
    public void Initialize(AudioClip intro, AudioClip loop, AudioClip outro)
    {
        this.intro = intro;
        this.loop = loop;
        this.outro = outro;
    }

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    void Update ()
    {
		if (state == State.INTRO)
        {
            if (intro == null)
            {
                state = State.LOOP;
            }
            else if (!introPlayed)
            {
                introPlayed = true;
                source.clip = intro;
                source.loop = false;
                source.Play();
            }
            else if (!source.isPlaying)
            {
                state = State.LOOP;
            }
        }

        if (state == State.LOOP)
        {
            if (loop == null)
            {
                state = State.OUTRO;
            }
            else if (!loopPlayed)
            {
                loopPlayed = true;
                source.clip = loop;
                source.loop = loopMiddle;
                source.Play();
            }
            else if (!source.isPlaying)
            {
                state = State.OUTRO;
            }
        }

        if (state == State.OUTRO)
        {
            if (outro == null)
            {
                state = State.INACTIVE;
            }
            else if (!outroPlayed)
            {
                outroPlayed = true;
                source.clip = outro;
                source.loop = false;
                source.Play();
            }
            else if (!source.isPlaying)
            {
                state = State.INACTIVE;
            }
        }
    }

    public void Play(bool loop = true)
    {
        introPlayed = false;
        loopPlayed = false;
        outroPlayed = false;
        loopMiddle = loop;
        state = State.INTRO;
    }

    public void Stop()
    {
        // don't do anything if already outro or we're inactive
        if (state == State.INTRO || state == State.LOOP)
        {
            state = State.OUTRO;
            source.Stop();
        }
    }

    // clean up the audio source at the end
    void OnDestroy()
    {
        if (source != null)
        {
            Destroy(source);
        }
    }
}
