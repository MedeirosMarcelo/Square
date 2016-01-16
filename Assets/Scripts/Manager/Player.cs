using UnityEngine;
using System.Collections;

public class Player {

    public ControllerId Controller { get; set; }
    public string Name { get; set; }
    public BaseCharacter Character { get; set; }

    public Player(ControllerId controller, string name) {
        this.Controller = controller;
        this.Name = name;
    }
}