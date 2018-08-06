using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    /**
     * The way to obtain a Dialog object.
     * Pass into ReadFile the file path to a .protd file, where the directory starts at "Assets/Dialog/"
     * This will output a Dialog object.
     * Used by the component DialogBehavior.
     */
    public class DialogLoader
    {
        public static Dialog ReadFile(string path)
        {
            string contents = File.ReadAllText("Assets/Dialog/" + path);
            return new Dialog((List<object>)JsonToData.Deserialize(contents));
        }
    }
}
