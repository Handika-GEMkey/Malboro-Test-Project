using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ManagerRacing : MonoBehaviour {

	public static ManagerRacing Instance;

	[SerializeField]
	private WebGLBridger WebBridger;
	public bool GameStarted;
	public ObjectiveGenerator ObjectiveGenerator;

	public int CarCode;
	public bool IsPopupPointOpen;

	[SerializeField] private Vector3[] linePositions;

	[Range(0.2f, 5f)]
	[SerializeField] private float racingSpeed;

	[SerializeField] private float totalScore;

	[SerializeField] private float tokenScore;

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

	private event Action<float> callbackGameFinish;
	public event Action<float> CallbackGameFinish
	{
		add
		{
			this.callbackGameFinish -= value;
			this.callbackGameFinish += value;
		}
		remove
		{
			this.callbackGameFinish -= value;
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

	private event Action<float> callbackTokenScore;
	public event Action<float> CallbackTokenScore
	{
		add
		{
			this.callbackTokenScore -= value;
			this.callbackTokenScore += value;
		}
		remove
		{
			this.callbackTokenScore -= value;
		}
	}

	private void Start()
	{
		StartCoroutine(delay());
	}

	IEnumerator delay()
	{
		yield return new WaitForSeconds(3);
		CarCode = WebBridger.CarCode;
		var popupPointCode = WebBridger.PointStatus;
		if (popupPointCode < 1)
		{
			IsPopupPointOpen = false;
		}
		else
		{
			IsPopupPointOpen = true;
		}

		Debug.Log("Car Code: " + CarCode);
		Debug.Log("Popup Point: " + IsPopupPointOpen);
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
			if (totalScore % 40 == 0 && racingSpeed < 2.5f)
			{
				racingSpeed += 0.05f;
			}
		}
	}

    public float TokenScore
    {
        get
        {
            return tokenScore;
        }
        set
        {
            tokenScore = value;
            if (tokenScore >= 50f)
            {
                if (callbackGameFinish != null) callbackGameFinish.Invoke(totalScore);
            }
            else if (tokenScore < 50f)
            {
                if (callbackTokenScore != null) { callbackTokenScore.Invoke(tokenScore); }
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
        gameTimer = 60;
		ObjectiveGenerator.BatchGenerator();
		StartCoroutine(TimerCoroutine());
	}

	IEnumerator TimerCoroutine()
	{
		while (GameStarted)
		{
			yield return new WaitForSeconds(1f);
			gameTimer -= 1;
			if (callBackGameTimer != null)
			{
				callBackGameTimer.Invoke(gameTimer);
			}
		}
	}
    /// <summary>
    /// Add Time from fuel
    /// </summary>
    public void AddingTime(int v)
    {
        gameTimer += v;
        if (gameTimer > 60) { gameTimer = 60; }
        if (callBackGameTimer != null)
        {
            callBackGameTimer.Invoke(gameTimer);
        }
    }
}
