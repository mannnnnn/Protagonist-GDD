using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    public class ParseTree
    {
        public Dictionary<String, CharacterDefinition> characters = new Dictionary<String, CharacterDefinition>();
        public Dictionary<String, LabelDefinition> labels = new Dictionary<String, LabelDefinition>();
        public List<ParseStatement> instructions = new List<ParseStatement>();
    }
}
