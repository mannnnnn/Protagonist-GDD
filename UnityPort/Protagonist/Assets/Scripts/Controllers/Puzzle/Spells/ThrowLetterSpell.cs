using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controllers.Puzzle.Spells
{
    public class ThrowLetterSpell : PuzzleSpell
    {
        SpellInteractionTarget puzzle;
        SpellInputTarget player;
        public void Initialize(string spell, SpellInteractionTarget puzzle, SpellInputTarget player)
        {
            this.puzzle = puzzle;
            this.player = player;
        }
    }
}
