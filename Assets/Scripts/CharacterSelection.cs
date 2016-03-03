using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public int controllerValue = 0;
    public bool inputVertical = true;
    public bool inputHorizontal = true;

    GameObject PlayerUI;
    GameObject controllerText;
    GameObject readyText;
    GameObject colorPanel;
    GameObject[] color;
    GameObject fashionPanel;
    RectTransform cursor;
    bool panelActive = true;
    float[] colorPosition = new float[3];
    int cursorIndex = 0;

    void Start() {
        colorPosition[0] = 19f;
        colorPosition[1] = 0f;
        colorPosition[2] = -19f;
        controllerText = transform.Find("Controller").gameObject;
        readyText = transform.Find("Ready").gameObject;
        colorPanel = transform.Find("Color Panel").gameObject;
        fashionPanel = transform.Find("Fashion Panel").gameObject;
        cursor = transform.Find("Color Panel").transform.Find("Cursor").GetComponent<RectTransform>();
    }

    void Update() {
        if (Input.GetButtonDown("ExplodeC" + (controllerValue + 1))) {
            if (panelActive) {
                DeactivatePanel();
            }
            else {
                ActivatePanel();
            }
        }
        ControlSelection();
    }

    void ActivatePanel() {
        controllerText.SetActive(false);
        readyText.SetActive(true);
        colorPanel.SetActive(true);
        fashionPanel.SetActive(true);
        panelActive = true;
    }

    void DeactivatePanel() {
        controllerText.SetActive(true);
        readyText.SetActive(false);
        colorPanel.SetActive(false);
        fashionPanel.SetActive(false);
        panelActive = false;
    }

    void ControlSelection() {
        if (panelActive) {
            if (inputVertical) {
                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    if (cursorIndex > 0) {
                        cursorIndex -= 1;
                    }
                    cursor.anchoredPosition = new Vector2(cursor.anchoredPosition.x, colorPosition[cursorIndex]);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    if (cursorIndex < colorPosition.Length - 1) {
                        cursorIndex += 1;
                    }
                    cursor.anchoredPosition = new Vector2(cursor.anchoredPosition.x, colorPosition[cursorIndex]);
                }
            }

            if (inputHorizontal) {
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    if (cursorIndex > 0) {
                        cursorIndex -= 1;
                    }
                    cursor.anchoredPosition = new Vector2(cursor.anchoredPosition.x, colorPosition[cursorIndex]);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    if (cursorIndex < colorPosition.Length - 1) {
                        cursorIndex += 1;
                    }
                    cursor.anchoredPosition = new Vector2(cursor.anchoredPosition.x, colorPosition[cursorIndex]);
                }
            }
        }
    }
}
