using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public void Load(string name) {
        Application.LoadLevel(name);
    }
}
