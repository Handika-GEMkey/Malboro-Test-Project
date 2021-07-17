using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ManagerRacing : MonoBehaviour {

	public static ManagerRacing Instance;

	public bool GameStarted;
	public ObjectiveGenerator ObjectiveGenerator;

	[SerializeField] private Vector3[] linePositions;

	[Range(0.2f, 5f)]
	[SerializeField] private float racingSpeed;

	[SerializeField] private float totalScore;

	[SerializeField] private int totalLife;

	[SerializeField] private int gameTimer;

	[SerializeField] 
	[Range(10f, 20f)]
	private int obsDistance;

	private event Action<float> callbackGameOver;
	public event Action<float> CallbackGameOver
	{
		add
		{
			this.callbackGameOver -= value;
			this.callbackGameOver += value;
		}
		remove
		{
			this.callbackGameOver -= value;
		}
	}

	private event Action<int> callBackGameTimer;
	public event Action<int> CallBackGameTimer
	{
		add
		{
			this.callBackGameTimer -= value;
			this.callBackGameTimer += value;
		}
		remove
		{
			this.callBackGameTimer -= value;
		}
	}

	private event Action<int> callbackPlayerLife;
	public event Action<int> CallbackPlayerLife
	{
		add
		{
			this.callbackPlayerLife -= value;
			this.callbackPlayerLife += value;
		}
		remove
		{
			this.callbackPlayerLife -= value;
		}
	}

	private event Action<float> callbackPlayerScore;
	public event Action<float> CallbackPlayerScore
	{
		add
		{
			this.callbackPlayerScore -= value;
			this.callbackPlayerScore += value;
		}
		remove
		{
			this.callbackPlayerScore -= value;
		}
	}

	public float TotalScore
	{
		get
		{
			return totalScore;
		}
		set
		{
			totalScore = value;
			if (callbackPlayerScore != null) { callbackPlayerScore.Invoke(totalScore); }
			if (totalScore % 200 == 0 && racingSpeed < 2.5f)
			{
				racingSpeed += 0.02f;
			}
		}
	}

	public int TotalLife
	{
		get
		{
			return totalLife;
		}
		set
		{
			totalLife = value;
			if (callbackPlayerLife != null) { callbackPlayerLife.Invoke(totalLife); }
			if (totalLife < 1)
			{
				if (callbackGameOver != null) callbackGameOver.Invoke(totalScore);
			}
		}
	}

	public int ObsDistance 
	{
		get { return obsDistance; }
	}

	public Vector3[] GetLinePositions() { return linePositions; }
	public float RacingSpeed
	{
		set
		{
			racingSpeed = value;
		}
		get
		{
			return racingSpeed;
		}
	}

	void Awake()
	{
		Instance = this;
	}

	public void StartGame()
	{
		GameStarted = true;
		ObjectiveGenerator.BatchGenerator();
		StartCoroutine(TimerCoroutine());
	}

	IEnumerator TimerCoroutine()
	{
		while (GameStarted)
		{
			yield return new WaitForSeconds(1);
			gameTimer += 1;
			if (callBackGameTimer != null)
			{
				callBackGameTimer.Invoke(gameTimer);
			}
		}
	}
}
