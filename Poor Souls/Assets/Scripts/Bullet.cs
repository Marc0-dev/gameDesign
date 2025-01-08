using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float speed = 100;
    private bool hasTarget;
    Vector3 hitPoint;
    Collider hitCollider;
    Collider thisCollider;
    private float damage;
    private Vector3 previousPosition;
    public void Move(Vector3 hitPoint_, Collider hitCollider_, float damage_){
        damage = damage_;
        hasTarget = true;
        hitPoint = hitPoint_;
        hitCollider = hitCollider_;
        Destroy(this.gameObject, 5);
    }
    public void Move(float damage_){
        damage = damage_;
        hasTarget = false;
        Destroy(this.gameObject, 5);
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Entity")){
            collision.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
        else{
            Debug.Log("hit");
        }
        Destroy(this.gameObject);
    }
    void Update()
    {
        if(hasTarget){
            transform.position = Vector3.MoveTowards(transform.position,hitPoint,Time.deltaTime*speed);
            if(transform.position == previousPosition){
                Destroy(this.gameObject);
            }
            previousPosition = transform.position;
        }
        else{
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }
}
