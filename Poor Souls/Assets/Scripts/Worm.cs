using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Ostile
{
    Animator wormAnimator;
    Player mainCharacter;
    GameObject bullseye;
    Rigidbody body;
    protected new float health = 30;
    public AudioSource hitSound;
    float attack = 10f;
    float speed = 5f;
    public override void Move(){
        if(wormAnimator.GetBool("moving")){
            Vector3 direction = (bullseye.transform.position - transform.position).normalized;
            body.MovePosition(transform.position + direction * speed * Time.deltaTime);
            SmoothLookat(bullseye.transform);
        }
        else{
            body.velocity = Vector3.zero;
        }
    }
    public override void Attack(){
        hitSound.Play();
        mainCharacter.TakeDamage(attack);
    }
    void OnTriggerEnter(Collider other)
    {
        wormAnimator.SetBool("moving", false);
        if(other.gameObject.CompareTag("Player")){
            wormAnimator.SetBool("attack", true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        wormAnimator.SetBool("moving", true);
        wormAnimator.SetBool("attack", false);    
    }
    void Start()
    {
        wormAnimator = this.gameObject.GetComponent<Animator>();
        wormAnimator.SetBool("moving", true);
        body = this.gameObject.GetComponent<Rigidbody>();
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        hitSound = this.gameObject.GetComponent<AudioSource>();
        bullseye = GameObject.Find("wormBullseye");
    }
    void FixedUpdate()
    {
        Move();
    }
}
