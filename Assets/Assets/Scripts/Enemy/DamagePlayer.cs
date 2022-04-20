using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
    private int damage;
    private bool delay;

    private void Start() {
        damage = World.Instance.day + 10;
        delay = true;
        StartCoroutine(SomeDelay());
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || delay) return;
        other.GetComponent<Player>().Damage(damage);
        delay = true;
        StartCoroutine(SomeDelay());

    }
    
    private IEnumerator SomeDelay() {
        yield return new WaitForSeconds(1);
        delay = false;
    }
}
