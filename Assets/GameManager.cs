using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public RectTransform[] Panels = new RectTransform[3];

    public static GameManager Instance { get; private set; }

    public enum GameModes { Playing, Paused, GameOver}
    public GameModes gameMode = GameModes.Playing;

    public GameObject[] canvas = new GameObject[3];

    public QuestionManager questionManager;
    public Menu menu;
    public AudioPlayer audioPlayer;

    public AudioClip PauseClip;

    public AudioClip Lose;
    public AudioClip Win;

    public int WinScore = 0;

    public Text EndQuote;

    public int NoOfOptions = 4;
    public int Mode = 0;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        menu = GameObject.FindGameObjectWithTag("Mode").GetComponent<Menu>();
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>();
        GameSetup();
    }

    public void GameSetup()
    {
        Mode = menu.GetMode();
        if (Mode == 0)
        {
            NoOfOptions = 2;
            WinScore = 15;
        }
        else
        {
            if (Mode == 1)
                WinScore = 9;
            else
                WinScore = 3;
            NoOfOptions = 4;
        }

        questionManager.enabled = true;
    }

    public void Replay()
    {
        audioPlayer.SetMusicDefaults();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        audioPlayer.SetMusicDefaults();
        SceneManager.LoadScene(0);
    }
    public void PlayGame()
    {
        audioPlayer.SetMusicDefaults();
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        audioPlayer.SetMusicDefaults();
        Application.Quit();
    }

    public void SetPlaying()
    {
        audioPlayer.SetMusicDefaults();
        Panels[0].DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.25f);
        Panels[1].DOAnchorPos(new Vector2(0, 1750), 0.25f);
        Panels[2].DOAnchorPos(new Vector2(3000, 0), 0.25f);

        gameMode = GameModes.Playing;

        audioPlayer.SetMusicVolume(.6f);
    }

    public void SetPaused()
    {
        audioPlayer.SetMusicDefaults();
        audioPlayer.SetMusicVolume(0.0f);
        questionManager.Confetti.Stop();
        
        Panels[0].DOAnchorPos(new Vector2(0, -1750), 0.25f);
        Panels[1].DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.25f);
        Panels[2].DOAnchorPos(new Vector2(3000, 0), 0.25f);

        gameMode = GameModes.Paused;
    }

    public void SetGameOver()
    {
        audioPlayer.SetMusicDefaults();
        questionManager.StopAllCoroutines();
        questionManager.Confetti.Stop();
        if (questionManager.Score >= WinScore)
        {
            questionManager.Confetti.Stop();
            audioPlayer.SetMusicPitch(1.25f);
            EndQuote.text = "Y O U\nD I D\nG R E A T !";
            EndQuote.color = questionManager.DefaultColour[2];
        }
        else
        {
            audioPlayer.SetMusicPitch(0.75f);
            audioPlayer.PlaySFX(Lose);
            EndQuote.text = "P L E A S E\nT R Y\nA G A I N !";
            EndQuote.color = questionManager.DefaultColour[4];
        }

        Panels[0].DOAnchorPos(new Vector2(0, -1750), 0.25f);
        Panels[1].DOAnchorPos(new Vector2(0, 1750), 0.25f);
        Panels[2].DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.25f);

        gameMode = GameModes.GameOver;
    }
}