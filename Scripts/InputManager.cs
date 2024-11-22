using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyBoardColorizer;
    [Header("Settings")]
    private int currentWordContainerIndex;
    private bool canAddLetter = true;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        KeyBoardKey.onKeyPressed += KeyPressedCallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    void OnDestroy()
    {
        KeyBoardKey.onKeyPressed -= KeyPressedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
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
        currentWordContainerIndex = 0;
        canAddLetter = true;
        DisableTryButton();
        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
    }

    void KeyPressedCallback(char letter)
    {
        if(!canAddLetter)
        {
            return;
        }
        wordContainers[currentWordContainerIndex].Add(letter);

        if (wordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddLetter = false;
            EnableTryButton();
            //CheckWord();
            //currentWordContainerIndex++;
        }
    }

    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();
        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyBoardColorizer.Colorize(secretWord,wordToCheck);
        if(wordToCheck == secretWord)
        {
            SetLevelComplete();
        }
        else
        {
            Debug.Log("Wrong Word");
            canAddLetter = true;
            DisableTryButton();
            currentWordContainerIndex++;
        }
    }

    void SetLevelComplete()
    {
        UpdateData();
        GameManager.Instance.SetGameState(GameState.LevelComplete);
    }

    void UpdateData()
    {
        int scoreToAdd = 6 - currentWordContainerIndex;

        DataManager.instance.increaseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd * 3); 
    }

    public  void BackSpacePressedCallBack()
    {
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        if(removedLetter)
        {
            DisableTryButton();
        }
        
        canAddLetter = true;
    }

    void EnableTryButton()
    {
        tryButton.interactable = true;
    }

    void DisableTryButton()
    {
        tryButton.interactable = false;
    }
}
