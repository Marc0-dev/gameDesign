using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveDataHolder
{
    public float time=0;
    public float health=100;
    public float positionX;
    public float positionY;
    public float positionZ;
    public int sceneId;
    public SaveDataHolder(float time_, float health_, int sceneId_, Vector3 position){
        time = time_;
        health = health_;
        sceneId = sceneId_;
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
    }
    public float GetTime(){
        return time;
    }
    public float GetHealth(){
        return health;
    }
    public int GetSceneId(){
        return sceneId;
    }
    public Vector3 GetPosition(){
        return new Vector3(positionX, positionY, positionZ);
    }
}
