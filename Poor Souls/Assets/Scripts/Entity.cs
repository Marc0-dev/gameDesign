using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health = 100;
    public virtual void TakeDamage(float damageNumber){
        health -= damageNumber;
        if(IsHealthDepleated() == true){
            Die();
        }
    }
    private bool IsHealthDepleated(){
        if(health <= 0)
            return true;
        else
            return false;
    }
    protected virtual void Die(){
        Destroy(this.gameObject);
    }
    /*
    IEnumerator Die(){
        Renderer objectRenderer = GetComponentInChildren<Renderer>();
        Color initColor = objectRenderer.material.color;
        Color targetColor = new Color(initColor.r, initColor.g, initColor.b, 0f);
        float elapsedTime = 0f;
        float fadeTime = 0.1f;
        Debug.Log("called");
        while(objectRenderer.material.color.a > 0f){
            elapsedTime += Time.deltaTime;
            objectRenderer.material.color = Color.Lerp(initColor, targetColor, elapsedTime);
            yield return null;
        }
        Destroy(this.gameObject);
    }
    */
}
