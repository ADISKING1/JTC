using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public AudioPlayer audioPlayer;

    public void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>();
    }
    public void SetMainMenu()
    {
        SceneManager.LoadScene(0);

        audioPlayer.SetMusicDefaults();
    }


}
