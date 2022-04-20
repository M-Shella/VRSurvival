using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilPosition : MonoBehaviour {
    private bool isGrabbing;
    public Transform player;
    private void Start() {
        isGrabbing = false;
    }

    private void Update() {
        if (isGrabbing) {
            transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.rotation = player.rotation;
    }

    public void SetIsGrabbing(bool value) {
        isGrabbing = value;
    }
}
