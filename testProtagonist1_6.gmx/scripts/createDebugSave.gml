///createDebugSave()
// must be run before loading debug mode. Gives the filename of the debug save.
var debugStr = '{ "jungle5": 1.000000, "jungle10": 0.000000, "jungle4": 1.000000, "jungle11": 0.000000, "jungle12": 0.000000, "jungle7": 0.000000, "jungle13": 0.000000, "jungle6": 1.000000, "jungle1": 1.000000, "jungle14": 0.000000, "intro3": 1.000000, "jungle15": 0.000000, "intro2": 1.000000, "jungle16": 0.000000, "intro1": 1.000000, "jungle3": 1.000000, "jungle2": 1.000000, "itemset1": 1.000000, "puzzle2": 1.000000, "puzzle3": 1.000000, "puzzle1": 1.000000, "intro": 3.000000, "jungle9": 0.000000, "puzzle6": 1.000000, "puzzle7": 1.000000, "jungle8": 0.000000, "pause": 1.000000, "puzzle4": 1.000000, "puzzle5": 1.000000, "sphinxx": 1.000000, "launched": 1.000000 }';
/// create debug save file
var fname = getGlobalSavePath("debug.protsav");
var file = file_text_open_write(fname);
file_text_write_string(file, debugStr);
file_text_writeln(file);
file_text_close(file);
return "debug.protsav";
