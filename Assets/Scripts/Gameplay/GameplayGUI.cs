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

	[Header("In-Game Result")]
	public GameObject GameOverUI;
	public Text NameText;
	public Text FinalScoreText;
	public Text CoinText;

	public RetreavingData RetreavingData;

	void Start()
	{
		managerRacing.CallbackGameOver += OnGameOver;
		managerRacing.CallBackGameTimer += OnGameTimer;
		managerRacing.CallbackPlayerLife += OnPlayerLife;
		managerRacing.CallbackPlayerScore += OnPlayerScore;
	}

	public void InitScore(float Point)
	{
		ScoreText.text = String.Format("{0:n0}", Point);
	}

	public void InitTimer(int time)
	{
		TimeSpan ts = TimeSpan.FromSeconds(time);
		TimerText.text = ts.ToString();
	}

	public void InitLife(int life)
	{
		switch (life)
		{
			case 0:
				Life1Image.sprite = LifeNullSprite;
				Life2Image.sprite = LifeNullSprite;
				Life3Image.sprite = LifeNullSprite;
				break;
			case 1:
				Life1Image.sprite = LifeFillSprite;
				Life2Image.sprite = LifeNullSprite;
				Life3Image.sprite = LifeNullSprite;
				break;
			case 2:
				Life1Image.sprite = LifeFillSprite;
				Life2Image.sprite = LifeFillSprite;
				Life3Image.sprite = LifeNullSprite;
				break;
			case 3:
				Life1Image.sprite = LifeFillSprite;
				Life2Image.sprite = LifeFillSprite;
				Life3Image.sprite = LifeFillSprite;
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
		StartCoroutine(SkipTimer(2f, totalScore));
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

	IEnumerator SkipTimer(float timer, float totalScore)
	{
		managerRacing.RacingSpeed = 0;
		managerRacing.GameStarted = false;
		audioHandler.StopCarSound();
		GameplayGUIGameObject.SetActive(false);
		GameOverUI.SetActive(true);
		NameText.text = RetreavingData.NAME;
		FinalScoreText.text = String.Format("{0:n0}", totalScore);
		yield return new WaitForSeconds(timer);
		RetreavingData.SCORE = (int) totalScore;
		RetreavingData.SendData();
	}

	void OnGameTimer(int gameTimer)
	{
		InitTimer(gameTimer);
	}

	void OnPlayerLife(int life)
	{
		InitLife(life);
	}

	void OnPlayerScore(float score)
	{
		InitScore(score);
	}
}
