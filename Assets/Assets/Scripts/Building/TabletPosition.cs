using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletPosition : MonoBehaviour {
    private bool isGrabbing;
    public Transform player;
    public GameObject pen;
    private Quaternion rotation;
    void Start() {
        isGrabbing = false;
    }
    void Update() {
        if (isGrabbing) {
            pen.SetActive(true);
            return;
        }
        pen.SetActive(false);
        rotation = player.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.position = new Vector3(player.position.x, + player.position.y - 0.36f,player.position.z);
        transform.rotation = rotation;
    }

    public void SetIsGrabbing(bool value) {
        isGrabbing = value;
    }
}
