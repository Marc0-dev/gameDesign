using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Ostile
{
    Animator wormAnimator;
    Player mainCharacter;
    GameObject bullseye;
    Rigidbody body;
    float attack = 10f;
    float speed = 5f;
    float lookSpeed = 2f;
    public override void Move(){
        if(wormAnimator.GetBool("moving")){
            Vector3 direction = (bullseye.transform.position - transform.position).normalized;
            body.MovePosition(transform.position + direction * speed * Time.deltaTime);
            Quaternion relativeRotation = Quaternion.LookRotation(bullseye.transform.position-this.gameObject.transform.position);
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, relativeRotation, Time.deltaTime * lookSpeed);
        }
        else{
            body.velocity = Vector3.zero;
        }
    }
    public override void Attack(){
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
        bullseye = GameObject.Find("wormBullseye");
    }
    void FixedUpdate()
    {
        Move();
    }
}
