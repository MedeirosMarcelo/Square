using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    public GameObject LoadingScene;
    public Image LoadingBar;

	// Use this for initialization
	void Start () {
	
	}

    public void LoadLevel() {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine() {
        LoadingScene.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync("LEVEL NAME");

        while (!async.isDone) {
            LoadingBar.fillAmount = async.progress / 0.9f;
            yield return null;
        }
    }
}
