using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerManager {

    static IList<Player> PlayerList = new List<Player>();

    static PlayerManager() {
        AddPlayer(ControllerId.One, "");
        AddPlayer(ControllerId.Two, "");
        AddPlayer(ControllerId.Three, "");
        AddPlayer(ControllerId.Four, "");
    }

    public static void AddPlayer(ControllerId control, string name) {
	    Player pl = new Player(control, name);
	    PlayerList.Add(pl);
    }

    public static void RemovePlayer(Player player) {
	    PlayerList.Remove(player);
    }

    public static IList<Player> GetPlayerList() {
	    return PlayerList;
    }
}