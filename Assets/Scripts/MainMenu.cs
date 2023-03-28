using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Level0() {
        SceneManager.LoadScene("Easy");
    }
    
    public void Level1() {
        SceneManager.LoadScene("Medium");
    }
    
    public void Level2() {
        SceneManager.LoadScene("Hard");
    }

    public void Quit() {
        Application.Quit();
    }
}