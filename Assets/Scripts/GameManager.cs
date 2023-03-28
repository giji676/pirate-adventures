using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour {
    public int maxCoins;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private string gameWon;
    [SerializeField] private string gameOver;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image timeImage;
    
    private float time;
    private bool gameFinished = false;
    private PlayerHealth playerHealth;
    private CoinCollide coinCollide;

    private void Start() {
        time = maxTime;
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        coinCollide = playerObject.GetComponent<CoinCollide>();
    }

    private void Update() {
        if (!playerHealth) return;
        if (!coinCollide) return;
        if (playerHealth.health <= 0) GameOver();        
        if (coinCollide.score >= maxCoins) GameWon();
        if (gameFinished) return;
        UpdateTimer();
    }

    private void UpdateTimer() {
        // Decrease the timer by the deltaTime
        time -= Time.deltaTime;
        if (time <= 0) GameOver();
        // Update the text and the image of the timer
        timeText.text = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
        timeImage.fillAmount = Mathf.InverseLerp(0, maxTime, time);
    }

    public void GameOver() {
        gameFinished = true;
        string newText = $"{gameOver} with {coinCollide.score} coins collected out of {maxCoins}";
        text.text = newText;
        StartCoroutine(MainMenu());
    }

    private void GameWon() {
        gameFinished = true;
        int timeLeft = (int)(maxTime-time);
        string newText = $"{gameWon} in {timeLeft} seconds";
        text.text = newText;
        StartCoroutine(MainMenu());
    }

    private IEnumerator MainMenu() {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");

    }
}