using System;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public GameObject itemToSpawn;
    private Material pressedMaterial;
    private Material defaultMaterial;

    private void Start() {
        pressedMaterial = Resources.Load<Material>("Materials/Emerald");
        defaultMaterial = Resources.Load<Material>("Materials/White");
    }
    
    private void OnTriggerStay(Collider other) {
        if (!other.CompareTag("Pencil")) return;
        GetComponent<MeshRenderer>().material = pressedMaterial;
        other.GetComponent<ObjectToSpawn>().setItem(itemToSpawn);
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    // private void OnCollisionExit(Collision other) {
    //     GetComponent<MeshRenderer>().material = defaultMaterial;
    // }
    //
    // private void OnCollisionEnter(Collision collision) {
    //     if (!collision.collider.CompareTag("Pencil")) return;
    //     GetComponent<MeshRenderer>().material = pressedMaterial;
    //     collision.collider.GetComponent<ObjectToSpawn>().setItem(itemToSpawn);
    // }
}
