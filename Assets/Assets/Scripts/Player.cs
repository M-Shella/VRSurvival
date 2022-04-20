using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int hp;
    public GameObject redScreen;
    private bool delay;
    public Transform zombies;

    private void Start() {
        delay = true;
        hp = 100;
        World.Instance.playerKills = 0;
        World.Instance.playerHp = hp;
        StartCoroutine(SomeDelay());
    }

    private void Update() {
        
        // heal evey 3 sec
        if (!delay && hp < 100) {
            hp += 1;
            World.Instance.playerHp = hp;
            delay = true;
            StartCoroutine(SomeDelay());
        }
        
        if (hp <= 0) {
            Respawn();
        }
    }

    public void Damage(int damage) {
        hp -= damage;
        World.Instance.playerHp = hp;
        redScreen.SetActive(true);
        StartCoroutine(RedScreenDelay());
    }
    
    private void Respawn() {
        hp = 100;
        World.Instance.playerHp = hp;
        World.Instance.day = 1;
        World.Instance.TimeOfDay = 600;
        World.Instance.playerKills = 0;
        transform.position = new Vector3(0, 100, 0);
        foreach (Transform child in zombies) {
            Destroy(child.gameObject);
        }
    }
    
    private IEnumerator SomeDelay() {
        yield return new WaitForSeconds(3);
        delay = false;
    }
    private IEnumerator RedScreenDelay() {
        yield return new WaitForSeconds(2);
        redScreen.SetActive(false);
    }
}
