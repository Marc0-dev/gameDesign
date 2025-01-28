using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manta : Ostile
{
    Animator mantaAnimator;
    Player mainCharacter;
    public AudioSource mantaAudio;
    protected new float health = 50;
    Rigidbody body;
    public GameObject bullet;
    Transform mantaOrb;
    float attack = 10f;
    float speed = 0.3f;
    public override void Move(){
        Vector3 direction = (mainCharacter.transform.position - transform.position);
        direction.y = 0;
        mantaOrb.LookAt(mainCharacter.transform.position);
        if(Vector3.Distance(transform.position, mainCharacter.transform.position) > 20f){
            mantaAnimator.SetBool("attack", false);
            body.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
        else{
            body.velocity = Vector3.zero;
            mantaAnimator.SetBool("attack", true);
        }
        SmoothLookat(mainCharacter.transform);
    }
    public override void Attack(){
        GameObject firedBullet = null;
        firedBullet = Instantiate(bullet, mantaOrb.position, mantaOrb.rotation);
        Vector3 mantaScale = new Vector3(20, 20, 20);
        firedBullet.transform.localScale = mantaScale;
        mantaAudio.Play(0);
        firedBullet.GetComponent<Bullet>().MoveEnemy(attack, 50);
    }
    void Start()
    {
        mantaAnimator = this.gameObject.GetComponent<Animator>();
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        mantaOrb = this.gameObject.transform.Find("Armature/Bone/Bone.001/Bone.003/Bone.002/Bone.004/mantaOrb");
        mantaAudio = gameObject.GetComponent<AudioSource>();
        body = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Move();
    }
}
