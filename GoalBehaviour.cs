using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBehaviour : MonoBehaviour
{
    public UIBehaviour UI;
    public AudioSource ASource;
    public AudioClip Goal;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.tag == "Disc")
        {
            if (gameObject.tag == "GoalBlue")
                UI.SetScoreRed(++GameManager.ScoreRed);
            else if (gameObject.tag == "GoalRed")
                UI.SetScoreBlue(++GameManager.ScoreBlue);

            if (SceneManager.GetActiveScene().name == "Play")
                go.GetComponent<DiscBehaviour>().ResetDiscPosition();
            else
                go.GetComponent<DiscBehaviour>().ResetDiscPositionWithRandomVelocity();

            ASource.PlayOneShot(Goal);
        }
    }
}
