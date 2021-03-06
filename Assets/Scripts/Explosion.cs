﻿using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public float bombDelay = 1f;
    public float duration = 3f;
    public Material mat;
    public float matValue;
    public GameObject explosionModel;
    public GameObject chargingModel;
    SphereCollider explosionCollider;
    Bomber bomber;
    Timer timer = new Timer();
    Animator animator;
    bool triggered;

    void Start() {
        animator = GetComponent<Animator>();
        bomber = transform.parent.GetComponent<Bomber>();
        explosionCollider = GetComponent<SphereCollider>();
    }

    void Update() {
        ChangeBomberColor();
        //mat.SetFloat("_SliceAmount", matValue);
    }

    public void TriggerBomb() {
        triggered = true;
        chargingModel.SetActive(true);
        StartCoroutine("Trigger", bombDelay);
    }

    IEnumerator Trigger(float delay) {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    public void Explode() {
        if (bomber.State == CharacterState.Alive) {
            transform.SetParent(null);
            explosionModel.SetActive(true);
            explosionCollider.enabled = true;
            Destroy(this.gameObject, duration);
        }
    }

    public void Destroy() {
        Destroy(this.gameObject);
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
