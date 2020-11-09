using UnityEngine;
using TMPro;

public class GameOverSceneUI : MonoBehaviour
{
    public TextMeshProUGUI BlueScore;
    public TextMeshProUGUI RedScore;

    private void Awake()
    {
        BlueScore.text = GameManager.ScoreBlue.ToString();
        RedScore.text = GameManager.ScoreRed.ToString();
    }
}
