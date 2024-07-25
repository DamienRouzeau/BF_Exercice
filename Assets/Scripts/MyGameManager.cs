using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;
    public GameObject deathScreen;
    public TMPro.TextMeshProUGUI meterCounter;
    public float timeInGame;

    public bool gameIsFinish = false;

    public void EndGame()
    {
        deathScreen.SetActive(true);
        gameIsFinish = true;
    }

    public void Update()
    {
        timeInGame += Time.fixedDeltaTime;
        int _timeInGameTruncate = (int)timeInGame;
        if (!gameIsFinish) meterCounter.text = _timeInGameTruncate.ToString();
    }
}
