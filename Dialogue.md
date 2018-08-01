# Protagonist Dialogue

## Basic dialogue

Protagonist dialogue is written in JSON. The root of the file must be a JSON array. Each statement is a JSON object in that array.

Here is a simple example:

```javascript
[
    {
        character: "Hades",
        abbrev: "H"
    }
    { H: "Hello, world!" }
]
```
Note the square brackets used to start and end the dialogue. These must exist.

Every character used in the dialogue must have a character definition, which looks like:
```javascript
{
    character: "Hades",
    abbrev: "H"
}
```

This means that, in the game, the character's dialogue will appear as being said by "Hades", but you will refer to Hades with "H".

Then, you can have Hades say something, like:
```javascript
{ H: "Hello, world!" }
```

This will cause the dialogue box to appear, with Hades' nameplate, and have the text "Hello, world!".

## Showing character images

Before displaying a sprite, you need to assign it a position.

There are 4 built-in positions for sprites:
* Left front ("Left" or "Left front")
* Right front ("Right" or "Right front")
* Left back ("Left back")
* Right back ("Right back")

Alternatively, you can specify a custom position, such as "(50, 100)".

```javascript
{
    show: {
        name: "H",
        side: "Right"
    }
}
```

After that, you can specify a sprite to display:
```javascript
{
    show: {
        name: "H",
        side: "Right",
        sprite: "HadesLeft_0"
    }
}
```

You can also specify a transition.
There are several built-in transitions:
* Fade ("Fade")
* Jump ("Jump")
* Swing ("Swing")

Note that if a transition does not exist or is not available for a given sprite, it will throw an error.

Here is an example of a full "show" statement:
```javascript
{
    show: {
        name: "H",
        side: "Right",
        sprite: "HadesLeft_0",
        transition: "Swing"
    }
}
```

After showing the sprite, when you want to change the sprite, you can leave out the "side" and "transition" parameters, and they will be maintained as the sprite's defaults.
```javascript
{
    show: {
        name: "H",
        sprite: "HadesLeft_1"
    }
}
```

When you want to hide the sprite, you can leave out the transition to use what was last specified, or provide a new one.
```javascript
{
    hide: {
        name: "H",
        transition: "Fade"
    }
}
```

## Control statements

You can use variables in the dialogue system via the "var" statement. These variables are the same as the ones used in the global "GameData" dictionary.

```javascript
[
    { var: "x", val: true },
    { var: "y", val: false },
    { var: "z", val: "x || y" }
]
```
The dialogue system only supports boolean flags.

Then, you can use the "if" statement:
```javascript
{
    cond: "z || x",
    if: [
        { H: "If ran." }
    ],
    else: [
        { H: "Else ran." }
    ]
}
```

You can also use method calls, called "labels", via the "jump" statement. Before using a jump statement, however, you must define the label.
Example:
```javascript
[
    { jump: "hadesSaysHi" },
    { jump: "hadesSaysHi" },
    { jump: "hadesSaysHi" },

    {
        label: "hadesSaysHi",
        block: [
            { H: "Hi." }
        ]
    }
]
```
This would cause Hades to say "Hi." 3 times.

## Event Triggers

You can make calls to the game code by setting up an event in the game code, and then calling it using the "event" statement:
```javascript
{
    event: "eventName",
    args: {
        "someArg1": 20
    }
}
```
These are defined in the DialogueEvents class, in the Handle(string event, Dictionary<string, dynamic> args) method.