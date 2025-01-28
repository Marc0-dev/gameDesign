using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GameEnd());
    }
    IEnumerator GameEnd(){
        yield return new WaitForSeconds(10f);
        ScoreBoardManager.SaveToFile();
        SceneManager.LoadScene(0);
    }
}
