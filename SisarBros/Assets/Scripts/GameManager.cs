using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using System.Text;

public enum GameState
{
    menu,
    inGame,
    deadGame,
    helpGame,
    scoreGame,
    endGame
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;
    public Canvas MenuCanvas;
    public Canvas gameCanvas;
    public Canvas endGame;
    public Canvas scoreGame;
    public Canvas helpGame;
    public Canvas deadGame;
    public GameObject mushroom;
    public GameObject cactus;
    public Text scores;
    public Text username;
    public Text maxScore;

    float currentTime = 0;
    float maxTimeMushroom = 5;
    float maxTimeCactus = 4;

    void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.menu);
        
        string scoresPath = ".\\Scores\\";
        string logsPath = ".\\Logs\\";

        if (!Directory.Exists(scoresPath))
        {
            DirectoryInfo di = Directory.CreateDirectory(scoresPath);
        }

        if (!Directory.Exists(logsPath))
        {
            DirectoryInfo di = Directory.CreateDirectory(logsPath);
        }

        string path = "Logs/logs.txt";

        string logText = Environment.MachineName + "/" + Environment.UserName + "/" + DateTime.Now + Environment.NewLine;

        File.AppendAllText(path, logText, Encoding.UTF8);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame)
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            BackToMenu();
        }*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

        if (mushroom == null)
        {
            currentTime += Time.deltaTime;
            
            if (currentTime >= maxTimeMushroom)
            {
                currentTime = 0;
                PlayerController.sharedInstance.runningSpeed = 5.0f;
                PlayerController.sharedInstance.animator.SetBool("isDimoni", false);
            }
        }

        if (cactus == null)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTimeCactus)
            {
                currentTime = 0;
                PlayerController.sharedInstance.jumpForce = 15.0f;
            }
        }
    }
    
    public void StartGame()
    {
        // Modo juego activado
        SetGameState(GameState.inGame);

        // Reinicializa las stats
        PlayerController.sharedInstance.scoreText.text = "Score 0";
        CountCoin.sharedInstance.coinsText.text = "Coins 0";

        // Reinicializa la camara
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        // Reinicializa el personaje
        PlayerController.sharedInstance.StartGame();
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void DeadGame()
    {
        SetGameState(GameState.deadGame);
    }

    public void HelpGame()
    {
        SetGameState(GameState.helpGame);
    }

    public void ScoreGame()
    {
        SetGameState(GameState.scoreGame);
    }

    public void EndGame()
    {
        SetGameState(GameState.endGame);
    }

    public void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            // lo que tenga que hacer en el menu
            MenuCanvas.enabled = true;
            gameCanvas.enabled = false;
            helpGame.enabled = false;
            scoreGame.enabled = false;
            deadGame.enabled = false;
            endGame.enabled = false;
        }
        if (newGameState == GameState.inGame)
        {
            // Lo que hace en estado juego
            // reiniciar Objetos
            PlayerController.sharedInstance.StartGame();

            MenuCanvas.enabled = false;
            gameCanvas.enabled = true;
            helpGame.enabled = false;
            scoreGame.enabled = false;
            deadGame.enabled = false;
            endGame.enabled = false;

        }
        if (newGameState == GameState.helpGame)
        {
            MenuCanvas.enabled = false;
            gameCanvas.enabled = false;
            helpGame.enabled = true;
            scoreGame.enabled = false;
            deadGame.enabled = false;
            endGame.enabled = false;

        }
        if (newGameState == GameState.scoreGame)
        {
            MenuCanvas.enabled = false;
            gameCanvas.enabled = false;
            helpGame.enabled = false;
            scoreGame.enabled = true;
            deadGame.enabled = false;
            endGame.enabled = false;

            // Establecemos los scores de esta manera y aqui 
            // porque no dejaba instanciarlo en otro lado.

            string path = "Scores/scores.txt";

            if (!File.Exists(path))
            {
                scores.text = "No hay scores aun";
            }
            else
            {
                string[] names = File.ReadAllLines(path);
                string score = "";

                for (int i = 0; i < names.Length; i++)
                {
                    score += i + 1 + " " + names[i] + "\n\n";
                }

                scores.text = score;
            }
        }
        if (newGameState == GameState.deadGame)
        {
            MenuCanvas.enabled = false;
            gameCanvas.enabled = false;
            helpGame.enabled = false;
            scoreGame.enabled = false;
            deadGame.enabled = true;
            endGame.enabled = false;
        }
        if (newGameState == GameState.endGame)
        {
            MenuCanvas.enabled = false;
            gameCanvas.enabled = false;
            helpGame.enabled = false;
            scoreGame.enabled = false;
            deadGame.enabled = false;
            endGame.enabled = true;

            // Guardamos datos del jugador
            string path = "Scores/scores.txt";

            string appendText = username.text + " " + maxScore.text + "\r";

            File.AppendAllText(path, appendText, Encoding.UTF8);

            PlayerController.sharedInstance.scoreText.text = "Score 0";
            CountCoin.sharedInstance.coinsText.text = "Coins 0";
        }

        this.currentGameState = newGameState;

    }

    public void ExitGame()
    {

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif


    }
}
