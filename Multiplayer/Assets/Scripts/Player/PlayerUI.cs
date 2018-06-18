using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    private Player player;
    private Canvas canvas;
    private Text score;
    private Slider slider;

    private void Start() {
        player = GetComponent<Player>();
        canvas = GetComponentInChildren<Canvas>();
        score = canvas.transform.GetChild(1).GetComponent<Text>();
        slider = GetComponentInChildren<Slider>();
        UpdateScore();
        UpdateHealth();
    }

    public void UpdateScore() {
        score.text = player.GetScore().ToString();
    }

    public void UpdateHealth() {
        slider.value = player.GetCurrentHealth();
    }
}
