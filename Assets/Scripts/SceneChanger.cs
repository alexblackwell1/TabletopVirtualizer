using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Go to the Load Start Screen
    public void LoadNextScreen(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneLoad(Scene s) {
        SceneManager.LoadScene(s.name);
    }
}
