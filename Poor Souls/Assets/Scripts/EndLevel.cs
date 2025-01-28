using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Collider>().enabled = false;
        if(SceneManager.GetActiveScene().buildIndex != 4){//cambiare con endscene
            SaveManager.NextLevel();
        }
        else{
            ScoreBoardManager.SaveTime();
            SceneManager.LoadScene(5);
        }
    }
}
