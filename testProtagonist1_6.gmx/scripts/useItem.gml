///useItem(itemID)
var item = argument0;
var bunX = (getDisplayRoomWidth() / 6) + 15;
var bunY = getDisplayRoomHeight() * 0.8;
createParticleEffect(PARTSYS_UI, PARTEFF_DUST, bunX - 20, bunY - 20, bunX + 20, bunY + 20, ps_shape_ellipse, ps_distr_linear, 3);
addNotification(createNotification("You gained +5 in [NULL-STAT]"));
setFlag("ArQual", true);
setFlag("HeQual", false);
