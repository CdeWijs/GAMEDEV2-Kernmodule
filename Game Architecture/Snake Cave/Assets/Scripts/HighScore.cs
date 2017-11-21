using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {
    public float score;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        score = PlayerPrefsManager.GetHighScore() ;
    }

    private void Update()
    {
        text.text = score.ToString();
    }
}
