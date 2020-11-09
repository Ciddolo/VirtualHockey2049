using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.StateOfGame == MatchState.EndGame)
            SceneManager.LoadScene("GameOver");
    }
}
