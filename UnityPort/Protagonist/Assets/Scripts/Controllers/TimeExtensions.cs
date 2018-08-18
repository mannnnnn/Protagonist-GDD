using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// timescale used for in-game activities
public class GameTime
{
    public static bool Active => timeScale > 0;
    public static float timeScale = 1f;
    public static float deltaTime => Time.deltaTime * timeScale;
}

// timescale used for the UI (so we can stop in-game activities while the UI is up)
public class UITime
{
    public static bool Active => timeScale > 0;
    public static float timeScale = 1f;
    public static float deltaTime => Time.deltaTime * timeScale;
}
