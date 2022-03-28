using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeItemMove : MonoBehaviour {
    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.tag.Equals("InventoryWall")) return;
        Debug.Log(collision.gameObject.name);
        switch (collision.gameObject.name) {
            case "px": rb.AddForce(-1,Random.Range(-1f, 1f),Random.Range(-1f, 1f), ForceMode.Impulse);
                break;
            case "mx": rb.AddForce(1,Random.Range(-1f, 1f),Random.Range(-1f, 1f), ForceMode.Impulse);
                break;
            case "py": rb.AddForce(Random.Range(-1f, 1f),-1,Random.Range(-1f, 1f), ForceMode.Impulse);
                break;
            case "my": rb.AddForce(Random.Range(-1f, 1f),1,Random.Range(-1f, 1f), ForceMode.Impulse);
                break;
            case "pz": rb.AddForce(Random.Range(-1f, 1f),Random.Range(-1f, 1f),-1, ForceMode.Impulse);
                break;
            case "mz": rb.AddForce(Random.Range(-1f, 1f),Random.Range(-1f, 1f),1, ForceMode.Impulse);
                break;
        }
    }
}
