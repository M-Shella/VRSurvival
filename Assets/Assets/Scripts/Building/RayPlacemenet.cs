using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;

public class RayPlacemenet : MonoBehaviour {
    private GameObject objectToSpawn;
    private bool enabled;
    public XRHandControllerLink turnController;
    [Header("Input")]
    public Common2DAxis turnAxis;

    private bool rotationDelay;
    private float rotation;

    void Start() {
        enabled = false;
        rotationDelay = true;
        ChangeObjectToSpawn();
        transform.GetChild(0).rotation = Quaternion.identity;
    }

    void FixedUpdate() {
        transform.GetChild(0).gameObject.SetActive(enabled);
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity)) {
            enabled = false;;
            return;
        }
        if (!hit.transform.CompareTag("Terrain") && !hit.transform.CompareTag("InGrid")) {
            enabled = false;
            return;
        }
        enabled = true;
        
        //Rotation
        if (rotationDelay && (turnController.GetAxis2D(turnAxis).x < -0.2 || turnController.GetAxis2D(turnAxis).x > 0.2)) {
            if (turnController.GetAxis2D(turnAxis).x < -0.1) rotation -= 90f;
            if (turnController.GetAxis2D(turnAxis).x > 0.1) rotation += 90f;
            rotationDelay = false;
        }else if(turnController.GetAxis2D(turnAxis).x > -0.05 && turnController.GetAxis2D(turnAxis).x < 0.05) rotationDelay = true;

        transform.GetChild(0).position = objectToSpawn.CompareTag("InGrid") 
            ? new Vector3( Mathf.Floor(hit.point.x/2.5f) * 2.5f, Mathf.Floor(hit.point.y/2.5f) * 2.5f, Mathf.Floor(hit.point.z/2.5f) * 2.5f) 
            : hit.point;
        
        transform.GetChild(0).rotation = Quaternion.Euler(0f,rotation,0f);
    }

    public void SpawnItem() {
        if (!enabled) return;
        Instantiate(objectToSpawn, transform.GetChild(0).position, transform.GetChild(0).rotation);
    }

    public void ChangeObjectToSpawn() {
        objectToSpawn = GetComponent<ObjectToSpawn>().item;
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = GetComponent<ObjectToSpawn>().item.GetComponent<MeshFilter>().sharedMesh;
    }    
}
