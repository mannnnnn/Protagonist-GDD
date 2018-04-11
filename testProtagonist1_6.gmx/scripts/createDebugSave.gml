///createDebugSave()
// must be run before loading debug mode. Gives the filename of the debug save.
var debugStr = '{ "jungle5": 0.000000, "jungle4": 1.000000, "jungle1": 1.000000, "jungle14": 1.000000, "jungle15": 1.000000, "jungle16": 1.000000, "jungle3": 1.000000, "jungle2": 1.000000, "intro": 3.000000, "pause": 1.000000, "puzzle4": 2.000000, "launched": 1.000000, "ApQual": 1.000000, "Ap": 1.000000 }';
/// create debug save file
var fname = getGlobalSavePath("debug.protsav");
var file = file_text_open_write(fname);
file_text_write_string(file, debugStr);
file_text_writeln(file);
file_text_close(file);
return "debug.protsav";
