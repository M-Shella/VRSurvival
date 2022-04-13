using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToSpawn : MonoBehaviour {
    public GameObject item;
    public void setItem(GameObject item) {
        this.item = item;
        GetComponent<RayPlacemenet>().ChangeObjectToSpawn();
    }
}
