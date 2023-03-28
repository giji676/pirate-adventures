using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCollide : MonoBehaviour {
    public LayerMask layerMask;
    private bool isTakingDamage = false;
    private bool colliding = false;
    private void OnTriggerEnter(Collider collider) {
        // If the player collides with the sea (check if the collision object is the same layer as sea)
        if (((1 << collider.gameObject.layer) & layerMask.value) != 0 && !colliding) {
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        // If the player collides with the sea (check if the collision object is the same layer as sea)
        if (((1 << collider.gameObject.layer) & layerMask.value) != 0) {
            colliding = false;
        }
    }

    private void Update() {
        if (colliding && !isTakingDamage) { // If the player is still colliding with sea and isn't already taking damage
            StartTakeDamageFunction(10, 2); // Start taking damage
        }
    }
    
    void StartTakeDamageFunction(int damage, float delay) {
        StartCoroutine(TakeDamage(10, 2));
    }

    private IEnumerator TakeDamage(int damage, float delay) {
        // Damage every (delay) seconds
        isTakingDamage = true;
        GetComponentInParent<PlayerHealth>().TakeDamage(10);
        yield return new WaitForSeconds(delay);
        isTakingDamage = false;
    }
}