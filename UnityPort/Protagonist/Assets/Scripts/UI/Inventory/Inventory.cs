using Assets.Scripts.Controllers;
using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class Inventory : MonoBehaviour, SaveLoadTarget
    {
        List<ItemType> itemTypes = new List<ItemType>();
        Dictionary<ItemType, List<Item>> items = new Dictionary<ItemType, List<Item>>();

        public List<string> itemFiles;
        public List<KeyedPrefab> prefabs;
        public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

        void Start()
        {
            LoadItemTypes(itemFiles);
            foreach (KeyedPrefab prefab in prefabs)
            {
                Prefabs[prefab.name] = prefab.prefab;
            }
            SaveLoad.Register("inventory", this);
        }

        private void LoadItemTypes(List<string> paths)
        {
            itemTypes.Clear();
            foreach (string path in paths)
            {
                string contents = File.ReadAllText("Assets/Dialog/Items/" + path);
                var data = (List<object>)JsonToData.Deserialize(contents);
                var items = data.Cast<Dictionary<string, object>>().ToList();
                foreach (Dictionary<string, object> item in items)
                {
                    itemTypes.Add(new ItemType(item, itemTypes.Count));
                }
            }
        }

        public object GetSaveData()
        {
            return null;
        }
        public void LoadSaveData(object save)
        {
        }
    }

    public class ItemType
    {
        public int ID { get; private set; }
        public string name { get; private set; }
        public string img { get; private set; }
        public string text { get; private set; }
        public bool edible { get; private set; }
        public string eatText { get; private set; }
        public int size { get; private set; }
        public string message { get; private set; }
        // convert string dictionary to item data
        public ItemType(Dictionary<string, object> item, int ID)
        {
            this.ID = ID;
            name = GetKey(item, "name");
            img = GetKey(item, "img");
            text = GetKey(item, "text");
            edible = Convert.ToBoolean(GetKey(item, "edible"));
            eatText = GetKey(item, "eatText");
            size = Int32.Parse(GetKey(item, "size"));
            message = GetKey(item, "message", true);
        }
        private string GetKey(Dictionary<string, object> item, string key, bool optional = false)
        {
            if (!item.ContainsKey(key))
            {
                if (optional)
                {
                    return null;
                }
                throw new ParseError("Field '" + key + "' is required for all items.");
            }
            return item[key].ToString();
        }
    }

    public class Item
    {
        public int type { get; private set; }
        public GameObject gameObject { get; private set; }
    }
}
