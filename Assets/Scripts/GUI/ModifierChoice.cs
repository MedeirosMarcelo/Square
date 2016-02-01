using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModifierChoice : MonoBehaviour {

    public float maxTime = 5f;
    public Modifier[] modifier = new Modifier[3];
    public Image[] modSlot;
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
        for (int i = 0; i < modSlot.Length; i++) {
            int rnd = Random.Range(0, 3);
            modSlot[i].sprite = modifier[rnd].icon;
        }
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
        modsChosen.Add(modifier[0]); //TODO - Remover após criar controle de escolha dos mods.
        modsChosen.Add(modifier[1]);
        gameManager.BuildChosenMods(modsChosen);
    }
}