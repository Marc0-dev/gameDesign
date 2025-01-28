using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : Prop, IInteractive
{
    private Text saveMessage;
    public IEnumerator SaveData(){
        saveMessage.enabled = true;
        SaveManager.SaveData();
        yield return new WaitForSeconds(2f);
        saveMessage.enabled = false;
    }
    void Start()
    {
        saveMessage = GameObject.Find("saveMessage").GetComponent<Text>();
        saveMessage.enabled = false;
    }
    public virtual void Interact(){StartCoroutine(SaveData());}
}
