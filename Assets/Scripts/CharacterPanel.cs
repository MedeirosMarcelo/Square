using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour {


    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public bool isReady = false;

    public ControllerId controllerId;
    public SkinnedMeshRenderer runner;
    public RectTransform colorContent;
    public Material[] colorMaterial;
    public Sprite[] colorSprite;
    public float colorRectOffset = 20f;

    GameObject controllerText;
    GameObject readyText;
    GameObject colorPanel;
    GameObject fashionPanel;
    Player activatedPlayer;

    IList<RectTransform> colorList;
    int materialIndex = 2;

    ControllerInput input;

    void Start() {
        controllerText = transform.Find("Controller").gameObject;
        readyText = transform.Find("Ready").gameObject;
        colorPanel = transform.Find("Color Panel").gameObject;
        fashionPanel = transform.Find("Fashion Panel").gameObject;
        colorList = new List<RectTransform>(colorContent.GetComponentsInChildren<RectTransform>());
        colorList.RemoveAt(0);
        colorList[2].sizeDelta = new Vector2(27.5f, 19.5f);

        input = new ControllerInput(controllerId);
    }

    void Update() {
        input.Update();

        if (input.menuSubmit) {
            if (isActive) {
                ReadySelection();
            }
            else {
                ActivatePanel();
            }
        }
        else if (input.menuCancel) {
            if (isReady) {
                UnreadySelection();
            }
            else {
                DeactivatePanel();

            }
        }

        if (isActive && !isReady) {
            ControlSelection();
            MoveAndDeform();
        }

        /*
        if (isActive) {
            Debug.Log("Player " + controllerId + "Confirmed=" + isReady + " Color=" + materialIndex);
        }
        */
    }

    void AddPlayer() {
        PlayerManager.RemovePlayer(controllerId);
        activatedPlayer = PlayerManager.AddPlayer(controllerId, "P" + (int)controllerId);
    }

    void RemovePlayer() {
        PlayerManager.RemovePlayer(activatedPlayer);
        PlayerManager.AddPlayer(controllerId, ((int)controllerId).ToString());
    }

    void ActivatePanel() {
        AddPlayer();
        ChangeCharacterColor();

        controllerText.SetActive(false);
        readyText.SetActive(true);
        colorPanel.SetActive(true);
        fashionPanel.SetActive(true);
        runner.gameObject.SetActive(true);
        isActive = true;
    }

    void DeactivatePanel() {
        RemovePlayer();
        controllerText.SetActive(true);
        readyText.SetActive(false);
        colorPanel.SetActive(false);
        fashionPanel.SetActive(false);
        runner.gameObject.SetActive(false);
        isActive = false;
    }

    void ReadySelection() {
        activatedPlayer.colorMaterial = colorMaterial[materialIndex];
        activatedPlayer.colorSprite = colorSprite[materialIndex];
        //activatedPlayer.Hat = selectedHat; -- When there are hats.

        readyText.SetActive(false);
        colorPanel.SetActive(false);
        fashionPanel.SetActive(false);
        isReady = true;
    }

    void UnreadySelection() {
        readyText.SetActive(true);
        colorPanel.SetActive(true);
        fashionPanel.SetActive(true);
        isReady = false;
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
        if (isActive) {
            if (input.menuVertical == -1) {
                MoveDown();
                ChangeCharacterColor();
            }
            else if (input.menuVertical == 1) {
                MoveUp();
                ChangeCharacterColor();
            }
        }
    }
}
