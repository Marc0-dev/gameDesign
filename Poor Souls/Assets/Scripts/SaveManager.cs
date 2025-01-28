using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.json";
    private static SaveDataHolder saveDataHolder;
    public static bool loadedScene = false;
    static Player mainCharacter;
    static CharacterUI playerInterface;
    public static bool LoadScene(){
        if(!File.Exists(path)){
            return false;
        }
        string jsonState = File.ReadAllText(path);
        saveDataHolder = JsonUtility.FromJson<SaveDataHolder>(jsonState);
        loadedScene = true;
        SceneManager.LoadScene(saveDataHolder.GetSceneId());
        return true;
    }
    public static void LoadData(){
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        playerInterface = GameObject.Find("UI Controller").GetComponent<CharacterUI>();
        if(loadedScene == false){
            playerInterface.StartTimer();
            return;
        }
        else{
            string jsonState = File.ReadAllText(path);
            saveDataHolder = JsonUtility.FromJson<SaveDataHolder>(jsonState);
            mainCharacter.SetHealth(saveDataHolder.GetHealth());
            mainCharacter.SetPosition(saveDataHolder.GetPosition());
            playerInterface.StartSavedTimer(saveDataHolder.GetTime());
            loadedScene = false;
        }
    }
    public static void NextLevel(){
        playerInterface = GameObject.Find("UI Controller").GetComponent<CharacterUI>();
        playerInterface.StopTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static void SaveData(){
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        playerInterface = GameObject.Find("UI Controller").GetComponent<CharacterUI>();
        saveDataHolder = new SaveDataHolder(playerInterface.GetTimer(), mainCharacter.GetHealth(), SceneManager.GetActiveScene().buildIndex, mainCharacter.GetPosition());
        string jsonInstance = JsonUtility.ToJson(saveDataHolder);
        Debug.Log(jsonInstance);
        if(!File.Exists(path)){
            File.Create(path);
        }
        File.WriteAllText(path, jsonInstance);
    }
}
