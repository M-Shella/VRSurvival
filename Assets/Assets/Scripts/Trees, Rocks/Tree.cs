using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour{
    public int hp = 10;
    private GameObject log;
    private GameObject[] sticks;
    private GameObject stick;
    private Vector3 position;
    void Start(){
        position = gameObject.transform.position;
        position.y += 2.1f;
        sticks = new GameObject[Random.Range(3,5)];
        log = Resources.Load<GameObject>("Prefabs/log");
        for (int i = 0; i < sticks.Length; i++) sticks[i] = Resources.Load<GameObject>("Prefabs/stick");
        stick = Resources.Load<GameObject>("Prefabs/stick");
    }
    
    void Update(){
        if (hp <= 0){
            Destroyed();
        }
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Axe")){
            hp -= 1;
            SpawnStick(stick);
        }
    }
    private void Destroyed(){
        var logs = Instantiate(log, position, Quaternion.Euler(90f, 0f, 0f));
        foreach (var sticki in sticks){
            SpawnStick(sticki);
        }
        logs.transform.parent = gameObject.transform.parent;
        Destroy(gameObject);
    }

    private void SpawnStick(GameObject sticki){
        var stickii = Instantiate(sticki, 
            new Vector3(position.x+Random.Range(-2,2), 
                position.y += 0.1f, 
                position.z+Random.Range(-2,2)), Quaternion.Euler(Random.Range(-180,180), 
                Random.Range(-180,180),
                Random.Range(-180,180)));
        stickii.transform.parent = gameObject.transform.parent;
    }
}
