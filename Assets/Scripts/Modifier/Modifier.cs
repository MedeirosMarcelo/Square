using UnityEngine;
using System.Collections;

public enum ModifierType {
    Any,
    Runner,
    Bomber
}

public class Modifier : MonoBehaviour {

    public ModifierType type;
    public GameObject equipment;
    Animator animator;

    protected void Start() {
        animator = GetComponent<Animator>();
    }

    public void PickUp(GameObject owner) {
        GameObject equip = (GameObject)Instantiate(equipment);
        equip.transform.SetParent(owner.transform);
        equip.transform.localPosition = new Vector3(0f, 0.64f, 0f);
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
                PickUp(col.gameObject);
            }
            else {
                if (type.ToString() == col.name) {
                    PickUp(col.gameObject);
                }
                else {
                    Remove();
                }
            }
        }
    }
}
