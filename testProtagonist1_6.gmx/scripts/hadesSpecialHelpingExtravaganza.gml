/// hadesSpecialHelpingExtravaganza(num)
var num = argument0;

switch (num)
{
    // 0: if going to rm_forest2 for the first time, play it
    case 0:
        setHelpTransition(spr_help, 0, rm_forest2);
        break;
    
    // 1: if going to the battle room for the first time, play it
    case 1:
        setHelpTransition(spr_help, 1, rm_test2);
        break;
    
    // 2: if going to rm_forest9 for the boss fight, play the right god help
    case 2:
        if (checkFlag("Ap"))
        {
            setHelpTransition(spr_help, 2, rm_forest9);
        }
        if (checkFlag("Ar"))
        {
            setHelpTransition(spr_help, 3, rm_forest9);
        }
        if (checkFlag("At"))
        {
            setHelpTransition(spr_help, 4, rm_forest9);
        }
        if (checkFlag("He"))
        {
            setHelpTransition(spr_help, 5, rm_forest9);
        }
        break;
}
