using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    Player mainCharacter;
    private struct Old{
        public float speed;
        public float damage;
    }
    private struct Boosted{
        public float speed;
        public float damage;
    }
    static Old oldValues;
    static Boosted boostedValues;
    public static bool activeSpeedBoost;
    public static bool activeDamageBoost;
    private float incrementPercent = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        oldValues.speed = mainCharacter.GetPlayerSpeed();
        oldValues.damage = mainCharacter.GetDamage();
        boostedValues.speed = oldValues.speed + oldValues.speed*incrementPercent;
        boostedValues.damage = oldValues.damage + oldValues.damage*incrementPercent;
    }
    public float getOldSpeed(){
        activeSpeedBoost = false;
        return oldValues.speed;
        }
    public float getNewSpeed(){
        activeSpeedBoost = true;
        return boostedValues.speed;
    }
    public float getOldDamage(){
        activeDamageBoost = false;
        return oldValues.damage;
    }
    public float getNewDamage(){
        activeDamageBoost = true;
        return boostedValues.damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
