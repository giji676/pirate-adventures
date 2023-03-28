using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth;
    public int health;
    private float durationTimer;

    [Header("Damage Overlay")]
    [SerializeField] private Image overlay;
    [SerializeField] private Image healthBar;
    [SerializeField] private float duration;
    [SerializeField] private float fadeSpeed;


    private void Start() {
        health = maxHealth;
    }
    
    private void Update() {
        health = Mathf.Clamp(health, 0, maxHealth); // Make sure health doesn't go out of the bounds (0, maxHealth)
        if (overlay.color.a > 0) { // If the blood effect overlay alpha value (transparency) is over 0
            if (health < 30) // If health is low don't change the alpha value
                return;

            // Slowly decrease the alpha value so the effect fades out
            durationTimer += Time.deltaTime;
            if (durationTimer > duration) {
                float tempAplha = overlay.color.a;
                tempAplha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAplha);
            }
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;

        // Set the damage effect alpa to 50% so it shows up on the screen
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.5f);
        // Decrease the health bar to the new health;
        healthBar.fillAmount = Mathf.InverseLerp(0, maxHealth, health);
    }
}