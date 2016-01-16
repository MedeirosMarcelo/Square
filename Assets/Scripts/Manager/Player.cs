using UnityEngine;
using System.Collections;

public class Player {

    public Controller Controller { get; set; }
    public string Name { get; set; }
    public GameObject Character { get; set; }

    public Player(Controller controller, string name) {
        this.Controller = controller;
        this.Name = name;
    }
}