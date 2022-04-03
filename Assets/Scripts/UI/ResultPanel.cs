using UnityEngine;

public class ResultPanel : MonoBehaviour
{
	#region Variables

	[SerializeField]
	private Canvas _canvas = null;

	#endregion

	#region Awake

	private void Awake()
	{
		_canvas.enabled = false;
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
		}
	}

	#endregion
}
