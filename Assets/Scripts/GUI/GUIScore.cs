using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScore : MonoBehaviour {

    public ControllerId controller;
    GameManager gameManager;
    Text scoreValue;

    void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        scoreValue = GetComponent<Text>();
        transform.parent.GetComponent<Image>().sprite = PlayerManager.GetPlayer(controller).colorSprite;
    }

    void Update() {
        scoreValue.text = "x" + MatchData.PlayerScore[controller].ToString();
    }
}
