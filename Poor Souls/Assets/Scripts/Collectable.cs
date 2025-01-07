using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Prop, IInteractive
{
    enum PowerUp{speed, damage, health}
    private PowerUp choice;
    private Renderer powerUpRenderer;
    private Color powerUpColor;
    Player mainCharacter;
    private float incrementPercent = 0.5f;
    private struct Old{
        public float speed;
        public float damage;
    }
    private struct Boosted{
        public float speed;
        public float damage;
    }
    Old oldValues;
    Boosted boostedValues;
    public void Initialize(){
        powerUpRenderer = this.gameObject.GetComponent<Renderer>();
        powerUpColor = powerUpRenderer.material.color;
        Random.InitState((int)System.DateTime.UtcNow.Ticks);
        int randomType = UnityEngine.Random.Range(0,3);
        switch(randomType){
            case 0:
                choice = PowerUp.speed;
                powerUpColor.r = 0f;
                powerUpColor.g = 0f;
                powerUpColor.b = 1f;
                break;
            case 1:
                choice = PowerUp.damage;
                powerUpColor.r = 1f;
                powerUpColor.g = 0f;
                powerUpColor.b = 0f;
                break;
            case 2:
                choice = PowerUp.health;
                powerUpColor.r = 0f;
                powerUpColor.g = 1f;
                powerUpColor.b = 0f;
                break;
        }
        powerUpRenderer.material.color = powerUpColor;
    }
    void Start()
    {
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        oldValues = new Old();
        boostedValues = new Boosted();
        oldValues.speed = mainCharacter.GetComponent<Player>().GetPlayerSpeed();
        oldValues.damage = mainCharacter.GetComponent<Player>().GetDamage();
        boostedValues.speed = oldValues.speed + oldValues.speed*incrementPercent;
        boostedValues.damage = oldValues.damage + oldValues.damage*incrementPercent;
        Initialize();
    }
    public void Interact(){
        StartCoroutine(ApplyPowerUp(choice));
    }
    IEnumerator ApplyPowerUp(PowerUp choice){
        Collider collectableCollider = this.gameObject.GetComponent<Collider>();
        collectableCollider.enabled = false;
        if(choice == PowerUp.speed){
            mainCharacter.SetPlayerSpeed(boostedValues.speed);
            powerUpRenderer.enabled = false;
            yield return new WaitForSeconds(30);
            mainCharacter.SetPlayerSpeed(oldValues.speed);
        }
        else if(choice == PowerUp.damage){
            mainCharacter.SetDamage(boostedValues.damage);
            powerUpRenderer.enabled = false;
            yield return new WaitForSeconds(30);
            mainCharacter.SetDamage(oldValues.damage); 
        }
        else if(choice == PowerUp.health){
            float currentHealth = mainCharacter.GetHealth();
            float boostedHealth = Mathf.Clamp(currentHealth + 25, 0, 100);//100 = maxhealth
            mainCharacter.SetHealth(boostedHealth);
        }
        Destroy(this.gameObject);
    }
}
