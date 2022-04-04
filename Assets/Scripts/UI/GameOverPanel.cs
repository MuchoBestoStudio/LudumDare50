using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
	#region Variables

	[SerializeField]
	private Canvas _canvas = null;

	[Header("Scenes")]
	[SerializeField]
	private string _gameplaySceneName = string.Empty;
	[SerializeField]
	private string _menuSceneName = string.Empty;

	[Header("Scores")]
	[SerializeField]
	private string _scoreFormat = string.Empty;
	[SerializeField]
	private string _highScoreFormat = string.Empty;
	[SerializeField]
	private TextMeshProUGUI _scoreTextMesh = null;
	[SerializeField]
	private TextMeshProUGUI _highScoreTextMesh = null;
	[SerializeField]
	private TextMeshProUGUI _newHighScoreTextMesh = null;

	[Header("Stars")]
	[SerializeField]
	private Image _leftStarImg = null;
	[SerializeField]
	private Image _middleStarImg = null;
	[SerializeField]
	private Image _rightStarImg = null;
	[SerializeField]
	private int _secondStarScore = 20;
	[SerializeField]
	private int _thirdStarScore = 30;

	[Header("Buttons")]
	[SerializeField]
	private Button _restartButton = null;
	[SerializeField]
	private Button _menuButton = null;

	#endregion

	#region Awake

	private void Awake()
	{
		_canvas.enabled = false;

		_restartButton.onClick.AddListener(OnRestartButtonClicked);
		_menuButton.onClick.AddListener(OnMenuButtonClicked);
	}

	#endregion

	#region OnEnable / OnDisable

	private void OnEnable()
	{
		GameplayManger.GameStateChangedEvent += OnGameStateChanged;
	}

	private void OnDisable()
	{
		GameplayManger.GameStateChangedEvent -= OnGameStateChanged;
	}

	#endregion

	#region Game Manager Events

	private void OnGameStateChanged(GameplayState _, GameplayState cur)
	{
		if (cur == GameplayState.Ending)
		{
			_canvas.enabled = true;

			UpdateScores();

			UpdateStars();
		}
	}

	#endregion

	#region Scores

	private void UpdateScores()
	{
		_scoreTextMesh.text = string.Format(_scoreFormat, ScoresManager.Instance.Score);

		_highScoreTextMesh.text = string.Format(_highScoreFormat, ScoresManager.Instance.HighScore);

		_highScoreTextMesh.gameObject.SetActive(ScoresManager.Instance.IsHighScoreBeaten() == false);
		_newHighScoreTextMesh.gameObject.SetActive(ScoresManager.Instance.IsHighScoreBeaten());
	}

	#endregion

	#region Stars

	private void UpdateStars()
	{
		_leftStarImg.gameObject.SetActive(true);

		_middleStarImg.gameObject.SetActive(ScoresManager.Instance.Score >= _secondStarScore);

		_rightStarImg.gameObject.SetActive(ScoresManager.Instance.Score >= _thirdStarScore);
	}

	#endregion

	#region Button

	private void OnRestartButtonClicked()
	{
		SceneManager.LoadScene(_gameplaySceneName, LoadSceneMode.Single);
	}

	private void OnMenuButtonClicked()
	{
		SceneManager.LoadScene(_menuSceneName, LoadSceneMode.Single);
	}

	#endregion
}
