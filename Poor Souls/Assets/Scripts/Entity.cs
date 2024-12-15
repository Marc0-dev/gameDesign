using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health;
    public void takeDamage(float damageNumber){
        health -= damageNumber;
        if(isHealthDepleated() == true){
            die();
        }
    }
    private bool isHealthDepleated(){
        if(health <= 0)
            return true;
        else
            return false;
    }
    private void die(){
        //play animation
    }
}
