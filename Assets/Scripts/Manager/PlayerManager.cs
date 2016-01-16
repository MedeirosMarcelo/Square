using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerManager {

    static IList<Player> PlayerList = new List<Player>();

    static PlayerManager() {
        AddPlayer(Controller.One, "");
        AddPlayer(Controller.Two, "");
        AddPlayer(Controller.Three, "");
        AddPlayer(Controller.Four, "");
    }

    public static void AddPlayer(Controller control, string name){
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