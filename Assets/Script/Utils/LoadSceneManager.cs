using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);  
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(SaveManager.Instance.lastLevel);
    }
}
