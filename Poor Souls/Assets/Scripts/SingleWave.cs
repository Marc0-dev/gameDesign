using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;

public class SingleWave : MonoBehaviour
{
    Collider waveCollider;
    [SerializeField] int enemyCount;
    public static List<GameObject> alive;
    private GameObject []enemyPool;
    private Transform []spawnPoints;
    public GameObject manta;
    public GameObject worm;
    public GameObject bell;
    Fog fog1;
    Fog fog2;
    public bool CheckAliveOstiles(){
        if(alive.Count != 0)
            return true;
        else
            return false;
    }
    public void CollisionDetected(){
        StartCoroutine(StartWave());
    }
    void Start()
    {
        enemyPool = new GameObject[]{manta, worm, bell};
        spawnPoints = this.gameObject.transform.Find("Spawnpoints").GetComponentsInChildren<Transform>();
        fog1 = this.gameObject.transform.Find("frame").GetComponentInChildren<Fog>();
        fog2 = this.gameObject.transform.Find("frame 1").GetComponentInChildren<Fog>();
        alive = new List<GameObject>();
    }
    public IEnumerator StartWave(){
        yield return new WaitForSeconds(3f);
        fog1.thisCollider.isTrigger = false;
        fog1.thisRenderer.enabled = true;
        fog2.thisCollider.isTrigger = false;
        fog2.thisRenderer.enabled = true;
        for(int i = 0; i < enemyCount; i++){
            InstantiateOstile();
            yield return new WaitForSeconds(7f);
        }
        while(CheckAliveOstiles()){
            yield return null;
        }
        fog1.thisCollider.enabled = false;
        fog1.thisRenderer.enabled = false;
        fog2.thisCollider.enabled = false;
        fog2.thisRenderer.enabled = false;
    }
    private void InstantiateOstile(){
        UnityEngine.Random.InitState((int)System.DateTime.UtcNow.Ticks);
        int randomType = UnityEngine.Random.Range(0,3);
        GameObject spawned = Instantiate(enemyPool[randomType], spawnPoints[1].position, spawnPoints[1].rotation);
        alive.Add(spawned);
    }
}
