using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Assets.Scripts.Libraries.ProtagonistDialog
{
    public class Parser
    {
        public List<Dictionary<string, Object>> ReadFile(string path)
        {
            string contents = File.ReadAllText("Assets/Dialog/" + path);
            return JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(contents);
        }

        public Dialog Parse(List<Dictionary<string, Object>> json)
        {
            // pre-process characters and labels
            foreach (Dictionary<string, Object> statement in json)
            {
                // character
                if (statement.ContainsKey("character"))
                {

                }
                // label
                if (statement.ContainsKey("label"))
                {

                }
            }
            return null;
        }
    }
}
