using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Text saveMessage;
    CanvasGroup settingsWindow;
    CanvasGroup tutorialWindow;
    public void Start(){
        saveMessage = GameObject.Find("No save").GetComponent<Text>();
        saveMessage.enabled = false;
        settingsWindow = GameObject.Find("Settings background").GetComponent<CanvasGroup>();
        settingsWindow.interactable = false;
        settingsWindow.alpha = 0f;
        tutorialWindow = GameObject.Find("Tutorial Window").GetComponent<CanvasGroup>();
        tutorialWindow.interactable = false;
        tutorialWindow.alpha = 0f;
    }
    public void NewGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGame(){
        if(SaveManager.LoadScene() == false){
            saveMessage.enabled = true;
        }
    }
    public void ToggleSettings(){
        if(settingsWindow.alpha == 0f){
            settingsWindow.interactable = true;
            settingsWindow.alpha = 1f;
        }
        else{
            settingsWindow.interactable = false;
            settingsWindow.alpha = 0f;
        }
    }
    public void ToggleTutorial(){
        if(tutorialWindow.alpha == 0f){
            tutorialWindow.interactable = true;
            tutorialWindow.alpha = 1f;
        }
        else{
            tutorialWindow.interactable = false;
            tutorialWindow.alpha = 0f;
        }
    }
    public void Quit(){
        Application.Quit();
    }
}
