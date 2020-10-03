using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    public AudioPlayer audioPlayer;

    public void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>();
    }
    public void PlayGame()
    {
        audioPlayer.SetMusicDefaults();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(2);
    }
    public void LoadCredits()
    {
        audioPlayer.SetMusicDefaults();
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        audioPlayer.SetMusicDefaults();
        Application.Quit();
    }
}
