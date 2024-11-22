using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    private KeyBoardKey[] keys;

    void Awake()
    {
        keys = GetComponentsInChildren<KeyBoardKey>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Game:
                Initialize();
                break;

            case GameState.LevelComplete:
                break;
        }
    }

    void Initialize()
    {
        for(int i = 0; i < keys.Length; i++)
        {
            keys[i].Initialize();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        for(int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();

            for(int j = 0; j< wordToCheck.Length; j++)
            {
                if(keyLetter != wordToCheck[j])
                {
                    continue;
                }

                if(keyLetter == secretWord[j])
                {
                    //valid
                    keys[i].SetValid();
                }
                else if(secretWord.Contains(keyLetter))
                {
                    //potential
                    keys[i].SetPotential();
                }
                else
                {
                    //Invalid
                    keys[i].SetInvalid(); 
                }
            }
        }
    }
}
