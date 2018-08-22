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
     * The way to obtain a DialogParser object.
     * Pass into ReadFile the file path to a .protd file, where the directory starts at "Assets/DialogParser/"
     * This will output a DialogParser object.
     * Used by the component Dialog.
     */
    public class DialogLoader
    {
        public static DialogParser ReadFile(string path)
        {
            string contents = File.ReadAllText("Assets/Dialog/" + path);
            return new DialogParser((List<object>)JsonToData.Deserialize(contents));
        }
    }
}
