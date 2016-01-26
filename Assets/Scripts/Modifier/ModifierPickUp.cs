using UnityEngine;
using System.Collections;

public enum ModifierType {
    Any,
    Runner,
    Bomber
}

public class ModifierPickUp : MonoBehaviour {

    public bool Active { get; set; }
    public ModifierType type;
    public GameObject equipment;
    Animator animator;

    protected void Start() {
        animator = GetComponent<Animator>();
    }

    public void PickUp(BaseCharacter owner) {
        GameObject mod = (GameObject)Instantiate(equipment);
         mod.GetComponent<Modifier>().Owner = owner;
         mod.transform.SetParent(owner.transform);
         mod.transform.localPosition = new Vector3(0f, 0.64f, 0f);
         mod.GetComponent<Modifier>().Active = true;
        //animator.SetBool("Pickup", true);
        Destroy(this.gameObject, 0f);
    }

    public void Remove() {
        //animator.SetBool("Destroy", true);
        Destroy(this.gameObject, 1f);
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Bomber" || col.name == "Runner") {
            if (type == ModifierType.Any) {
                PickUp(col.GetComponent<BaseCharacter>());
            }
            else {
                if (type.ToString() == col.name) {
                    PickUp(col.GetComponent<BaseCharacter>());
                }
                else {
                    Remove();
                }
            }
        }
    }
}
