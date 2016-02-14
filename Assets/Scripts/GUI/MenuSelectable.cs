using UnityEngine;
using System.Collections;

public class MenuSelectable : MonoBehaviour {

    public RectTransform rectTransform { get; private set; }
    public bool selected;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
}