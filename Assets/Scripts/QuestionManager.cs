using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class QuestionManager : MonoBehaviour
{
    public enum Modes { Playing, NextQuestion, Gameover }


    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public ParticleSystem Confetti;

    [HideInInspector]
    public Color CorrectColour;
    [HideInInspector]
    public Color IncorrectColour;
    [HideInInspector]
    public Color[] DefaultColour = new Color[4];
    [HideInInspector]
    public Color Invisible;

    [Space]
    [HideInInspector]
    public int CurrentLevel = 0;
    [HideInInspector]
    public int Score = 0;
    [HideInInspector]
    public int DisplayScore = 0;
    [HideInInspector]
    public static int FHS = 0, QHS = 0, HHS = 0;
    [HideInInspector]
    public int Point = 1;
    [HideInInspector]
    public Text Best;
    [HideInInspector]
    public Text[] ScoreText;
    [HideInInspector]
    public Text[] LevelText;

    [HideInInspector]
    public AudioPlayer audioPlayer;

    public AudioClip Correct;
    public AudioClip Incorrect;

    [HideInInspector]
    public Modes currentMode = Modes.NextQuestion;
    [HideInInspector]
    public Question[] Questions;
    public Question[] Factarr;
    public Question[] Questionarr;
    public Question[] Hardarr;
    public List<Question> unansweredQuestions = new List<Question>();

    public Question currentQuestion;
    public string currentAnswer;
    [HideInInspector]
    public string playerAnswer;

    [HideInInspector]
    public GameObject[] TextObj = new GameObject[3]; // Point, Score, Level

    [HideInInspector]
    public Text Qtext;

    [HideInInspector]
    public GameObject[] OptionsGameObject = new GameObject[4];
    [HideInInspector]
    public Text[] OptionsText = new Text[4];
    [HideInInspector]
    public Button[] Options = new Button[4];
    [HideInInspector]
    public Image FactSprite;
    [HideInInspector]
    public Text Explaination;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>();
        audioPlayer.SetMusicDefaults();
        AudioPlayer.currentPitch = 1;

        Confetti.Pause();
        unansweredQuestions.Clear();

        if (gameManager.Mode == 0)
            Questions = Factarr;
        else if (gameManager.Mode == 1)
            Questions = Questionarr;
        else if (gameManager.Mode == 2)
            Questions = Hardarr;


        if (Questions.Length != 0 || unansweredQuestions == null)
            unansweredQuestions = Questions.ToList();

        SetQuestion();

        currentMode = Modes.Playing;
        gameManager.SetPlaying();
    }


    public void SetQuestion()
    {
        ClearOptions();
        CurrentLevel++;
        TextObj[2].transform.DOComplete();
        TextObj[2].transform.DOShakePosition(.2f, 4, 14, 90, false, true);
        if (unansweredQuestions.Count != 0)
        {
            int currentQuestionIndex = Random.Range(0, unansweredQuestions.Count);

            if (gameManager.Mode == 2)
                currentQuestionIndex = 0;

            currentQuestion = unansweredQuestions[currentQuestionIndex];
            currentAnswer = currentQuestion.A;
            
            if(gameManager.Mode == 0)
            {
                FactSprite.enabled = true;
                if (currentQuestion.I)
                    FactSprite.sprite = currentQuestion.I;
                FactSprite.enabled = false;
            }
            else
            {
                Explaination.enabled = true;
                if(currentQuestion.Explaination != null)
                    Explaination.text = currentQuestion.Explaination;
                Explaination.enabled = false;
            }

            unansweredQuestions.RemoveAt(currentQuestionIndex);

            Debug.Log(currentAnswer);

            SetUI();
            SetOptions();
        }
        else
        {
            currentMode = Modes.Gameover;
            Debug.Log("Game Over");
            gameManager.SetGameOver();
        }
    }

    public void SetOptions()
    {
        int correctOption = Random.Range(0, gameManager.NoOfOptions);
        for( int i = 0; i < gameManager.NoOfOptions; i++ )
        {
            if(gameManager.NoOfOptions == 4)
            {
                OptionsText[i].text = currentQuestion.ABCD[i];
            }
            else if (gameManager.NoOfOptions == 2)
            {
                    OptionsText[0].text = "Fact";
                    OptionsText[1].text = "Myth";
            }
            OptionsGameObject[i].SetActive(true);
            Options[i].interactable = true;
            OptionsGameObject[i].GetComponent<Image>().color = DefaultColour[i];
        }
        currentMode = Modes.Playing;
    }
    public void ClearOptions()
    {
        for (int i = 0; i < 4; i++)
        {
            OptionsGameObject[i].SetActive(false);
        }
    }


    public void ChooseA()
    {
        if(currentMode == Modes.Playing)
        {
            playerAnswer = OptionsText[0].text;
            DecisionPending(0);
            currentMode = Modes.NextQuestion;
            StartCoroutine(NextQuestion());
        }
    }
    public void ChooseB()
    {
        if (currentMode == Modes.Playing)
        {
            playerAnswer = OptionsText[1].text;
            DecisionPending(1);
            currentMode = Modes.NextQuestion;
            StartCoroutine(NextQuestion());
        }
    }
    public void ChooseC()
    {
        if (currentMode == Modes.Playing)
        {
            playerAnswer = OptionsText[2].text;
            DecisionPending(2);
            currentMode = Modes.NextQuestion;
            StartCoroutine(NextQuestion());
        }
    }
    public void ChooseD()
    {
        if (currentMode == Modes.Playing)
        {
            playerAnswer = OptionsText[3].text;
            DecisionPending(3);
            currentMode = Modes.NextQuestion;
            StartCoroutine(NextQuestion());
        }
    }

    public void DecisionPending(int Selected)
    {
        for (int i = 0; i < 4; i++)
        {
            if (OptionsText[i].text == currentAnswer)
                OptionsGameObject[i].GetComponent<Image>().color = CorrectColour;
            else
                OptionsGameObject[i].SetActive(false);
            if ((i == Selected) && (playerAnswer != currentAnswer))
            {
                OptionsGameObject[i].SetActive(true);
                OptionsGameObject[i].GetComponent<Image>().color = IncorrectColour;
            }
            Options[i].interactable = false;
        }
        if (FactSprite != null && gameManager.Mode == 0)
            FactSprite.enabled = true;
        if (Explaination != null && gameManager.Mode != 0)
        {
            Qtext.enabled = false;
            Explaination.enabled = true;

            TextObj[0].transform.DOComplete();
            TextObj[0].transform.DOShakePosition(.2f, 4, 14, 90, false, true);
        }
    }

    public IEnumerator NextQuestion()
    {
        if (playerAnswer == currentAnswer)
        {
            Debug.Log("Correct");
            Score += Point;
            TextObj[1].transform.DOComplete();
            TextObj[1].transform.DOShakePosition(.2f, 4, 14, 90, false, true);
            Confetti.Play();
            audioPlayer.PlayCorrectAudio(Correct);
        }
        else
        {
            audioPlayer.PlaySFX(Incorrect);
            AudioPlayer.currentPitch = 1f;
            Debug.Log("Wrong");
        }
        SetUI();

        //Color QtextTemp = Qtext.color;
        yield return StartCoroutine(WaitForKeyDown());
        yield return new WaitForSeconds(.25f);
        Confetti.Stop();
        if (gameManager.Mode == 0)
            FactSprite.enabled = false;
        else
        {
            Qtext.enabled = true;
            Explaination.enabled = false;
        }
        SetQuestion();
    }

    IEnumerator WaitForKeyDown()
    {
        while (!(Input.GetButtonDown("Fire1") || Input.touchCount > 1))
            yield return null;
    }

    public void SetUI()
    {
        Qtext.text = currentQuestion.Q;
        if (Score > GetHighScore())
            SetHighScore(Score);
        foreach (Text LT in  LevelText)
            LT.text = CurrentLevel.ToString() + " / " + Questions.Length.ToString();
        foreach (Text ST in ScoreText)
            ST.text = Score.ToString() + " / " + Questions.Length.ToString();
        Best.text = GetHighScore().ToString();
    }
    public int GetHighScore()
    {
        if (gameManager.Mode == 0)
            return FHS;
        else if (gameManager.Mode == 1)
            return QHS;
        else
            return HHS;
    }

    public void SetHighScore(int HS)
    {
        if (gameManager.Mode == 0)
            FHS = HS;
        if (gameManager.Mode == 1)
            QHS = HS;
        if (gameManager.Mode == 2)
            HHS = HS;
    }
}
