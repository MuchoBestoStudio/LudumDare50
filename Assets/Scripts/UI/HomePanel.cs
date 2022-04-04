using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{
	#region Variables

	[SerializeField]
	private Canvas _canvas = null;
	[SerializeField]
	private string _gameplaySceneName = string.Empty;
	[SerializeField]
	private OptionPanel _optionalPanel = null;

	[Header("Buttons")]
	[SerializeField]
	private Button _playButton = null;
	[SerializeField]
	private Button _optionsButton = null;
	[SerializeField]
	private Button _creditsButton = null;

	#endregion

	#region Awake

	private void Awake()
	{
		_playButton.onClick.AddListener(OnPlayButtonClicked);
		_optionsButton.onClick.AddListener(OnOptionsButtonClicked);
	//	_creditsButton.onClick.AddListener(OnCreditsButtonClicked);
	}

	#endregion

	#region Show / Hide

	public void Show()
	{
		_canvas.enabled = true;
	}

	public void Hide()
	{
		_canvas.enabled = false;
	}

	#endregion

	#region Buttons

	private void OnPlayButtonClicked()
	{
		SceneManager.LoadScene(_gameplaySceneName);
	}

	private void OnOptionsButtonClicked()
	{
		Hide();

		_optionalPanel.Show();
	}

	private void OnCreditsButtonClicked()
	{
		Hide();

	}

	#endregion
}
