using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MatchData {

    public static Queue<CharacterType> CharacterRotation { get; private set; }
    public static Dictionary<ControllerId, int> PlayerScore { get; set; }
    public static int[] KillScore { get; set; }

    static MatchData() {
        Reload();
    }

    public static void Reload() {
        LoadDictionaries();
        LoadCharacterRotation();
    }

    static void LoadDictionaries() {
        PlayerScore = new Dictionary<ControllerId, int>();
        foreach(Player pl in PlayerManager.GetPlayerList()){
            PlayerScore.Add(pl.Controller, 0);
        }
    }

    static void LoadCharacterRotation() {
        CharacterRotation = new Queue<CharacterType>();
        CharacterRotation.Enqueue(CharacterType.Runner);
        CharacterRotation.Enqueue(CharacterType.Bomber);
        CharacterRotation.Enqueue(CharacterType.Bomber);
        CharacterRotation.Enqueue(CharacterType.Bomber);

        CharacterRotation = RandomizeList(CharacterRotation);
    }

    public static void RotateCharacters() {
        CharacterType type = CharacterRotation.Dequeue();
        CharacterRotation.Enqueue(type);
    }

    static Queue<CharacterType> RandomizeList(Queue<CharacterType> playerList) {
        //Implement
        return playerList;
    }
}
