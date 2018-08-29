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
        Debug.Log(evt);
        switch (evt)
        {
        }
        return true;
    }

    public bool HandleJungle(string evt, Dictionary<string, object> args)
    {
        Debug.Log(evt);
        switch (evt)
        {
        }
        return true;
    }
}