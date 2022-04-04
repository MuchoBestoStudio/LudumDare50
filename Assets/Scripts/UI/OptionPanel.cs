using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
	#region Variables

	[SerializeField]
	private Canvas _canvas = null;
	[SerializeField]
	private HomePanel _mainMenuPanel = null;

	[SerializeField]
	private AudioMixerGroup _musicMixerGroup = null;
	[SerializeField]
	private AudioMixerGroup _sfxMixerGroup = null;

	[SerializeField]
	private Slider _musicSlider = null;
	[SerializeField]
	private Slider _sfxSlider = null;
	[SerializeField]
	private Button _backButton = null;

	#endregion

	#region Awake

	private void Awake()
	{
		_musicMixerGroup.audioMixer.GetFloat("MusicVolume", out float musicVolume);
		_musicSlider.value = musicVolume;

		_sfxMixerGroup.audioMixer.GetFloat("SFXVolume", out float sfxVolume);
		_sfxSlider.value = sfxVolume;

		_musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
		_sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
		_backButton.onClick.AddListener(OnBackButtonClicked);
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

	#region Sliders

	private void OnMusicSliderChanged(float value)
	{
		_musicMixerGroup.audioMixer.SetFloat("MusicVolume", value);
	}

	private void OnSFXSliderChanged(float value)
	{
		_sfxMixerGroup.audioMixer.SetFloat("SFXVolume", value);
	}

	#endregion

	#region Buttons

	private void OnBackButtonClicked()
	{
		Hide();

		_mainMenuPanel.Show();
	}

	#endregion
}
