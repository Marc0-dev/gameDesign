using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ostile : Entity
{
    float lookSpeed = 2f;
    protected void SmoothLookat(Transform target){
        Quaternion relativeRotation = Quaternion.LookRotation(target.position-this.gameObject.transform.position);
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, relativeRotation, Time.deltaTime * lookSpeed);
    }
    public override void Die(){
        SingleWave.alive.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    public abstract void Move();
    public abstract void Attack();
}
