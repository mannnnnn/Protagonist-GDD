using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class Inventory : SaveLoadTarget
    {
        public object GetSaveData()
        {
            return null;
        }
        public void LoadSaveData(object save)
        {
        }
    }
}
