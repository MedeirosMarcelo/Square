using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public bool confirmed;
    public ControllerId controllerId;
    public SkinnedMeshRenderer runner;
    public RectTransform colorContent;
    public Material[] colorMaterial;
    public float colorRectOffset = 20f;

    GameObject controllerText;
    GameObject readyText;
    GameObject colorPanel;
    GameObject fashionPanel;
    Player activatedPlayer;
    bool panelActive;
    IList<RectTransform> colorList;
    int materialIndex = 2;

    void Start() {
        controllerText = transform.Find("Controller").gameObject;
        readyText = transform.Find("Ready").gameObject;
        colorPanel = transform.Find("Color Panel").gameObject;
        fashionPanel = transform.Find("Fashion Panel").gameObject;
        colorList = new List<RectTransform>(colorContent.GetComponentsInChildren<RectTransform>());
        colorList.RemoveAt(0);
        colorList[2].sizeDelta = new Vector2(27.5f, 19.5f);
    }

    void Update() {
        if (Input.GetButtonDown("ExplodeC" + ((int)controllerId)) || Input.GetKeyDown(KeyCode.Return)) {
            if (panelActive) {
                ConfirmSelection();
            }
            else {
                AddPlayer();
                ActivatePanel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace)) {
            DeactivatePanel();
            RemovePlayer();
        }

        ControlSelection();
        MoveAndDeform();
    }

    void AddPlayer() {
        activatedPlayer = PlayerManager.AddPlayer(controllerId, "P1");
    }

    void RemovePlayer() {
        PlayerManager.RemovePlayer(activatedPlayer);
    }

    void ActivatePanel() {
        controllerText.SetActive(false);
        readyText.SetActive(true);
        colorPanel.SetActive(true);
        fashionPanel.SetActive(true);
        runner.gameObject.SetActive(true);
        panelActive = true;
    }

    void DeactivatePanel() {
        controllerText.SetActive(true);
        readyText.SetActive(false);
        colorPanel.SetActive(false);
        fashionPanel.SetActive(false);
        runner.gameObject.SetActive(false);
        panelActive = false;
    }

    float colorSpeed = 2f;
    public Vector2 GetNewPosition(bool moveUp, int i, RectTransform position) {
        Vector2 offsetY;
        if (moveUp)
            offsetY = new Vector2(0, colorRectOffset);
        else
            offsetY = new Vector2(0, -(colorRectOffset));
        return offsetY * i;
    }

    RectTransform teleportedColorRect;
    void MoveUp() {
        teleportedColorRect = colorList[0];
        colorList.RemoveAt(0);
        colorList.Add(teleportedColorRect);
        ControlTexturePointer(true);
    }

    void MoveDown() {
        teleportedColorRect = colorList[colorList.Count - 1];
        colorList.RemoveAt(colorList.Count - 1);
        colorList.Insert(0, teleportedColorRect);
        ControlTexturePointer(false);
    }

    void MoveAndDeform() {
        if (true) {
            int i = 0;
            foreach (RectTransform colorRect in colorList) {
                if (colorRect == teleportedColorRect) {
                    Teleport(colorRect, i);
                }
                else {
                    Move(colorRect, i, 0.1f);
                    if (i == 2)
                        Deform(colorRect);
                    else
                        Straighten(colorRect);
                }
                i++;
            }
        }
    }

    void Move(RectTransform rect, int i, float lerpAmount) {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, GetNewPosition(false, i, rect), lerpAmount * colorSpeed);
    }

    void Teleport(RectTransform rect, int i) {
        Move(rect, i, 1f);
    }

    void Deform(RectTransform rect) {
        rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, new Vector2(27.5f, 19.5f), 0.1f * colorSpeed);
    }
    void Straighten(RectTransform rect) {
        rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, new Vector2(13f, 13f), 0.1f * colorSpeed);
    }

    void ChangeCharacterColor() {
        runner.material = colorMaterial[materialIndex];
    }

    void ConfirmSelection() {
        activatedPlayer.colorMaterial = colorMaterial[materialIndex];
        //activatedPlayer.Hat = selectedHat; -- When there are hats.
        confirmed = true;
    }

    void ControlTexturePointer(bool moveUp) {
        if (moveUp) {
            materialIndex++;
            if (materialIndex == colorMaterial.Length)
                materialIndex = 0;
        }
        else {
            materialIndex--;
            if (materialIndex == -1)
                materialIndex = colorMaterial.Length - 1;
        }
    }

    void ControlSelection() {
        if (panelActive) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                MoveDown();
                ChangeCharacterColor();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                MoveUp();
                ChangeCharacterColor();
            }
        }
    }
}
