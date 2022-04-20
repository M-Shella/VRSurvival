using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordScript : MonoBehaviour {
    public int damage;
    private GameObject blood;
    private void Start() {
        damage = 15;
        blood = Resources.Load<GameObject>("Prefabs/BloodCube");
    }

    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Zombie")) return;
        other.gameObject.GetComponent<ZombieScript>().hp -= damage;
        var contact = other.contacts[0];
        for (int i = 0; i < Random.Range(20,50); i++) {
            Instantiate(blood, new Vector3(contact.point.x + Random.Range(-9,9) * 0.01f,contact.point.y + Random.Range(-9,9) * 0.01f,contact.point.z + Random.Range(-9,9) * 0.01f), Quaternion.identity);
        }
    }
}
