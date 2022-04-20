using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieScript : MonoBehaviour {

    public bool run = false;

    private float speed = 0;
    private float targetSpeed = 0;
    public float animSpeed = 10;
    public int hp;
    private CharacterController characterController;
    private Animator animator;
    public Transform target;
    private static readonly int IdleType = Animator.StringToHash("IdleType");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Death = Animator.StringToHash("Death");
    
    public float TimeAlive;
    

    private Vector2 destination => new Vector2(target.position.x, target.position.z);

    private Vector2 posV2 => new Vector2(transform.position.x, transform.position.z);

    private void Awake() {
        hp = World.Instance.day * 10 + 100;
        TimeAlive = Time.time + 120;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetFloat(IdleType,  Random.Range(0f, 1f));
    }

    private void Update() {
        if (hp <= 0) {
            World.Instance.playerKills += 1;
            animator.SetBool(Death, true);
            GetComponent<CharacterController>().enabled = false;
            StartCoroutine(DeathDelay());
            return;
        }
        TimeAlive = Time.time;
        run = (World.Instance.TimeOfDay > World.Instance.dayEndTime || World.Instance.TimeOfDay <= World.Instance.dayStartTime);
        transform.LookAt(new Vector3(destination.x,transform.position.y,destination.y));

        if (Vector2.Distance(posV2, destination) > .8f && Vector2.Distance(posV2, destination) < 20) {
            targetSpeed = run ? 2f : 1f;
        }else if (Vector2.Distance(posV2, destination) <= .8f ) {
            targetSpeed = run ? 2f : 1f;
            animator.SetTrigger(Attack);
        } else {
            targetSpeed = 0;
        }
        
        speed = Mathf.SmoothStep(speed, targetSpeed, Time.deltaTime * animSpeed);
        animator.SetFloat(Speed, speed);
        if (Vector2.Distance(posV2, destination) < 20 && TimeAlive < Time.time) {
            Destroy(gameObject);
        }
    }
    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
