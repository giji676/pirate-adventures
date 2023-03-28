using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollide : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] public AudioSource sound;
    public int score = 0;
    
    private void Start() {
        // Set the initial text
        scoreUI.text = $"Coins collected: {score}/{gameManager.maxCoins}";
    }
    
    private void OnTriggerEnter(Collider collider) {
        // If the player collides with the coin (check if the collision object is the same layer as coin)
        if (((1 << collider.gameObject.layer) & layerMask.value) != 0) {
            ScoreUpdate(1);
            Destroy(collider.gameObject);
            sound.Play();
        }
    }

    private void ScoreUpdate(int points) {
        score += points;
        scoreUI.text = $"Coins collected: {score}/{gameManager.maxCoins}";
    }
}