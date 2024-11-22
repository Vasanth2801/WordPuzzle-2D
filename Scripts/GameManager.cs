using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { Menu, Game, LevelComplete, GameOver, Idle }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Elements")]
    private GameState gameState;

    [Header("Events")]
    public static Action<GameState> onGameStateChanged;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextButtonCallback()
    {
        SetGameState(GameState.Game);
    }
}
