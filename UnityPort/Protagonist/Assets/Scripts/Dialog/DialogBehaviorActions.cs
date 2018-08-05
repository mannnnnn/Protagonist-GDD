using Assets.Scripts.Libraries.ProtagonistDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// partial to DialogBehavior class so I don't have to put the statement actions in the same file as the logic
public partial class DialogBehavior
{
    // sides and positions
    public static readonly Dictionary<string, Vector2> sides = new Dictionary<string, Vector2>()
    {
        { "Center", new Vector2(0.5f, 0) },
        { "Left", new Vector2(0.2f, 0) },
        { "Front Left", new Vector2(0.25f, -0.1f) },
        { "Back Left", new Vector2(0.2f, 0) },
        { "Alt Left", new Vector2(0.35f, -0.05f) },
        { "Right", new Vector2(1 - 0.2f, 0) },
        { "Front Right", new Vector2(1 - 0.25f, -0.1f) },
        { "Back Right", new Vector2(1 - 0.2f, 0) },
        { "Alt Right", new Vector2(1 - 0.35f, -0.05f) }
    };

    // image states
    public enum DialogImage
    {
        Normal, Happy, Glitch
    }
    static Dictionary<string, DialogImage> images = new Dictionary<string, DialogImage>();
    static DialogBehavior() {
        foreach (DialogImage img in Enum.GetValues(typeof(DialogImage)))
        {
            images[img.ToString()] = img;
        }
    }

    public GameObject ShowAction(Dictionary<string, object> show)
    {
        var chr = UpdateCharacter(show);
        // make sure sprite is filled in
        string spr = null;
        if (chr.gameObject == null)
        {
            spr = GetValue(show, "sprite");
            if (!DialogPrefabs.Prefabs.ContainsKey(spr))
            {
                throw new ParseError("No DialogPrefab with name '" + spr + "' is registered. See the DialogPrefabs script on the DialogSystem GameObject.");
            }
            chr.gameObject = Instantiate(DialogPrefabs.Prefabs[spr]);
            // transition in
            chr.gameObject.GetComponent<DialogAnimationBehavior>().SetTransition(chr.transition, false, show);
        }
        UpdateCharacterImage(show, chr);
        return chr.gameObject;
    }

    public GameObject HideAction(Dictionary<string, object> show)
    {
        var chr = UpdateCharacter(show);
        // make sure game obj exists
        if (chr.gameObject == null)
        {
            throw new ParseError("Invalid 'hide' statement, character '" + chr.abbrev + "' is not being shown.");
        }
        UpdateCharacterImage(show, chr);
        // transition out
        chr.gameObject.GetComponent<DialogAnimationBehavior>().SetTransition(chr.transition, true, show);
        // set as hidden
        chr.gameObject = null;
        return chr.gameObject;
    }

    private DialogCharacter UpdateCharacter(Dictionary<string, object> show)
    {
        // name is required
        var name = GetValue(show, "name");
        // find the character obj with this name
        if (!dialog.characters.ContainsKey(name))
        {
            throw new ParseError("A character with name '" + name + "' does not exist.");
        }
        var chr = dialog.characters[name];
        // set transition if necessary
        var transition = GetValue(show, "transition", true);
        if (transition != null)
        {
            chr.transition = transition;
        }
        return chr;
    }
    private void UpdateCharacterImage(Dictionary<string, object> show, DialogCharacter chr)
    {
        // set image if necessary
        var image = GetValue(show, "image", true);
        if (image != null)
        {
            if (!images.ContainsKey(image))
            {
                throw new ParseError("The image tag '" + image + "' is not registered in the DialogImage enum.");
            }
            chr.gameObject.GetComponent<DialogAnimationBehavior>().SetImage(images[image]);
        }
    }

    public string GetValue(Dictionary<string, object> show, string name, bool optional = false)
    {
        // see if there is a key with the given name
        if (show.ContainsKey(name) && show[name] is string)
        {
            return (string)show[name];
        }
        else if (!optional)
        {
            throw new ParseError("Statement element 'show' must have a field with name " + name + ".");
        }
        return null;
    }
}
