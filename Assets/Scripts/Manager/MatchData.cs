using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchData {

    public Queue<CharacterType> CharacterRotation { get; private set; }
    public Dictionary<Controller, int> PlayerScore { get; set; }
    public int[] KillScore { get; set; }

    public MatchData() {
        LoadDictionaries();
        LoadCharacterRotation();
    }

    void LoadDictionaries() {
        PlayerScore = new Dictionary<Controller,int>();
        foreach(Player pl in PlayerManager.GetPlayerList()){
            PlayerScore.Add(pl.Controller, 0);
        }
    }

    void LoadCharacterRotation() {
        CharacterRotation = new Queue<CharacterType>();
        CharacterRotation.Enqueue(CharacterType.Runner);
        CharacterRotation.Enqueue(CharacterType.Bomber);
        CharacterRotation.Enqueue(CharacterType.Bomber);
        CharacterRotation.Enqueue(CharacterType.Bomber);

        CharacterRotation = RandomizeList(CharacterRotation);
    }

    public CharacterType GetCharacterInRotation() {

        CharacterType type = CharacterRotation.Dequeue();
        CharacterRotation.Enqueue(type);
        return CharacterRotation.Peek();
    }

    Queue<CharacterType> RandomizeList(Queue<CharacterType> playerList) {
        //Implement
        return playerList;
    }
}
