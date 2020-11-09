using UnityEngine;
using UnityEngine.SceneManagement;

public enum MatchState
{
    PreGame = 0,
    InGame = 1,
    EndGame = 2
}

public class GameManager : MonoBehaviour
{
    public static uint ScoreBlue, ScoreRed;
    public static float Seconds;
    public static int Minutes;
    public static MatchState StateOfGame;

    private SceneManager sceneManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        StateOfGame = MatchState.InGame;
    }

    private void Start()
    {
        InitGame();
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (StateOfGame == MatchState.InGame)
        {
            Seconds -= Time.deltaTime;

            if (Seconds < 0.0f)
            {
                Seconds = 60.0f;
                Minutes--;

                if (Minutes < 0)
                {
                    Minutes = 0;
                    Seconds = 0.0f;
                    StateOfGame = MatchState.EndGame;
                }
            }
        }
    }

    public static void InitGame()
    {
        ScoreBlue = 0;
        ScoreRed = 0;
        if (SceneManager.GetActiveScene().name == "Play")
        {
            Minutes = 3;
            Seconds = 0.0f;
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial1")
        {
            Minutes = 0;
            Seconds = 20.0f;
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial2")
        {
            Minutes = 0;
            Seconds = 20.0f;
        }

    }
}
