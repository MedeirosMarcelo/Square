using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {

    GameManager gameManager;
    [SerializeField] Text clockText;
    [SerializeField] GameObject modifierChoice;
    [SerializeField] GameObject nextRunner;

    void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

	void Update () {
        ShowClock();
	}

    void ShowClock(){
        clockText.text = gameManager.roundClock.ToString();
    }

    public void ShowModifierChoice(){
        modifierChoice.SetActive(true);
    }

    public void ShowNextRunner() {
        nextRunner.SetActive(true);
    }
}
