using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombieController : MonoBehaviour{
    void Start() {
        for (var i = 0; i < transform.childCount; i++) {
            var rand = Random.Range(0, transform.childCount);
            if (!transform.GetChild(rand).name.Contains("Zombie")) continue;
            transform.GetChild(rand).gameObject.SetActive(true);
            break;
        }
    }
}
