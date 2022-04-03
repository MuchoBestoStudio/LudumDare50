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
		ScoresManager.ScoreChangedEvent += OnScoreChanged;

		OnScoreChanged(ScoresManager.Instance.Score);
	}

	private void OnDisable()
	{
		ScoresManager.ScoreChangedEvent -= OnScoreChanged;
	}

	#endregion

	#region Score

	private void OnScoreChanged(int score)
	{
		_scoreTextMesh.text = string.Format(_scoreFormat, score);
	}

	#endregion
}
