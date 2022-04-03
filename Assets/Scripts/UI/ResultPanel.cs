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
		GameManger.GameStateChangedEvent += OnGameStateChanged;
	}

	private void OnDisable()
	{
		GameManger.GameStateChangedEvent -= OnGameStateChanged;
	}

	#endregion

	#region Game Manager Events

	private void OnGameStateChanged(GameState _, GameState cur)
	{
		if (cur == GameState.Ending)
		{
			_canvas.enabled = true;
		}
	}

	#endregion
}
