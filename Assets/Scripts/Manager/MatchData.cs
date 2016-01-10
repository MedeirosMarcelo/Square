using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchData {

    public IList<BaseCharacter> PlayerList { get; set; }
    public byte[] PlayerScore { get; set; }
    public int[] KillScore { get; set; }

    public MatchData() {
        PlayerList = new List<BaseCharacter>();
        PlayerScore = new byte[4];
    }
}
