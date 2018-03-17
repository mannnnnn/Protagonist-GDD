///createDebugSave()
// must be run before loading debug mode. Gives the filename of the debug save.
var debugStr = '{ "dialogue1": 1.000000, "dialogue3": 1.000000, "dialogue2": 1.000000, "intro": 3.000000, "sphinxxWall": 0.000000, "puzzle6": 0.000000, "puzzle7": 0.000000, "puzzle4": 0.000000, "puzzle5": 0.000000, "sphinxx": 1.000000, "launched": 1.000000 }';
/// create debug save file
var fname = getGlobalSavePath("debug.protsav");
var file = file_text_open_write(fname);
file_text_write_string(file, debugStr);
file_text_writeln(file);
file_text_close(file);
return "debug.protsav";
