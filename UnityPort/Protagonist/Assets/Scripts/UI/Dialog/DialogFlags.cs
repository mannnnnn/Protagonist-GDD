using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFlags : MonoBehaviour
{
    // init all dialog flags
	void Start ()
    {
        Dialog.flags["ApQual"] = true;
        Dialog.flags["AtQual"] = true;
        Dialog.flags["ArQual"] = true;
        Dialog.flags["HeQual"] = true;
        Dialog.flags["Ap"] = false;
        Dialog.flags["At"] = false;
        Dialog.flags["Ar"] = true;
        Dialog.flags["He"] = false;
    }
}
