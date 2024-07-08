using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    int _lives;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public void FirstLevel()
    {
        int level = 0;
        LoadLevel(level);
    }

    public void LastLevel()
    {
        int level = SceneManager.sceneCountInBuildSettings - 1;
        LoadLevel(level);
    }

    public void NextLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        if (level > SceneManager.sceneCountInBuildSettings - 1)
        {
            level = SceneManager.sceneCountInBuildSettings - 1;
        }
        LoadLevel(level);
    }

    public void PreviousLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex - 1;
        if (level < 0)
        {
            level = 0;
        }
        LoadLevel(level);
    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reload()
    {
        _lives = UIController.Instance.GetLives();
        if (_lives <= 0)
        {
            LastLevel();
            return;
        }

        StartCoroutine(ReloadCoroutine());


    }
    private IEnumerator ReloadCoroutine()
    {
        int livesBeforeReload = UIController.Instance.GetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Espera un pequeño tiempo para asegurar que la escena se ha cargado completamente
        yield return new WaitForSeconds(0.05F);


        while (livesBeforeReload >= _lives)
        {
            int livesInsideWhile = UIController.Instance.GetLives();
            while (livesInsideWhile > livesBeforeReload)
            {
                UIController.Instance.DecreaseLives();
                livesInsideWhile--;
            }
            UIController.Instance.DecreaseLives();
            livesBeforeReload--;
        }



    }

}