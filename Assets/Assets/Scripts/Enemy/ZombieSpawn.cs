using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour {
    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform zombieParent;

    [SerializeField] private int numberOfZombiesToSpawnAtNight;
    [SerializeField] private int numberOfZombiesToSpawnAtDay;
    
    //range how far away from player to spawn zombie
    [SerializeField] private int rangeAtNight; 
    [SerializeField] private int rangeAtDay;
    
    [SerializeField] private int howOftenSpawnAtNight;
    [SerializeField] private int howOftenSpawnAtDay;
    
    [SerializeField] private int whenNightStart;
    [SerializeField] private int whenNightEnd;
    
    private bool delay;
    private int time;
    private int zombieCount;

    private void Start() {
        delay = true;
        zombieCount = 1;
        StartCoroutine(SpawnDelay());
        time = World.Instance.TimeOfDay;
        whenNightStart = 1320; //22
        whenNightEnd = 240; //4
        howOftenSpawnAtDay = 41;
        howOftenSpawnAtNight = 21;
        numberOfZombiesToSpawnAtDay = 4;
        numberOfZombiesToSpawnAtNight = 8;
        rangeAtNight = 20;
        rangeAtDay = 40;
    }

    private void Update() {
        time = World.Instance.TimeOfDay;
        
        if (time > World.Instance.dayEndTime || time <= World.Instance.dayStartTime) {
            if (time % howOftenSpawnAtDay != 0) return;
            if (!delay) SpawnZombieNearPlayer(rangeAtDay, Random.Range(numberOfZombiesToSpawnAtDay-2, numberOfZombiesToSpawnAtDay+2));
        }
        else {
            if (time % howOftenSpawnAtNight != 0) return;
            if (!delay) SpawnZombieNearPlayer(rangeAtNight, Random.Range(numberOfZombiesToSpawnAtNight-2, numberOfZombiesToSpawnAtNight+2));
        }
    }

    private void SpawnZombieNearPlayer(int range,int numberOfZombies) {
        var position = playerPosition.position;
        for (int i = 0; i < numberOfZombies; i++) {
            var spawnedZombie = Instantiate(zombie, new Vector3( position.x + Random.Range(-range, range),
                                                                                    position.y + 2,
                                                                                    position.z + Random.Range(-range, range)),
                Quaternion.identity, zombieParent);
            spawnedZombie.GetComponent<ZombieScript>().target = playerPosition;
            spawnedZombie.name = "Zombie " + zombieCount;
            zombieCount++;
        }
        delay = true;
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay() {
        yield return new WaitForSeconds(2);
        delay = false;
    }
}
