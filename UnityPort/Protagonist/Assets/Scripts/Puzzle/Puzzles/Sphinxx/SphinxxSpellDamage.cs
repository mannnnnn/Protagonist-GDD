using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SphinxxSpellDamage
{
    static Dictionary<string, float> spellDamage;
    static SphinxxSpellDamage()
    {
        spellDamage = new Dictionary<string, float>()
        {
            { "", 3 },
            { "SEAR", 25 }
        };
    }

    public static float SpellDamage(string spell)
    {
        if (spellDamage.ContainsKey(spell))
        {
            return spellDamage[spell];
        }
        return spellDamage[""];
    }
}
