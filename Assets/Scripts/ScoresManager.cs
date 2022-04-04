using System;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoresManager : MonoBehaviour
{
	#region Variables

	public const string HIGH_SCORE_KEY = "HighScore";

	public static ScoresManager Instance { get; private set; } = null;

	public int Score { get; private set; } = 0;
	public int HighScore { get; private set; } = 0;

	#endregion

	#region Events

	public static event Action<int> ScoreChangedEvent = delegate(int score) { };

	#endregion

	#region Awake

	private void Awake()
	{
		Assert.IsNull(Instance, "More than one instance of scores manager exists in the scene.");

		Instance = this;

		LoadHighScore();

		EllipseMaker.OnCompleteEllipse += IncreaseScore;
	}

	#endregion

	#region OnDestroy

	private void OnDestroy()
	{
		Instance = null;

		SaveHighScore();
		EllipseMaker.OnCompleteEllipse -= IncreaseScore;
	}

	#endregion

	#region Score

	public void IncreaseScore()
	{
		Score++;

		ScoreChangedEvent(Score);
	}

	#endregion

	#region High Score

	public void LoadHighScore()
	{
		HighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
	}

	public void SaveHighScore()
	{
		if (Score > HighScore)
		{
			PlayerPrefs.SetInt(HIGH_SCORE_KEY, Score);
			PlayerPrefs.Save();
		}
	}

	#endregion
}
