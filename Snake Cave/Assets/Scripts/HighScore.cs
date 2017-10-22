using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {
    public float score;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        score = PlayerPrefsManager.GetHighScore() ;
    }

    void Update()
    {
        text.text = score.ToString();
    }
}
