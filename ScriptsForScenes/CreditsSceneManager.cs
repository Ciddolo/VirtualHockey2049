using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneManager : MonoBehaviour
{
    public AudioSource ASource;
    public AudioClip ButtonPressedSound;
    public AudioClip ButtonHighlightedSound;

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ASource.PlayOneShot(ButtonPressedSound);
    }

    public void HighlightButton()
    {
        ASource.PlayOneShot(ButtonHighlightedSound);
    }
}