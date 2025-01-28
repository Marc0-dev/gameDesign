using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreCaller : MonoBehaviour
{
    Text score;
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        ScoreBoardManager.LoadTime();
        score.text = ScoreBoardManager.DisplayTime();
    }


}
