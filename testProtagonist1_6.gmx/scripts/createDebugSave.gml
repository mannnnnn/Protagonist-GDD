///createDebugSave()
// must be run before loading debug mode. Gives the filename of the debug save.
var debugStr = '{ "jungle5": 0.000000, "jungle10": 0.000000, "jungle4": 0.000000, "jungle11": 0.000000, "jungle12": 0.000000, "jungle7": 0.000000, "jungle13": 0.000000, "jungle6": 0.000000, "jungle1": 0.000000, "jungle14": 0.000000, "jungle15": 0.000000, "jungle16": 0.000000, "intro1": 1.000000, "jungle3": 0.000000, "jungle2": 0.000000, "puzzle2": 0.000000, "puzzle3": 0.000000, "puzzle1": 0.000000, "intro": 2.000000, "jungle9": 0.000000, "puzzle6": 0.000000, "puzzle7": 0.000000, "jungle8": 0.000000, "puzzle4": 0.000000, "puzzle5": 0.000000, "launched": 0.000000 }';
/// create debug save file
var fname = getGlobalSavePath("debug.protsav");
var file = file_text_open_write(fname);
file_text_write_string(file, debugStr);
file_text_writeln(file);
file_text_close(file);
return "debug.protsav";
