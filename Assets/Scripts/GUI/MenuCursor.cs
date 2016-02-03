using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCursor : MonoBehaviour {

    public float speed = 1f;
    public float margin = 1.2f;
    public bool inputVertical = true;
    public bool inputHorizontal = true;
    public RectTransform[] selection;
    public int location { get; private set; }
    int lastLocation;
    RectTransform rectTransform;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        location = 0;
        lastLocation = location;
        this.rectTransform.anchoredPosition = selection[location].anchoredPosition;
        this.rectTransform.sizeDelta = selection[location].sizeDelta; //* margin;
    }

    void Update() {
        ControlSelection();
        Move();
    }

    void ControlSelection() {
        if (inputVertical) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (location == 0) {
                    location = selection.Length - 1;
                }
                else {
                    location -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (location == selection.Length - 1) {
                    location = 0;
                }
                else {
                    location += 1;
                }
            }
        }

        if (inputHorizontal) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (location == 0) {
                    location = selection.Length - 1;
                }
                else {
                    location -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (location == selection.Length - 1) {
                    location = 0;
                }
                else {
                    location += 1;
                }
            }
        }
    }

    void Move() {
        this.rectTransform.anchoredPosition = Vector2.Lerp(this.rectTransform.anchoredPosition, selection[location].anchoredPosition, 0.1f * speed);
        this.rectTransform.sizeDelta = Vector2.Lerp(this.rectTransform.sizeDelta, selection[location].sizeDelta * margin, 0.1f * speed);
    }
}
