using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour {

	public ManagerRacing managerRacing;

	public Text ScoreText;
	public Text TimerText;
    public Text TokenText;

	public Image Life1Image;
	public Image Life2Image;
	public Image Life3Image;

	public Sprite LifeFillSprite;
	public Sprite LifeNullSprite;

	public GameObject GameStartedUp;
	public GameObject GameplayGUIGameObject;
	public GameObject CountDownGameObject;
	public Text CountDownText;

	public AudioHandler audioHandler;

    [Header("Game Timer Result")]
    public Image TimerBarOutline;



    [Header("Game Finish Result")]
	public GameObject GameFinishUI;
	public Text NameText;
	public Text FinalScoreText;
	public Text CoinText;

    [Header("Game Over Result")]
    public GameObject GameOverUI;
    public Text NameGOText;
    public Text FinalScoreGOText;
    public Text CoinGOText;

    public RetreavingData RetreavingData;

	void Start()
	{
		managerRacing.CallbackGameOver += OnGameOver;
        managerRacing.CallbackGameFinish += OnGameFinish;
        managerRacing.CallbackTokenScore += OnPlayerToken;
        managerRacing.CallBackGameTimer += OnGameTimer;
		managerRacing.CallbackPlayerLife += OnPlayerLife;
		managerRacing.CallbackPlayerScore += OnPlayerScore;
        StartGameplay();

    }

	public void InitScore(float Point)
	{
		ScoreText.text = String.Format("{0:n0}", Point);
	}

    public void InitToken(float Point)
    {
        TokenText.text = String.Format("{0:n0}", Point);
    }

    public void InitTimer(int time)
	{
        float timeBarCount=0f;
		TimeSpan ts = TimeSpan.FromSeconds(time);
		TimerText.text = ts.ToString();
        timeBarCount = (float)(((float)time/60f));
        Debug.Log(timeBarCount.ToString());
        TimerBarOutline.fillAmount = timeBarCount;

    }

	public void InitLife(int life)
	{
		switch (life)
		{
			case 0:
				Life1Image.sprite = LifeNullSprite;
				Life2Image.sprite = LifeNullSprite;
				Life3Image.sprite = LifeNullSprite;
                Life1Image.fillAmount = 1f;
                Life2Image.fillAmount = 1f;
                Life3Image.fillAmount = 1f;
                break;
			case 1:
				Life1Image.sprite = LifeFillSprite;
				Life2Image.sprite = LifeNullSprite;
				Life3Image.sprite = LifeNullSprite;
                Life1Image.fillAmount = 0.5f;
                Life2Image.fillAmount = 1f;
                Life3Image.fillAmount = 1f;
                break;
			case 2:
				Life1Image.sprite = LifeFillSprite;
				Life2Image.sprite = LifeNullSprite;
				Life3Image.sprite = LifeNullSprite;
                Life1Image.fillAmount = 1f;
                Life2Image.fillAmount = 1f;
                Life3Image.fillAmount = 1f;
                break;
			case 3:
				Life1Image.sprite = LifeFillSprite;
				Life2Image.sprite = LifeFillSprite;
                Life3Image.sprite = LifeNullSprite;
                Life1Image.fillAmount = 1f;
                Life2Image.fillAmount = 0.5f;
                Life3Image.fillAmount = 1f;
                break;
            case 4:
                Life1Image.sprite = LifeFillSprite;
                Life2Image.sprite = LifeFillSprite;
                Life3Image.sprite = LifeNullSprite;
                Life1Image.fillAmount = 1f;
                Life2Image.fillAmount = 1f;
                Life3Image.fillAmount = 1f;
                break;
            case 5:
                Life1Image.sprite = LifeFillSprite;
                Life2Image.sprite = LifeFillSprite;
                Life3Image.sprite = LifeFillSprite;
                Life1Image.fillAmount = 1f;
                Life2Image.fillAmount = 1f;
                Life3Image.fillAmount = 0.5f;
                break;
            case 6:
                Life1Image.sprite = LifeFillSprite;
                Life2Image.sprite = LifeFillSprite;
                Life3Image.sprite = LifeFillSprite;
                Life1Image.fillAmount = 1f;
                Life2Image.fillAmount = 1f;
                Life3Image.fillAmount = 1f;
                break;
        }
	}

	public void StartGameplay()
	{
		StartCoroutine(CountDownCoroutine());
	}

	public IEnumerator CountDownCoroutine()
	{
		GameStartedUp.SetActive(false);
		yield return new WaitForSeconds(1);
		CountDownGameObject.SetActive(true);
		audioHandler.PlaySFXCounter();
		CountDownText.text = "3";
		yield return new WaitForSeconds(1);
		audioHandler.PlaySFXCounter();
		CountDownText.text = "2";
		yield return new WaitForSeconds(1);
		audioHandler.PlaySFXCounter();
		CountDownText.text = "1";
		yield return new WaitForSeconds(1);
		audioHandler.PlaySFXCounterFinal();
		CountDownText.text = "GO";
		CountDownText.fontSize = 195;
		yield return new WaitForSeconds(1);
		CountDownGameObject.SetActive(false);
		ManagerRacing.Instance.StartGame();
		GameplayGUIGameObject.SetActive(true);
		audioHandler.PlayCarSound();
		RetreavingData.GetData();
	}

	void OnGameOver(float totalScore)
	{
		StartCoroutine(SkipTimer_GameOver(2f, totalScore));
		/*Debug.Log("Game Over Indicated");
		if (ManagerApplication.Instance && ManagerPlayer.Instance)
		{

			Debug.Log("Game Over Initiated");
			StartCoroutine(SkipTimer(2f, totalScore));
		}
		else
		{
			Debug.Log("Game Over Error");
		}*/
	}

    void OnGameFinish(float totalScore)
    {
        StartCoroutine(SkipTimer_GameFinish(2f, totalScore));
    }


    IEnumerator SkipTimer_GameFinish(float timer, float totalScore)
	{
		managerRacing.RacingSpeed = 0;
		managerRacing.GameStarted = false;
		audioHandler.StopCarSound();
		GameplayGUIGameObject.SetActive(false);
		GameFinishUI.SetActive(true);
		//NameText.text = RetreavingData.NAME;
		FinalScoreText.text = String.Format("{0:n0}", totalScore);
		yield return new WaitForSeconds(timer);
		//RetreavingData.SCORE = (int) totalScore;
		///RetreavingData.SendData();
	}


    IEnumerator SkipTimer_GameOver(float timer, float totalScore)
    {
        managerRacing.RacingSpeed = 0;
        managerRacing.GameStarted = false;
        audioHandler.StopCarSound();
        GameplayGUIGameObject.SetActive(false);
        GameOverUI.SetActive(true);
        //NameText.text = RetreavingData.NAME;
        FinalScoreGOText.text = String.Format("{0:n0}", totalScore);
        yield return new WaitForSeconds(timer);
        //RetreavingData.SCORE = (int) totalScore;
        ///RetreavingData.SendData();
    }

    void OnGameTimer(int gameTimer)
	{
		InitTimer(gameTimer);
	}

    void OnPlayerToken(float tokenScore)
    {
        InitToken(tokenScore);
    }

    void OnPlayerLife(int life)
	{
		InitLife(life);
	}

	void OnPlayerScore(float score)
	{
		InitScore(score);
	}

    public void OnGameRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
