using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    GameObject explosion;
    public float bombDelay = 1f;
    Timer timer = new Timer();
    float duration = 1;
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

	void Update () {
        ExplosionEffect();
	}

    public void TriggerBomb() {
        StartCoroutine("Trigger", bombDelay);
    }

    IEnumerator Trigger(float delay) {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    public void Explode() {
        transform.SetParent(null);
        animator.SetBool("Explode", true);
    }

    void ExplosionEffect() {
        bool ended = false;
        timer.TimerCounter(duration, out ended);
        if (ended) {
           // Explode();
        }
    }
}
