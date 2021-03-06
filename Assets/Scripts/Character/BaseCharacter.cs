﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharacterState {
    Alive,
    Dead
}

public enum CharacterType {
    Runner,
    Bomber
}

public class BaseCharacter : MonoBehaviour {

    public float maxSpeed = 15;
    public float acceleration = 0.1f;
    public float deceleration = 0.05f;

    public CharacterType type;
    public Player player;
    public BaseInput input;
    public bool canControl = true;
    public bool canMove = true;
    public Rigidbody rigidbody;
    public Vector3 publicVelocity;
    public CharacterState State { get; protected set; }
    public ControllerId id;
    public IList<GameObject> modifierObj = new List<GameObject>();

    protected float respawnDelay;
    protected GameManager gameManager;
    protected GameObject deathParticle;

    [SerializeField]
    Color baseColor = Color.white;

    IList<GameObject> activeModifiers = new List<GameObject>();
    Timer respawnTimer = new Timer();
    Animator animator;
    ParticleSystem.EmissionModule smallEmission;
    ParticleSystem.EmissionModule largeEmission;
    GameObject trail;
    GameObject trail2;
    GameObject model;
    Vector3 velocity;

    protected virtual void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        animator = transform.Find("Model").GetComponent<Animator>();
        deathParticle = transform.Find("Death Particles").gameObject;
        smallEmission = transform.Find("Running Trail_02").GetComponent<ParticleSystem>().emission;
        largeEmission = transform.Find("Running Trail_01").GetComponent<ParticleSystem>().emission;
        trail2 = transform.Find("Running Trail_01").gameObject;
        trail = transform.Find("Running Trail_01").gameObject;
        rigidbody = GetComponent<Rigidbody>();
        model = transform.Find("Model").gameObject;
        Reset();
        if (player != null) {
            input = new ControllerInput(player.Controller);
            id = player.Controller;
        }
        else {
            input = new ControllerInput(ControllerId.One);
        }
    }

    protected virtual void Update() {
        ShowRunningTrail();
    }

    public virtual void Reset() {
        model.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
        State = CharacterState.Alive;
    }

    protected void Move() {
        Vector3 direction = new Vector3(input.horizontal, 0, input.vertical);

        if (direction == Vector3.zero) {
            velocity.x = Mathf.Lerp(velocity.x, 0, deceleration);
            if (Mathf.Abs(velocity.x - 0) < 0.01f)
                velocity.x = 0;

            velocity.z = Mathf.Lerp(velocity.z, 0, deceleration);
            if (Mathf.Abs(velocity.z - 0) < 0.01f)
                velocity.z = 0;
        }
        else {
            velocity += direction * acceleration;
        }

        velocity = new Vector3(Mathf.Clamp(velocity.x, -1f, 1f), Mathf.Clamp(velocity.y, -1f, 1f), Mathf.Clamp(velocity.z, -1f, 1f));
        Vector3 newPosition = transform.position + (velocity * maxSpeed) * Time.deltaTime;

        rigidbody.MovePosition(newPosition);
        Forward();
        //if (type == CharacterType.Runner) animator.SetFloat("Velocity", (input.horizontal + input.vertical) / 2f);
        if (type == CharacterType.Runner) {
            if (Mathf.Abs(input.horizontal) >= Mathf.Abs(input.vertical)) {
                animator.SetFloat("VelocityX", input.horizontal);
                animator.SetFloat("VelocityY", 0);
            }
            else {
                animator.SetFloat("VelocityX", 0);
                animator.SetFloat("VelocityY", input.vertical);
            }
            
        }
    }

    protected void Forward() {
        var direction = new Vector2(input.horizontal, input.vertical);
        if (direction.magnitude < 0.75f) {
            return;
        }
        if (Mathf.Abs(input.horizontal) >= Mathf.Abs(input.vertical)) {
         //   transform.forward = new Vector3(input.horizontal, 0f, 0f);
            trail2.transform.forward = new Vector3(input.horizontal, 0f, 0f);
            trail.transform.forward = new Vector3(input.horizontal, 0f, 0f);
        }
        else {
        //    transform.forward = new Vector3(0f, 0f, input.vertical);
            trail2.transform.forward = new Vector3(0f, 0f, input.vertical);
            trail.transform.forward = new Vector3(0f, 0f, input.vertical);
        }
    }

    public void HideModifiers(bool hidden) {
        foreach (GameObject mod in modifierObj) {
            Debug.Log(mod.name + modifierObj.Count);
            mod.SetActive(hidden);
        }
        Debug.Log("HideModifiers");
    }

    protected void WaitRespawn() {
        if (respawnTimer.Run(respawnDelay)) {
            Respawn();
        }
    }

    public void Respawn() {
        if (gameManager.State == GameState.Play) {
            GameObject obj = gameManager.SpawnCharacter(player, type, gameManager.currentMap.GetSpawnPosition(type));
            //   foreach (GameObject mod in modifierObj) {
            //      GameObject modObj = Instantiate(mod.gameObject);
            //       modObj.GetComponent<Modifier>().PickUp(this);
            //   }
            Destroy(this.gameObject);
        }
    }

    virtual public void Die(string killerTag) {
        //HideModifiers(true);
        model.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        rigidbody.velocity = Vector3.zero;
        gameManager.RemoveCharacter(this);
    }

    public Color BaseColor {
        get {
            return baseColor;
        }
        set {
            baseColor = value;
            //model.GetComponent<SkinnedMeshRenderer>().material.color = baseColor;
        }
    }

    void ShowRunningTrail() {
        if ((State == CharacterState.Alive) && (input.horizontal != 0 || input.vertical != 0)) {
                smallEmission.enabled = true;
                largeEmission.enabled = true;
                // animator.SetBool("Running", true);
        }
        else {
            if (smallEmission.enabled != false) {
                smallEmission.enabled = false;
                largeEmission.enabled = false;
                //    animator.SetBool("Running", false);
            }
        }
    }
}
