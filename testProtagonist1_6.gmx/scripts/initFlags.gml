/// initFlags()
// initializes story data flags to base values.

// intro: indicates whether to use intro1, intro2, or intro3.
// values are 1, 2, or 3
setFlag("intro", 1);

// launched: indicates whether the player has completed the launch animation sequence
setFlag("launched", false);

// dialogueX: indicates whether or not player has completed the jungleX.protd dialogue.
setFlag("jungle1", false);
setFlag("jungle2", false);
setFlag("jungle3", false);
setFlag("jungle4", false);
setFlag("jungle5", false);
setFlag("jungle6", false);
setFlag("jungle7", false);
setFlag("jungle8", false);
setFlag("jungle9", false);
setFlag("jungle10", false);
setFlag("jungle11", false);
setFlag("jungle12", false);
setFlag("jungle13", false);
setFlag("jungle14", false);
setFlag("jungle15", false);
setFlag("jungle16", false);

// puzzleX: indicates whether or not player has completed the Xth puzzle.
setFlag("puzzle1", false);
setFlag("puzzle2", false);
setFlag("puzzle3", false);
setFlag("puzzle4", false);
setFlag("puzzle5", false);
setFlag("puzzle6", false);
setFlag("puzzle7", false);

show_debug_message(json_encode(obj_storyData.data));
