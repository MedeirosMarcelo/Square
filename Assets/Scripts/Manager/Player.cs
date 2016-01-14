using UnityEngine;
using System.Collections;

public class Player {

    public int Number { get; set; }
    public string Name { get; set; }
    public GameObject Character { get; set; }
    //public Input Controller { get; set; }

    public Player(int number, string name, GameObject character) {
        this.Number = number;
        this.Name = name;
        this.Character = character;
    }
}
