using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPlacemenet : MonoBehaviour {
    private GameObject objectToSpawn;
    void Start() {
        objectToSpawn = GetComponent<ObjectToSpawn>().item;
        Debug.Log(objectToSpawn.name+ " " +  transform.GetChild(0).name+ " " +  transform.GetChild(1).name);
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = objectToSpawn.GetComponent<MeshFilter>().sharedMesh;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) {
            return;
        }
        else {
            
        }
        if (!hit.transform.CompareTag("Terrain")) return;
        
        transform.GetChild(0).position = hit.point;
        transform.GetChild(0).rotation = Quaternion.identity;
    }

    public void SpawnItem() {
        Instantiate(objectToSpawn, transform.GetChild(0).position, Quaternion.identity);
    }
    
}
