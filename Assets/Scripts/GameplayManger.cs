using System;
using UnityEngine;
using UnityEngine.Assertions;

public class GameplayManger : MonoBehaviour
{
	#region Variables

	public static GameplayManger Instance { get; private set; } = null;

	public GameplayState CurrentState { get; private set; } = GameplayState.None;
	public GameplayState PreviousState { get; private set; } = GameplayState.None;

	#endregion

	#region Events

	public static event Action<GameplayState, GameplayState> GameStateChangedEvent = delegate(GameplayState previous, GameplayState current) { };

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

	public void ChangeState(GameplayState state)
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
