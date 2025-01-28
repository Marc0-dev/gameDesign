using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bell : Ostile
{
    NavMeshAgent agent;
    GameObject mainCharacter;
    Animator bellAnimator;
    Transform barrel;
    AudioSource shootNoise;
    public GameObject bullet;
    float attack = 10f;
    public override void Move(){
        agent.SetDestination(mainCharacter.transform.position);
        SmoothLookat(mainCharacter.transform);
        bellAnimator.SetBool("walking", true);
        bellAnimator.SetBool("attack", false);
        if(agent.remainingDistance < 30f){
            bellAnimator.SetBool("walking", false);
            bellAnimator.SetBool("attack", true);
        }
    }
    public override void Attack(){
        GameObject firedBullet = null;
        firedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        Vector3 bellScale = new Vector3(10, 10, 10);
        firedBullet.transform.localScale = bellScale;
        firedBullet.GetComponent<Bullet>().MoveEnemy(attack, 50);
    }
    void BellToll(){shootNoise.Play();}
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 10f;
        mainCharacter = GameObject.Find("mainCharacter");
        barrel = this.gameObject.transform.Find("metarig/spine.004/spine.004_end/barrel");
        bellAnimator = gameObject.GetComponent<Animator>();
        shootNoise = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        barrel.LookAt(mainCharacter.transform.position);
        Move();
    }
}
