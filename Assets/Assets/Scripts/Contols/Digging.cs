using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour{
    private WorldGenerator _worldGenerator;

    private void Start() {
        var a = GameObject.Find("/WorldGenerator");
        _worldGenerator = a.GetComponent<WorldGenerator>();
    }

    // private void OnTriggerEnter(Collider other) {
    //     if (!other.gameObject.CompareTag("Terrain")) return;
    //     var position = gameObject.transform.position;
    //     var position1 = other.transform.position;
    //     for (int i = -3; i < 3; i++) {
    //         for (int j = -3; j < 3; j++) {
    //             for (int k = -3; k < 3; k++) {
    //                 var test1 = new Vector3(position.x + (i * .1f), position.y+(j * .1f), position.z+(k * .1f));
    //                 var test2 = position1;
    //                 if (i>=0 && j>=0 && k>=0) {
    //                     test2 = new Vector3(position1.x + (i), position1.y + j, position1.z+(k ));
    //                 }
    //                 
    //                 _worldGenerator.GetChunkFromVector3(test2).RemoveTerrain(test1);
    //             }
    //         }
    //     }
    // }

    private void OnCollisionStay(Collision collision) {
        if (!collision.collider.CompareTag("Terrain")) return;
        ContactPoint contact = collision.contacts[0];
        _worldGenerator.GetChunkFromVector3(collision.collider.gameObject.transform.position).RemoveTerrain(contact.point);
    }
}
