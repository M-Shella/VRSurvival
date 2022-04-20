using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabletGidSystem : MonoBehaviour {
    public GameObject button;
    public int columnLength, rowLength;
    public float x_Space, y_Space;
    public float x_Start, y_Start;

    private GameObject[] items;
    
    void Start() {
        items = Resources.LoadAll<GameObject>("Prefabs/Placement");
        for (int i = 0; i < items.Length; i++) {
            GameObject item = items[i];
            var button1 = Instantiate(button,
                  new Vector3(transform.position.x + x_Start + (x_Space * (i % columnLength)), transform.position.y + y_Start + (-y_Space * (i / columnLength)), transform.position.z),
                        Quaternion.identity, transform);
            
            //Button icon
            button1.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh =
                item.gameObject.GetComponent<MeshFilter>().sharedMesh;
            button1.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material =
                item.gameObject.GetComponent<MeshRenderer>().sharedMaterial;

            button1.GetComponent<ButtonScript>().itemToSpawn = item;
        }
    }

}
