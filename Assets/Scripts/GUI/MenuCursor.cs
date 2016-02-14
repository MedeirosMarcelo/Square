using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCursor : MonoBehaviour {

    public float speed = 1f;
    public float margin = 1.2f;
    public bool inputVertical = true;
    public bool inputHorizontal = true;
    public MenuSelectable[] selection;
    public int location { get; private set; }
    RectTransform rectTransform;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        location = 0;
        selection[location].selected = true;
        this.rectTransform.anchoredPosition = selection[location].rectTransform.anchoredPosition;
        this.rectTransform.sizeDelta = selection[location].rectTransform.sizeDelta;
    }

    void Update() {
        ControlSelection();
        Move();
    }

    void ControlSelection() {
        int newLocation = location;

        if (inputVertical) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (location == 0) {
                    newLocation = selection.Length - 1;
                }
                else {
                    newLocation -= 1;
                }
                ChangeSelection(newLocation);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (location == selection.Length - 1) {
                    newLocation = 0;
                }
                else {
                    newLocation += 1;
                }
                ChangeSelection(newLocation);
            }
        }

        if (inputHorizontal) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (location == 0) {
                    newLocation = selection.Length - 1;
                }
                else {
                    newLocation -= 1;
                }
                ChangeSelection(newLocation);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (location == selection.Length - 1) {
                    newLocation = 0;
                }
                else {
                    newLocation += 1;
                }
                ChangeSelection(newLocation);
            }
        }
    }

    void ChangeSelection(int newLocation) {
        selection[location].selected = false;
        selection[newLocation].selected = true;
        location = newLocation;
    }

    void Move() {
        this.rectTransform.anchoredPosition = Vector2.Lerp(this.rectTransform.anchoredPosition, selection[location].rectTransform.anchoredPosition, 0.1f * speed);
        this.rectTransform.sizeDelta = Vector2.Lerp(this.rectTransform.sizeDelta, selection[location].rectTransform.sizeDelta * margin, 0.1f * speed);
    }
}
