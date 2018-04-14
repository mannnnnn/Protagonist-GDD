///removeDrawer(drawer)
// every drawer should have a self-destruct sequence that initiates when drawer.finish == true
// the self-destruct sequence should eventually lead to drawer's instance_destroy()
// This method starts the drawers's self destruct sequence by setting finish to true.
var drawer = argument0;
drawer.finish = true;
