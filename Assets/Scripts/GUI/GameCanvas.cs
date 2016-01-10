using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {

    GameManager gameManager;
    [SerializeField] Text clockText;

    void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

	void Update () {
        ShowClock();
	}

    void ShowClock(){
        clockText.text = gameManager.roundClock.ToString();
    }
}
