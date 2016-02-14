using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModifierChoice : MonoBehaviour {

    public float maxTime = 5f;
    public Modifier[] modifier = new Modifier[3];
    public ModifierButton[] modButton;
    public Text timer;
    Timer time = new Timer();

    IList<Modifier> modsChosen;
    GameManager gameManager;

	void Start () {
        modsChosen = new List<Modifier>();
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        LoadModifierList();
	}

	void Update () {
        CheckEndChoice();
	}

    void LoadModifierList() {
        for (int i = 0; i < modButton.Length; i++) {
            int rnd = Random.Range(0, 3);
            modButton[i].Modifier = modifier[rnd];
        }
    }

    public void SelectModifier(Modifier modifier) {
        modsChosen.Add(modifier);
        EndChoice();
    }

    void CheckEndChoice() {
        if (time.Run(maxTime)) {
            EndChoice();
        }
        else {
            timer.text = ((int)time.GetTimeDecreasing()).ToString();
        }
    }

    void EndChoice() {
        this.gameObject.SetActive(false);
        gameManager.BuildChosenMods(modsChosen);
    }
}