using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    // OBSOLUE
    public float time = 12f;

    private void Update() {
        time -= Time.deltaTime;
        if (time <= 0) GetComponent<GameManager>().GameOver();
    }
}