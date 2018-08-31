using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class DialogEvents
{
    public bool HandleIntro(string evt, Dictionary<string, object> args)
    {
        switch (evt)
        {
            case "music":
                return IntroMusic(GetStringArgument(evt, args, "name"));
            case "introDisplay":
                DialogDisplays.SwapTo("Intro");
                return true;
            case "standardDisplay":
                DialogDisplays.SwapTo("Default");
                return true;
            case "save1":
                return true;
            case "exit":
                Utilities.Quit();
                return true;
        }
        return true;
    }

    public bool HandleJungle(string evt, Dictionary<string, object> args)
    {
        switch (evt)
        {
        }
        return true;
    }
    
    private bool IntroMusic(string name)
    {
        switch (name)
        {
            case "intro":
                TriMusicPlayer.Get("Intro").Play();
                break;
            case "introFF":
                TriMusicPlayer.Get("Intro").Stop();
                TriMusicPlayer.Get("IntroFF").Play();
                break;
            case "hades":
                TriMusicPlayer.Get("Intro").Stop();
                TriMusicPlayer.Get("IntroFF").Stop();
                SFXPlayer.Play("Hades");
                break;
        }
        return true;
    }
}