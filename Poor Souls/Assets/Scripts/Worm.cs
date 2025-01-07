using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Ostile
{
    Animator wormAnimator;
    Player mainCharacter;
    Rigidbody body;
    float attack = 10f;
    float speed = 5f;
    public override void Move(){
        Vector3 direction = (mainCharacter.transform.position - transform.position).normalized;
        body.MovePosition(transform.position + direction * speed * Time.deltaTime);
        this.gameObject.transform.rotation = Quaternion.Euler(direction);
    }
    public override void Attack(){
        mainCharacter.TakeDamage(attack);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            wormAnimator.SetBool("attack", true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        wormAnimator.SetBool("attack", false);    
    }
    void Start()
    {
        wormAnimator = this.gameObject.GetComponent<Animator>();
        body = this.gameObject.GetComponent<Rigidbody>();
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
    }
    void FixedUpdate()
    {
        Move();
    }
}
