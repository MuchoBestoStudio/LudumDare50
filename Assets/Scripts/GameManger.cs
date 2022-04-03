using System;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManger : MonoBehaviour
{
	#region Variables

	public static GameManger Instance { get; private set; } = null;

	public GameState CurrentState { get; private set; } = GameState.None;
	public GameState PreviousState { get; private set; } = GameState.None;

	#endregion

	#region Events

	public static event Action<GameState, GameState> GameStateChangedEvent = delegate(GameState previous, GameState current) { };

	#endregion

	#region Awake

	private void Awake()
	{
		Assert.IsNull(Instance, "More than one instance of game manager exists in the scene.");

		Instance = this;
	}

	#endregion

	#region OnDestroy

	private void OnDestroy()
	{
		Instance = null;
	}

	#endregion

	#region States

	public void ChangeState(GameState state)
	{
		if (CurrentState == state)
		{
			return;
		}

		PreviousState = CurrentState;
		CurrentState = state;

		GameStateChangedEvent(PreviousState, CurrentState);
	}

	#endregion
}
