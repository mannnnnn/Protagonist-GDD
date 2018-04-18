using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtagonistCompiler
{
    // which side of the screen a character is on
    public enum Side
    {
        LEFT, RIGHT
    }

    // holds data on a character definition
    public class CharacterDefinition
    {
        public string name;
        public string id;
        public Side side;

        public CharacterDefinition(string name, string id, Side side)
        {
            this.name = name;
            this.id = id;
            this.side = side;
        }

        // character data to string
        public override string ToString()
        {
            return "{ characterID = " + id + ", name = " + name + ", side = " + side + " }";
        }
    }
}
