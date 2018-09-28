using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * Allows fading in/out of images at the back of the UI but in front of the game.
 * Use UIBackground.TransitionTo(sprite)
 * TransitionTo on the same layer will replace the image on that layer.
 * Using a new layer will display both images. Which one is above is determined by layer value.
 */
public class UIBackground : MonoBehaviour
{
    static UIBackground instance;

    public GameObject UIBackgroundLayer;
    Dictionary<int, UIBackgroundLayer> layers = new Dictionary<int, UIBackgroundLayer>();

    public List<KeyedSprite> backgrounds;
    static Dictionary<string, Sprite> UIBackgrounds = new Dictionary<string, Sprite>();

    void Start()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        foreach (KeyedSprite background in backgrounds)
        {
            UIBackgrounds[background.name] = background.sprite;
        }
    }

    public static void TransitionTo(Sprite s, int layer = 0)
    {
        instance.GetLayer(layer).StartTransition(s);
    }
    public static void TransitionTo(string key, int layer = 0)
    {
        if (string.IsNullOrEmpty(key))
        {
            TransitionTo((Sprite)null, layer);
            return;
        }
        if (!UIBackgrounds.ContainsKey(key))
        {
            throw new InvalidOperationException($"Sprite {key} isn't registered with the UIBackground component.");
        }
        TransitionTo(UIBackgrounds[key], layer);
    }

    private UIBackgroundLayer GetLayer(int layer)
    {
        if (!layers.ContainsKey(layer) || layers[layer] == null)
        {
            layers[layer] = Instantiate(UIBackgroundLayer, transform).GetComponent<UIBackgroundLayer>();
            layers[layer].Temporary = true;
            SortLayers();
        }
        return layers[layer];
    }

    private void SortLayers()
    {
        // filter out nulls
        var layerList = layers.ToList();
        foreach (var layer in layerList)
        {
            if (layer.Value == null)
            {
                layers.Remove(layer.Key);
            }
        }
        // sort layers and set sibling index
        layerList = layers.ToList();
        layerList.Sort((x, y) => x.Key - y.Key);
        int index = 0;
        foreach (var layer in layerList)
        {
            layer.Value.transform.position = new Vector3(layer.Value.transform.position.x,
                layer.Value.transform.position.y, layerList.Count - index);
            index++;
        }
    }
}

[Serializable]
public class KeyedSprite
{
    public string name;
    public Sprite sprite;
}