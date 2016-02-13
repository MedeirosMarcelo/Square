using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour {

    public float duration = 5f;
    public bool Active { get; set; }
    public BaseCharacter Owner { get; set; }
    public ModifierPickUp pickUpObject;
    public Sprite icon;

    protected void Start() {
	
	}
	
	void Update () {
	
	}

    public void PickUp(BaseCharacter owner) {
        Owner = owner;
        transform.SetParent(owner.transform);
        transform.localPosition = new Vector3(0f, 0.64f, 0f);
        Active = true;
        Owner.modifierObj.Add(this.gameObject);
    }

    public void Remove() {
        Owner.modifierObj.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
