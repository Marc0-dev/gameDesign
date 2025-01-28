using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SharedNamespace;
public static class ScoreBoardManager
{
    public static ScoreList scoreBoard = new ScoreList();
    private static string path = Application.persistentDataPath + "/scoreBoard.json";
    public static bool empty = true;
    public static int maxScores = 3;
    public static void LoadTime(){
        if(File.Exists(path) == false){
            empty = true;
            return;
        }
        empty = false;
        string jsonInstance = File.ReadAllText(path);
        scoreBoard = JsonUtility.FromJson<ScoreList>(jsonInstance);
    }
    public static string DisplayTime(){
        if (empty == true){
            return "Empty File";
        }
        else{
            string result = "";
            foreach(float i in scoreBoard.scoreHolder){
                result += (SharedFunctions.FormatTimeSpan(TimeSpan.FromSeconds(i))) + "\n";
            }
            return result;
        }
    }
    public static void SaveTime(){
        scoreBoard.scoreHolder.Add(CharacterUI.time);
        Debug.Log(CharacterUI.time + "valore in struttura dati");
        scoreBoard.scoreHolder.Sort();
        if(scoreBoard.scoreHolder.Count > maxScores){
            scoreBoard.scoreHolder.RemoveAt(scoreBoard.scoreHolder.Count-1);
        }
    }
    public static void SaveToFile(){
        if(File.Exists(path) == false){
            File.Create(path);
        }
        string jsonInstance = JsonUtility.ToJson(scoreBoard);
        File.WriteAllText(path, jsonInstance);
        empty = false;
    }

}
