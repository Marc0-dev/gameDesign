using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using SharedNamespace;
using System;

public class Settings : MonoBehaviour
{
    public PostProcessVolume volume;
    private Bloom bloom = null;
    private DepthOfField dof = null;
    private AmbientOcclusion ao = null;
    private MotionBlur motionBlur = null;
    private ChromaticAberration ca = null;
    private Grain grain = null;
    Toggle bloomToggle;
    Toggle dofToggle;
    Toggle aoToggle;
    Toggle motionBlurToggle;
    Toggle caToggle;
    Toggle grainToggle;
    Slider master;
    Slider sfx;
    Slider music;
    Dropdown resolution;
    public AudioMixer masterMixer;
    CanvasGroup settingsWindow;
    void Start()
    {
        volume.sharedProfile.TryGetSettings(out bloom);
        volume.sharedProfile.TryGetSettings(out dof);
        volume.sharedProfile.TryGetSettings(out ao);
        volume.sharedProfile.TryGetSettings(out motionBlur);
        volume.sharedProfile.TryGetSettings(out ca);
        volume.sharedProfile.TryGetSettings(out grain);
        bloomToggle = GameObject.Find("Bloom Toggle").GetComponent<Toggle>();
        dofToggle = GameObject.Find("DOF Toggle").GetComponent<Toggle>();
        aoToggle = GameObject.Find("AO Toggle").GetComponent<Toggle>();
        motionBlurToggle = GameObject.Find("MB Toggle").GetComponent<Toggle>();
        caToggle = GameObject.Find("CA Toggle").GetComponent<Toggle>();
        grainToggle = GameObject.Find("Grain Toggle").GetComponent<Toggle>();
        master = GameObject.Find("Master Volume").GetComponent<Slider>();
        sfx = GameObject.Find("SFX Volume").GetComponent<Slider>();
        music = GameObject.Find("Music Volume").GetComponent<Slider>();
        resolution = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        settingsWindow = this.GetComponent<CanvasGroup>();
        Load();
    }
    public void ToggleBloom(){
        bloom.enabled.value = bloomToggle.isOn;
    }
    public void ToggleDof(){
        dof.enabled.value = dofToggle.isOn;
    }
    public void ToggleAo(){
        ao.enabled.value = aoToggle.isOn;
    }
    public void ToggleMotionBlur(){
        motionBlur.enabled.value = motionBlurToggle.isOn;
    }
    public void ToggleCa(){
        ca.enabled.value = caToggle.isOn;
    }
    public void ToggleGrain(){
        grain.enabled.value = grainToggle.isOn;
    }
    public void setMasterVolume(){
        masterMixer.SetFloat("masterVol", master.value);
    }
    public void setMusicVolume(){
        masterMixer.SetFloat("musicVol", music.value);
    }
    public void setSFXVolume(){
        masterMixer.SetFloat("sfxVol", sfx.value);
    }
    public void handleRes(){
        switch(resolution.value){
            case 0:
                Screen.SetResolution(2560, 1440, true);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 2:
                Screen.SetResolution(1366, 768, true);
                break;
            case 3:
                Screen.SetResolution(1280, 720, true);
                break;
        }
    }
    public void Save(){
        float tempMaster, tempSfx, tempMusic;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("bloom", SharedFunctions.BoolToInt(bloom.enabled.value));
        PlayerPrefs.SetInt("AO", SharedFunctions.BoolToInt(ao.enabled.value));
        PlayerPrefs.SetInt("DOF", SharedFunctions.BoolToInt(dof.enabled.value));
        PlayerPrefs.SetInt("MB", SharedFunctions.BoolToInt(motionBlur.enabled.value));
        PlayerPrefs.SetInt("grain", SharedFunctions.BoolToInt(grain.enabled.value));
        PlayerPrefs.SetInt("CA", SharedFunctions.BoolToInt(ca.enabled.value));
        masterMixer.GetFloat("masterVol", out tempMaster);
        PlayerPrefs.SetFloat("masterVol", tempMaster);
        masterMixer.GetFloat("sfxVol", out tempSfx);
        PlayerPrefs.SetFloat("sfxVol", tempSfx);
        masterMixer.GetFloat("musicVol", out tempMusic);
        PlayerPrefs.SetFloat("musicVol", tempMusic);
        PlayerPrefs.SetInt("resolution", resolution.value);
        PlayerPrefs.Save();
        settingsWindow.interactable = false;
        settingsWindow.alpha = 0f;
    }
    public void Load(){
        if(PlayerPrefs.HasKey("bloom")){
            bloomToggle.isOn = SharedFunctions.IntToBool(PlayerPrefs.GetInt("bloom"));
            bloom.enabled.value = SharedFunctions.IntToBool(PlayerPrefs.GetInt("bloom"));
        }
        if(PlayerPrefs.HasKey("AO")){
            aoToggle.isOn = SharedFunctions.IntToBool(PlayerPrefs.GetInt("AO"));
            ao.enabled.value = SharedFunctions.IntToBool(PlayerPrefs.GetInt("AO"));
        }
        if(PlayerPrefs.HasKey("DOF")){
            dofToggle.isOn = SharedFunctions.IntToBool(PlayerPrefs.GetInt("DOF"));
            dof.enabled.value = SharedFunctions.IntToBool(PlayerPrefs.GetInt("DOF"));
        }
        if(PlayerPrefs.HasKey("MB")){
            motionBlurToggle.isOn = SharedFunctions.IntToBool(PlayerPrefs.GetInt("MB"));
            motionBlur.enabled.value = SharedFunctions.IntToBool(PlayerPrefs.GetInt("MB"));
        }
        if(PlayerPrefs.HasKey("grain")){
            grainToggle.isOn = SharedFunctions.IntToBool(PlayerPrefs.GetInt("grain"));
            grain.enabled.value = SharedFunctions.IntToBool(PlayerPrefs.GetInt("grain"));
        }
        if(PlayerPrefs.HasKey("CA")){
            caToggle.isOn = SharedFunctions.IntToBool(PlayerPrefs.GetInt("CA"));
            ca.enabled.value = SharedFunctions.IntToBool(PlayerPrefs.GetInt("CA"));
        }
        if(PlayerPrefs.HasKey("masterVol")){
            master.value = PlayerPrefs.GetFloat("masterVol");
        }
        if(PlayerPrefs.HasKey("sfxVol")){
            sfx.value = PlayerPrefs.GetFloat("sfxVol");
        }
        if(PlayerPrefs.HasKey("musicVol")){
            music.value = PlayerPrefs.GetFloat("musicVol");
        }
        if(PlayerPrefs.HasKey("resolution")){
            resolution.value = PlayerPrefs.GetInt("resolution");
        }
    }
}
