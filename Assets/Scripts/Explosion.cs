using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public float bombDelay = 1f;
    public float duration = 2f;
    public Material mat;
    public float matValue;
    GameObject explosion;
    Bomber bomber;
    Timer timer = new Timer();
    Animator animator;
    bool triggered;

    void Start() {
        animator = GetComponent<Animator>();
        
    }

    void Update() {
        ChangeBomberColor();
        mat.SetFloat("_SliceAmount", matValue);
    }

    public void TriggerBomb(Bomber bomber) {
        triggered = true;
        this.bomber = bomber;
        StartCoroutine("Trigger", bombDelay);
    }

    IEnumerator Trigger(float delay) {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    public void Explode() {
        transform.SetParent(null);
        animator.SetBool("Explode", true);
        Destroy(this.gameObject, duration);
    }

    float t = 0;
    void ChangeBomberColor() {
        if (triggered) {
            bomber.BaseColor = Color.Lerp(Bomber.bomberColor, Bomber.explodingColor, t);
            if (t < 1) {
                t += Time.deltaTime / bombDelay;
            }
        }
    }
}
