using System;
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
    CollectableController controller;
    private CollectableSpawnPoint spawnPoint;
    private CharacterUI characterUI;
    public void Initialize(){
        powerUpRenderer = this.gameObject.GetComponent<Renderer>();
        powerUpColor = powerUpRenderer.material.color;
        UnityEngine.Random.InitState((int)Guid.NewGuid().GetHashCode());
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
        controller = GameObject.Find("Collectable Controller").GetComponent<CollectableController>();
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        characterUI = GameObject.Find("UI Controller").GetComponent<CharacterUI>();
        spawnPoint = gameObject.GetComponentInParent<CollectableSpawnPoint>();
        Initialize();
    }
    public void Interact(){
        StartCoroutine(ApplyPowerUp(choice));
    }
    IEnumerator ApplyPowerUp(PowerUp choice){
        Collider collectableCollider = this.gameObject.GetComponent<Collider>();
        collectableCollider.enabled = false;
        spawnPoint.replaceTimerStart();
        if(choice == PowerUp.speed && CollectableController.activeSpeedBoost == false){
            mainCharacter.SetPlayerSpeed(controller.getNewSpeed());
            powerUpRenderer.enabled = false;
            characterUI.ToggleSpeedIcon(true);
            yield return new WaitForSeconds(30);
            characterUI.ToggleSpeedIcon(false);
            mainCharacter.SetPlayerSpeed(controller.getOldSpeed());
        }
        else if(choice == PowerUp.damage && CollectableController.activeDamageBoost == false){
            mainCharacter.SetDamage(controller.getNewDamage());
            powerUpRenderer.enabled = false;
            characterUI.ToggleDamageIcon(true);
            yield return new WaitForSeconds(30);
            characterUI.ToggleDamageIcon(false);
            mainCharacter.SetDamage(controller.getOldDamage()); 
        }
        else if(choice == PowerUp.health){
            float currentHealth = mainCharacter.GetHealth();
            float boostedHealth = Mathf.Clamp(currentHealth + 25, 0, 100);//100 = maxhealth
            mainCharacter.SetHealth(boostedHealth);
        }
        Destroy(this.gameObject);
    }
}
