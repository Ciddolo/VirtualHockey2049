using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    public AudioSource ASource;
    public AudioClip ButtonPressedSound;
    public AudioClip ButtonHighlightedSound;

    public void Play()
    {
        SceneManager.LoadScene("Play");
        ASource.PlayOneShot(ButtonPressedSound);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial1");
        ASource.PlayOneShot(ButtonPressedSound);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        ASource.PlayOneShot(ButtonPressedSound);
    }

    public void Exit()
    {
        Application.Quit();
        ASource.PlayOneShot(ButtonPressedSound);
    }

    public void HighlightButton()
    {
        ASource.PlayOneShot(ButtonHighlightedSound);
    }
}
