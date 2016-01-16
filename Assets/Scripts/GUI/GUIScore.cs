using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScore : MonoBehaviour {

    public Controller controller;
    GameManager gameManager;
    Text scoreValue;

    void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        scoreValue = GetComponent<Text>();
    }

    void Update() {
        scoreValue.text = GameManager.Match.PlayerScore[controller].ToString();
    }
}
