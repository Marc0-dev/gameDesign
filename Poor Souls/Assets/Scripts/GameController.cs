using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    Player mainCharacter;
    CharacterUI characterUI;
    public AudioMixer masterMixer;
    void Start()
    {
        mainCharacter = GameObject.Find("mainCharacter").GetComponent<Player>();
        characterUI = GameObject.Find("UI Controller").GetComponent<CharacterUI>();
        StartCoroutine(characterUI.UpdateUI());
        GetSavedVolume();
        SaveManager.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
            mainCharacter.Move();
    }
    void LateUpdate()
    {
        mainCharacter.HandleGuns();
    }
    public void GetSavedVolume(){
        if(PlayerPrefs.HasKey("masterVol")){
            masterMixer.SetFloat("masterVol", PlayerPrefs.GetFloat("masterVol"));
        }
        if(PlayerPrefs.HasKey("sfxVol")){
            masterMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
        }
        if(PlayerPrefs.HasKey("musicVol")){
            masterMixer.SetFloat("musicVol", PlayerPrefs.GetFloat("musicVol"));
        }
    }
}
