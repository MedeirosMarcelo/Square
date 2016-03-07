using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerManager {

    static IList<Player> PlayerList = new List<Player>();

    static PlayerManager() {
        AddPlayer(ControllerId.Four, "4");
        AddPlayer(ControllerId.Three, "3");
        AddPlayer(ControllerId.Two, "2");
        AddPlayer(ControllerId.One, "1");
    }

    public static Player AddPlayer(ControllerId control, string name) {
        Player pl = new Player(control, name);
        PlayerList.Add(pl);
        return pl;
    }

    public static void RemovePlayer(Player player) {
        PlayerList.Remove(player);
    }

    public static IList<Player> GetPlayerList() {
        return PlayerList;
    }
}