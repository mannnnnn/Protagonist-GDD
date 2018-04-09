/// initFlags()
// initializes story data flags to base values.

// intro: indicates whether to use intro1, intro2, or intro3.
// values are 1, 2, or 3
setFlag("intro", 1);

// launched: indicates whether the player has completed the launch animation sequence
setFlag("launched", false);

// whether or not the boss was defeated
setFlag("sphinxxDefeated", false);

// which god chosen
setFlag("Ap", false);
setFlag("Ar", false);
setFlag("At", false);
setFlag("He", false);

// intro scripts
setFlag("ApIntro", false);
setFlag("ArIntro", false);
setFlag("AtIntro", false);
setFlag("HsIntro", false);

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
setFlag("puzzle1", PUZZLE_UNSOLVED);
setFlag("puzzle2", PUZZLE_UNSOLVED);
setFlag("puzzle3", PUZZLE_UNSOLVED);
setFlag("puzzle4", PUZZLE_UNSOLVED);
setFlag("puzzle5", PUZZLE_UNSOLVED);
setFlag("puzzle6", PUZZLE_UNSOLVED);
setFlag("puzzle7", PUZZLE_UNSOLVED);

show_debug_message(json_encode(obj_storyData.data));
