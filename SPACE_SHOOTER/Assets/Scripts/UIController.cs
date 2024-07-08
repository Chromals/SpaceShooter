using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTxt;

    [SerializeField]
    Transform livesContainer;

    bool hasLives = true;
    int lives = 0;

    private static UIController _instance;


    private void Awake()
    {
        //implements singleton (instance) to invoke  IncreaseScore -> 10pts -check
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;


    }


    public static UIController Instance
    {
        get { return _instance; }
    }

    //https://www.dafont.com/
    //find a font and apply it to the textmeshpro controls -> 10pts - check


    public void IncreaseScore(float points)
    {
        float score = float.Parse(scoreTxt.text);

        score += points;
        scoreTxt.text = score.ToString();
    }


    public void DecreaseLives()
    {
        int maxLiveNumer = 0;
        Image[] liveImgs = livesContainer.GetComponentsInChildren<Image>();
        Image maxLiveImg = null;

        foreach (Image image in liveImgs)
        {
            if (image.name.StartsWith("Life_") && image.enabled)
            {
                int liveNumber = int.Parse(image.name.Remove(0, 5)) - 1;
                if (maxLiveNumer == 0 || liveNumber > maxLiveNumer)
                {
                    maxLiveNumer = liveNumber;
                    maxLiveImg = image;
                }
            }
        }

        if (maxLiveImg != null)
        {
            maxLiveImg.enabled = false;
        }

        hasLives = maxLiveNumer > 0;
    }

    public bool HasLives()
    {
        return hasLives;
    }

    public int GetLives()
    {
        int enabledCount = 0;

        Image[] liveImgs = livesContainer.GetComponentsInChildren<Image>();
        foreach (Image image in liveImgs)
        {
            if (image.enabled)
            {
                enabledCount++;
            }
        }

        return enabledCount;
    }


}