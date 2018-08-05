using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Player playerScript;
    private Canvas playerCanvas;
    private Text scoreDisplay;
    private Text waitingText;
    private Text wreckedText;
    private Slider healthSlider;

    private void Start()
    {
        playerScript = GetComponent<Player>();
        playerCanvas = GetComponentInChildren<Canvas>();
        scoreDisplay = playerCanvas.transform.GetChild(1).GetComponent<Text>();
        waitingText = playerCanvas.transform.GetChild(3).GetComponent<Text>();
        wreckedText = playerCanvas.transform.GetChild(4).GetComponent<Text>();
        healthSlider = GetComponentInChildren<Slider>();


        UpdateScore();
        UpdateHealth();
    }

    public void UpdateScore()
    {
        scoreDisplay.text = playerScript.GetScore().ToString();
    }

    public void UpdateHealth()
    {
        healthSlider.value = playerScript.GetCurrentHealth() / 100;
    }

    public void ShowWaitingText(bool isActive)
    {
        waitingText.gameObject.SetActive(isActive);
    }

    public IEnumerator ShowWreckedText()
    {
        wreckedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        wreckedText.gameObject.SetActive(false);
        yield return null;
    }
}
