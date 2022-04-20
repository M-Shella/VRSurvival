using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitScript : MonoBehaviour {
    public int countDown;
    public TextMeshPro text;
    private bool delay;
    
    void Start() {
        delay = false;
        countDown = 5;
        text.gameObject.SetActive(false);
    }
    
    private void OnTriggerStay(Collider other) {
        if (!other.tag.Equals("RHand")) return;
        text.gameObject.SetActive(true);
        text.text = countDown.ToString();
        if (!delay && countDown >= 0) {
            delay = true;
            StartCoroutine(SomeDelay());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!other.tag.Equals("RHand")) return;
        text.gameObject.SetActive(false);
        countDown = 5;
    }
    
    private IEnumerator SomeDelay() {
        countDown -= 1;
        if(countDown == 0) Application.Quit();
        yield return new WaitForSeconds(1);
        delay = false;
    }
}
