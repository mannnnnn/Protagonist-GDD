using Assets.Scripts.Controllers;
using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/**
 * The master controller of the inventory.
 * Handles the inventory data, and calls InventoryDisplayBehavior as needed to display the data.
 * Add/remove items from here.
 * Register item type JSON files to this behavior in the inspector.
 */
namespace Assets.Scripts.UI.Inventory
{
    public class Inventory : MonoBehaviour, SaveLoadTarget
    {
        public Dictionary<string, ItemType> itemTypes = new Dictionary<string, ItemType>();
        public Dictionary<ItemType, List<Item>> items = new Dictionary<ItemType, List<Item>>();

        public List<string> itemFiles;
        public List<KeyedPrefab> prefabs;
        public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

        public int InventorySize = 25;

        InventoryDisplayBehavior display;

        void Start()
        {
            LoadItemTypes(itemFiles);
            foreach (KeyedPrefab prefab in prefabs)
            {
                Prefabs[prefab.name] = prefab.prefab;
            }
            SaveLoad.Register("inventory", this);
            display = GetComponent<InventoryDisplayBehavior>();
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
                    ItemType itemType = new ItemType(item);
                    itemTypes.Add(itemType.ID, itemType);
                }
            }
        }

        public int Count
        {
            get
            {
                int c = 0;
                foreach (var type in items)
                {
                    c += type.Key.size * type.Value.Count;
                }
                return c;
            }
        }
        public bool IsFull => Count >= InventorySize;

        public ItemType GetItemType(string ID)
        {
            if (!itemTypes.ContainsKey(ID))
            {
                return null;
            }
            return itemTypes[ID];
        }

        // real methods for adding an item to the inventory. Use these, not InventoryDisplayBehavior's version.
        public Item AddItem(ItemType type)
        {
            if (Count + type.size > InventorySize)
            {
                return null;
            }
            Item item = new Item(type);
            if (!items.ContainsKey(type))
            {
                items[type] = new List<Item>();
            }
            items[type].Add(item);
            display.AddItem(item);
            return item;
        }
        public Item AddItem(string ID)
        {
            return AddItem(GetItemType(ID));
        }

        // real methods for removing an item from the inventory.
        public bool RemoveItem(Item item)
        {
            if (!items.ContainsKey(item.type) || !items[item.type].Contains(item))
            {
                return false;
            }
            items[item.type].Remove(item);
            Destroy(item.gameObject);
            return true;
        }

        public object GetSaveData()
        {
            // return Dictionary<string, int> for serializability
            var data = new Dictionary<string, int>();
            foreach (var pair in items)
            {
                data[pair.Key.ID] = pair.Value.Count;
            }
            return data;
        }
        public void LoadSaveData(object save)
        {
            var data = (Dictionary<string, int>)save;
            display.ClearItems();
            items.Clear();
            foreach (var pair in data)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    AddItem(pair.Key);
                }
            }
        }
    }

    public class ItemType
    {
        public string ID { get; private set; }
        public string name { get; private set; }
        public string img { get; private set; }
        public string text { get; private set; }
        public bool edible { get; private set; }
        public string eatText { get; private set; }
        public int size { get; private set; }
        public string message { get; private set; }
        // convert string dictionary to item data
        public ItemType(Dictionary<string, object> item)
        {
            ID = GetKey(item, "ID");
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
        public ItemType type { get; private set; }
        public GameObject gameObject { get; set; }
        public Item(ItemType type)
        {
            this.type = type;
        }
    }
}
