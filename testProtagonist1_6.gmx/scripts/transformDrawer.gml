///transformDrawer(obj, preserveSpr, preserveImg, preservePos, preserveScale, preserveRot, preserveAlpha, preserveSide)
var obj = argument0;
var preserveSpr = argument1;
var preserveImg = argument2;
var preservePos = argument3;
var preserveScale = argument4;
var preserveRot = argument5;
var preserveAlpha = argument6;
var preserveSide = argument7;

// save old vars
var spr = sprite;
var img = image;
var pX = posX;
var pY = posY;
var sX = scaleX;
var sY = scaleY;
var ang = angle;
var alp = alpha;
var sid = side;

// preserve talk vars always
var oldtalkTimer = talkTimer;
var oldtalkDuration = talkDuration;
var oldtalkFocus = talkFocus;
var oldtalkFocusDuration = talkFocusDuration;
var oldtalkFocusGray = talkFocusGray;
var oldtalkFocusScale = talkFocusScale;
var oldinitScaleX = initScaleX;
var oldinitScaleY = initScaleY;

// change
instance_change(obj, true);

// restore old vars
if (preserveSpr)
{
    sprite = spr;
}
if (preserveImg)
{
    image = img;
}
if (preservePos)
{
    posX = pX;
    posY = pY;
}
if (preserveScale)
{
    scaleX = sX;
    scaleY = sY;
}
if (preserveRot)
{
    angle = ang;
}
if (preserveAlpha)
{
    alpha = alp;
}
if (preserveSide)
{
    side = sid;
}

// preserve talk vars always
talkTimer = oldtalkTimer;
talkDuration = oldtalkDuration;
talkFocus = oldtalkFocus;
talkFocusDuration = oldtalkFocusDuration;
talkFocusGray = oldtalkFocusGray;
talkFocusScale = oldtalkFocusScale;
initScaleX = oldinitScaleX;
initScaleY = oldinitScaleY;

return id;
