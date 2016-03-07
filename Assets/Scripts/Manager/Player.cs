using UnityEngine;
using System.Collections;

public class Player {

    public ControllerId Controller { get; set; }
    public string Name { get; set; }
    public BaseCharacter Character { get; set; }
    public Material colorMaterial;
    public GameObject Hat;

    public Player(ControllerId controller, string name) {
        this.Controller = controller;
        this.Name = name;
    }

    //public Material Material{
    //    get {
    //        switch (color.ToString()) {
    //            case "Red":
    //                return 1;
    //        }
    //    }
    //}
}