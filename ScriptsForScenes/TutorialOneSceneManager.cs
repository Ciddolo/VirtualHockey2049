using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialOneSceneManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.StateOfGame == MatchState.EndGame)
            SceneManager.LoadScene("Tutorial2");
    }
}
