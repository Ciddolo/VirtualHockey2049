using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTwoSceneManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.StateOfGame == MatchState.EndGame)
            SceneManager.LoadScene("MainMenu");
    }
}
