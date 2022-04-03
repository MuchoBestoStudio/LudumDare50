using System;
using UnityEngine;

public class Earth : MonoBehaviour, IHitableBySunWave
{
	#region Variables

	public float Temperature { get; private set; } = 0f;

	[SerializeField]
	private EclipseMaker _eclispseMaker = null;
	[SerializeField]
	private float _startTemperature = 20f;
	[SerializeField]
	private float _warningTemperature = 40f;
	[SerializeField]
	private float _maxTemperature = 50f;

	[SerializeField]
	private GameObject _warningIndicator = null;

	#endregion

	#region Events

	public event Action<float> TemperatureChangedEvent = delegate(float temperature) { };

	#endregion

	#region Start

	private void Start()
	{
		ChangeTemperature(_startTemperature);
	}

	#endregion

	#region Temperature

	private void ChangeTemperature(float temperature)
	{
		Temperature = temperature;

		TemperatureChangedEvent(Temperature);
	}

	#endregion

	#region Interfaces

	public void HitBySunWave(float amountOfTemperature)
	{
		ChangeTemperature(Temperature + amountOfTemperature);

		_eclispseMaker.ModifyEllipse();
	}

	#endregion
}
