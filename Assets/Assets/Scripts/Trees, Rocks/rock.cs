    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour{
    public int hp = 10;
    private GameObject[] smallRocks;
    private GameObject smallRock;
    private Vector3 position;
    void Start(){
        position = gameObject.transform.position;
        smallRock = Resources.Load<GameObject>("Prefabs/smallStone");
        smallRocks = new GameObject[Random.Range(5,9)];
        for (int i = 0; i < smallRocks.Length; i++) smallRocks[i] = Resources.Load<GameObject>("Prefabs/smallStone");
    }
    
    void Update(){
        if (hp <= 0){
            Destroyed();
        }
    }
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Pickaxe")){
            hp -= 1;
            var smallRocka = Instantiate(smallRock, new Vector3(position.x,position.y+1.2f,position.z), Quaternion.identity);
            var rockRB = smallRocka.GetComponent<Rigidbody>();
            rockRB.AddForce(Random.Range(-4,4), .5f, Random.Range(-4,4), ForceMode.Impulse);
        }
    }

    private void Destroyed(){
        foreach (var rocks in smallRocks){
            var rocksi = Instantiate(rocks, 
                new Vector3(position.x+Random.Range(-1.5f,1.5f), 
                    position.y += Random.Range(0f,.5f), 
                    position.z+Random.Range(-1.5f,1.5f)), Quaternion.Euler(Random.Range(-180,180), 
                    Random.Range(-180,180),
                    Random.Range(-180,180)));
            rocksi.transform.parent = gameObject.transform.parent;
        }
        Destroy(gameObject);
    }
}
