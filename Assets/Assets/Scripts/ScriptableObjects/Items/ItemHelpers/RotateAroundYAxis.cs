using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundYAxis : MonoBehaviour {
    private void Start() {
        gameObject.GetComponent<MeshFilter>().mesh  = GetItem().GetComponent<MeshFilter>().sharedMesh;
        gameObject.GetComponent<MeshRenderer>().materials = GetItem().GetComponent<MeshRenderer>().sharedMaterials;
        transform.localScale = GetItem().transform.localScale;
    }

    private GameObject GetItem() {
        return gameObject.transform.parent.GetComponent<Item>().item;
    }
    private void Update() {
        transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
