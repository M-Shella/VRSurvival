using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyOpenScript : MonoBehaviour {
    [SerializeField] private GameObject inventory;
    private bool isInventoryOpen;

    private void Start() {
        isInventoryOpen = false;
        inventory.SetActive(isInventoryOpen);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.tag.Equals("RHand")) return;
        isInventoryOpen = !isInventoryOpen;
        inventory.SetActive(isInventoryOpen);
    }
}
