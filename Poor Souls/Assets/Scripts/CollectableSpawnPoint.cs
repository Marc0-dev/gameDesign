using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawnPoint : MonoBehaviour
{
    public GameObject powerUpPrefab;
    private GameObject powerUpInstance;
    bool replace = false;
    void Start()
    {
        powerUpInstance = Instantiate(powerUpPrefab, this.transform);
        StartCoroutine(ReplacePowerUp());
    }
    public void replaceTimerStart(){replace = true;}
    IEnumerator ReplacePowerUp(){
        while(true){
            if(replace == true){
                yield return new WaitForSeconds(20f);
                powerUpInstance = Instantiate(powerUpPrefab, this.transform);
                replace = false;
            }
            yield return null;
        }
    }
    void Update()
    {

    }
}
