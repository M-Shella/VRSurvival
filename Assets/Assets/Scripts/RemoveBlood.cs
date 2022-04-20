using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlood : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")) {
            StartCoroutine(SomeDelay());
        }
    }

    private IEnumerator SomeDelay() {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
