using UnityEngine;
using System.Collections;

public static class ModifierManager {

    public static bool AnalyzeScoreForBalance() {
        if (MatchData.PlayerScore[ControllerId.One] > 0) return true;
        else if (MatchData.PlayerScore[ControllerId.Two] > 0) return true;
        else if (MatchData.PlayerScore[ControllerId.Three] > 0) return true;
        else if (MatchData.PlayerScore[ControllerId.Four] > 0) return true;
        else return false;
    }
}
