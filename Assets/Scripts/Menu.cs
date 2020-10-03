using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int Mode = 0;

    public static Menu Instance { get; private set; }
    public AudioPlayer audioPlayer;

    public void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void SetFactMode()
    {
        Mode = 0;
        SceneManager.LoadScene(1);
    }
    public void SetQuestionMode()
    {
        Mode = 1;
        SceneManager.LoadScene(1);
    }
    public void SetHardMode()
    {
        Mode = 2;
        SceneManager.LoadScene(1);
    }

    public int GetMode()
    {
        return Mode;
    }

    public void SetMainMenu()
    {
        audioPlayer.SetMusicDefaults();

        SceneManager.LoadScene(0);
    }
}
