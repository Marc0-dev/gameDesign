using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using SharedNamespace;
public class CharacterUI : MonoBehaviour
{
    private Text health;
    private Text ammo;
    private Text death;
    private Image speedIcon;
    private Image damageIcon;
    private Player mainCharacter;
    public static float time = 0;
    private bool isTimerActive;
    private Text timer;
    private CanvasGroup pauseMenu; 
    void Start()
    {
        health = GameObject.Find("Health").GetComponent<Text>();
        ammo = GameObject.Find("Ammo").GetComponent<Text>();
        death = GameObject.Find("Death").GetComponent<Text>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
        speedIcon = GameObject.Find("Speed").GetComponent<Image>();
        damageIcon = GameObject.Find("Damage").GetComponent<Image>();
        pauseMenu = GameObject.Find("pauseMenu").GetComponent<CanvasGroup>();
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        speedIcon.enabled = false;
        damageIcon.enabled = false;
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        //save = SessionHolder.GetSessionHolder();
    }
    void Update()
    {
        if(isTimerActive == true){
            timer.text = SharedFunctions.FormatTimeSpan(TimeSpan.FromSeconds(time+=Time.deltaTime));
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePauseMenu();
        }
    }
    public void StartTimer(){isTimerActive = true;}
    public void StartSavedTimer(float time_){
        time = time_;
        isTimerActive = true;
        //time = save.time;
    }
    public float StopTimer(){
        isTimerActive = false;
        //save.time = time;
        return time;    
    }
    public float GetTimer(){
        return time;
    }
    public void ToggleSpeedIcon(bool visible){
        speedIcon.enabled = visible;
    }
    public void TogglePauseMenu(){
        if(pauseMenu.alpha == 0f){
            pauseMenu.alpha = 1f;
            pauseMenu.interactable = true;
            Time.timeScale = 0;
        }
        else if(pauseMenu.alpha == 1f){
            pauseMenu.alpha = 0f;
            pauseMenu.interactable = false;
            Time.timeScale = 1;
        }
    }
    public void CloseApp(){
        Application.Quit();
    }
    public void ToggleDamageIcon(bool visible){
        damageIcon.enabled = visible;
    }
    public IEnumerator UpdateUI(){
        while(true){
            health.text = mainCharacter.GetHealth().ToString();
            ammo.text = String.Concat(mainCharacter.GetBulletCount(), "|", mainCharacter.GetBulletCap());
            yield return null;
        }
    }
}
