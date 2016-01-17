using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float minDistance = 32f;
    public float maxDistance = 40f;
    public float distance = 32f;

    public 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float clampedDist = Mathf.Clamp(distance, minDistance, maxDistance);
        transform.position = new Vector3(transform.position.x, clampedDist, transform.position.z);
	}
    
}
