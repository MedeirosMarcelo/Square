using UnityEngine;
using System.Collections;

public class PanicButton : Modifier {

    GameManager gameManager;
    Vector3 velocity;

    void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        base.Start();
    }
    
    void Update(){
        CheckPanicButton();
    }

    void CheckPanicButton() {
        if (Active) {
            foreach (Bomber bomber in gameManager.bomberList) {
                if (bomber.input.explode) {
                    ExplodeAll();
                }
            }
        }
    }

    void ExplodeAll() {
        foreach (Bomber bomber in gameManager.bomberList) {
            bomber.explosion.TriggerBomb();
        }
    }
}
