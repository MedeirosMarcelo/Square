using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModifierChoice : MonoBehaviour {

    public Modifier[] modifier = new Modifier[1];
    public Image[] modSlot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadModifierList() {
        int rnd = Random.Range(0, 1);
        for (int i = 0; i < modSlot.Length; i++) {
            modSlot[i].sprite = modifier[rnd].icon;
        }
    }
}
