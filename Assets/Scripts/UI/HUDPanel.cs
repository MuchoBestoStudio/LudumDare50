using TMPro;
using UnityEngine;

public class HUDPanel : MonoBehaviour
{
	#region Variables

	[Header("Score")]
	[SerializeField]
	private TextMeshProUGUI _scoreTextMesh = null;
	[SerializeField]
	private string _scoreFormat = string.Empty;

	#endregion

	#region OnEnable / OnDisable

	private void OnEnable()
	{
		GameplayManger.GameStateChangedEvent += OnGameplayStateChanged;
		ScoresManager.ScoreChangedEvent += OnScoreChanged;
	}

	private void OnDisable()
	{
		GameplayManger.GameStateChangedEvent -= OnGameplayStateChanged;
		ScoresManager.ScoreChangedEvent -= OnScoreChanged;
	}

	#endregion

	#region Start

	private void Start()
	{
		OnScoreChanged(ScoresManager.Instance.Score);
	}

	#endregion

	#region Gameplay Manager

	private void OnGameplayStateChanged(GameplayState _, GameplayState current)
	{
		if (current == GameplayState.GameOver)
		{
			_scoreTextMesh.gameObject.SetActive(false);
		}
	}

	#endregion

	#region Score

	private void OnScoreChanged(int score)
	{
		_scoreTextMesh.text = string.Format(_scoreFormat, score);
	}

	#endregion
}
