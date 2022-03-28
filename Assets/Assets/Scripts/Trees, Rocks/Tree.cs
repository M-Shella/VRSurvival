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
    private GameObject stump;
    private Vector3 position;
    void Start(){
        position = gameObject.transform.position;
        position.y += 2.1f;
        sticks = new GameObject[Random.Range(3,5)];
        log = Resources.Load<GameObject>("Prefabs/log");
        stump = Resources.Load<GameObject>("Prefabs/Stump");
        for (int i = 0; i < sticks.Length; i++) sticks[i] = Resources.Load<GameObject>("Prefabs/stick");
        stick = Resources.Load<GameObject>("Prefabs/stick");
    }
    
    void Update(){
        if (hp <= 0){
            Destroyed();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Axe")) return;
        hp -= 1;
        SpawnStick(stick);
    }
    private void Destroyed() {
        var logPosition = new Vector3(position.x, position.y + 0.2f, position.z);
        var stumpPosition = new Vector3(position.x, position.y - 2.7f, position.z);
        var logs = Instantiate(log, logPosition, Quaternion.Euler(90f, 0f, 0f));
        var stumps = Instantiate(stump, stumpPosition, Quaternion.identity);
        
        foreach (var sticki in sticks){
            SpawnStick(sticki);
        }

        var o = gameObject;
        var parent = o.transform.parent;
        
        logs.transform.parent = parent;
        stumps.transform.parent = parent;
        Destroy(o);
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
