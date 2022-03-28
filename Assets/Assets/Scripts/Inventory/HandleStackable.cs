using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleStackable : MonoBehaviour {
    private TextMeshPro text;
    public int nuberOfItemsInside;
    private void Start() {
        nuberOfItemsInside = 1;
        text = transform.GetChild(1).GetComponent<TextMeshPro>();
        text.SetText(nuberOfItemsInside.ToString());
    }

    public void ChangeNumberOfItems(int number) {
        text.gameObject.SetActive(true);
        nuberOfItemsInside += number;
        text.SetText(nuberOfItemsInside.ToString());
    }
}
