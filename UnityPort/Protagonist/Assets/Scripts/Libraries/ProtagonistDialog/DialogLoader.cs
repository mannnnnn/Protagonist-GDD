using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    public class DialogLoader
    {
        public static Dialog ReadFile(string path)
        {
            string contents = File.ReadAllText("Assets/Dialog/" + path);
            return new Dialog((List<object>)JsonToData.Deserialize(contents));
        }
    }
}
