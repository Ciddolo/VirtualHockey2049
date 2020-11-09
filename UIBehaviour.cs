using UnityEngine;
using TMPro;

public class UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI ScoreBlue, ScoreRed, Timer;
    public GameManager GM;

    private void Update()
    {
        if (GameManager.StateOfGame == MatchState.InGame)
            Timer.text = GameManager.Minutes.ToString("00") + ":" + ((int)GameManager.Seconds).ToString("00");
    }

    public void SetScoreBlue(uint score)
    {
        if (GameManager.StateOfGame == MatchState.InGame)
            ScoreBlue.text = score.ToString("00");
    }

    public void SetScoreRed(uint score)
    {
        if (GameManager.StateOfGame == MatchState.InGame)
            ScoreRed.text = score.ToString("00");
    }
}
