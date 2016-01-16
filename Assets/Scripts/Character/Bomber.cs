using UnityEngine;
using System.Collections;

public class Bomber : BaseCharacter {

    bool explosionTriggered;
    bool previousButton1State;
    Timer triggerTimer;

    const float explosionMaxSize = 185f;
    const float explosionTime = 1f;
    float explodingSize;
    Timer explosionTimer;

    public static Color bomberColor = Color.white;
    public static Color explodingColor = Color.red;

    static Texture2D explosionSprite;
    static Texture2D bomberSprite;
    CharacterState state;
    public Explosion explosion;

    void Start() {
        input = new ControllerInput(ControllerId.Two);
        explosion = transform.Find("Explosion").GetComponent<Explosion>();
        name = "Bomber";
        base.Start();
    }

    void Update() {
        input.Update();
        StateMachine();
        input.FixedUpdate();
    }

    protected void StateMachine() {
        switch (state) {
            default:
            case CharacterState.Alive:
                if (canControl && canMove) {
                    Move();
                    ControlInput();
                }
                if (characterHit != null && characterHit is Bomber) {
                    explosion.Explode();
                }
                break;
            case CharacterState.Dead:
                break;
        }
    }

    protected void EnterState(CharacterState newState) {
        CharacterState previoState = newState;
        state = newState;

        switch (state) {
            default:
            case CharacterState.Alive:
                Move();
                break;
            case CharacterState.Dead:
                //   StartRespawn();
                break;
        }
    }

    public override void Reset() {
        explosionTriggered = false;
        triggerTimer = new Timer();
        explosionTimer = new Timer();
        explodingSize = 1.0f;
        BaseColor = bomberColor;
        respawnTime = 0.85f;
        base.Reset();
    }

    //public override void Draw(SpriteBatch spriteBatch) {
    //    base.Draw(spriteBatch);
    //    if (explosionTriggered) {
    //        baseColor = Color.Lerp(baseColor, explodingColor, 0.05f);
    //    }
    //    if (exploding) {
    //        Vector2 origin = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
    //        explodingSize = MathHelper.Lerp(explodingSize, explosionMaxSize, 0.33f);
    //        float scale = 2.0f * explodingSize / sprite.Width;
    //        spriteBatch.Draw(explosionSprite, position, null, null, origin, 0f, Vector2.One * scale, explodingColor, SpriteEffects.None, 0f);
    //    }
    //}

    void UpdateExplosionTrigger() {
        if (explosionTriggered) {
            bool ended = false;
            triggerTimer.TimerCounter(explosionTime, out ended);
            if (ended) {
                explosion.Explode();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                explosionTriggered = true;
            }
        }
    }

    void ControlInput() {
        if (input.explode) {
            explosion.TriggerBomb(this);
        }
    }

    public override void Die(string killerTag) {
        state = CharacterState.Dead;
        if (killerTag == "Explosion" || killerTag == "Bomber") {
            explosion.Explode();
        }
        base.Die(killerTag);
    }

    void ReturnFlag(Flag flag) {
        flag.ResetPosition();
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Explosion" ||
            col.gameObject.tag == "Runner" ||
            col.gameObject.tag == "Bomber") {
            Die(col.gameObject.tag);
        }
    }
}
