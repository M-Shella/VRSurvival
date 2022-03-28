using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideingWIthSphere : MonoBehaviour{
    public Material red;
    private MeshRenderer _renderer;
    public GameObject[] corners;
     
    void Start(){
        _renderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag.Equals("Sphere")){
            foreach (var v in corners){
                Destroy(v);
            }
        }
        else{
            //_renderer.material = red;
            foreach (var v in corners){
                v.SetActive(true);
                v.GetComponent<MeshRenderer>().material = red;
            }

        }
        Destroy(gameObject);
    }
}
