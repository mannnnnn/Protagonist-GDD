using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    // TODO: passing around objects and then having to throw around casts everywhere is DUMB.
    // Going to redo this at some point
    public interface SaveLoadTarget
    {
        object GetSaveData();
        void LoadSaveData(object save);
    }

    public class SaveLoad : MonoBehaviour
    {
        public static SaveLoad instance { get; private set; }
        Dictionary<string, SaveLoadTarget> targets = new Dictionary<string, SaveLoadTarget>();
        static string filepath => Application.persistentDataPath + "/save.dat";

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SaveFile();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                LoadFile();
            }
        }

        public static void Register(string key, SaveLoadTarget target)
        {
            if (instance == null)
            {
                throw new InvalidOperationException("SaveLoad has not been initialized yet.");
            }
            if (key == null || target == null)
            {
                throw new InvalidOperationException("Cannot register a null key or target.");
            }
            if (instance.targets.ContainsKey(key))
            {
                throw new InvalidOperationException("Cannot register duplicate key '" + key + "'.");
            }
            instance.targets[key] = target;
        }

        public static void SaveFile()
        {
            if (instance == null)
            {
                throw new InvalidOperationException("SaveLoad has not been initialized yet.");
            }
            // get data
            var data = new Dictionary<string, object>();
            foreach (string target in instance.targets.Keys)
            {
                data[target] = instance.targets[target].GetSaveData();
            }
            // open/create file
            FileStream file;
            if (File.Exists(filepath))
            {
                file = File.OpenWrite(filepath);
            }
            else
            {
                file = File.Create(filepath);
            }
            // save
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public static bool LoadFile()
        {
            if (instance == null)
            {
                throw new InvalidOperationException("SaveLoad has not been initialized yet.");
            }
            // open file
            FileStream file;
            if (File.Exists(filepath))
            {
                file = File.OpenRead(filepath);
            }
            else
            {
                return false;
            }
            // get data
            BinaryFormatter bf = new BinaryFormatter();
            var data = (Dictionary<string, object>)bf.Deserialize(file);
            file.Close();
            // write data to game objects
            foreach (string target in instance.targets.Keys)
            {
                instance.targets[target].LoadSaveData(data[target]);
            }
            return true;
        }
    }
}
