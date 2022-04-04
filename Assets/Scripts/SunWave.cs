using System.Collections.Generic;
using UnityEngine;

public class SunWave : MonoBehaviour
{
	#region Variables

	[SerializeField, Min(0.1f)]
	private float _translationSpeed = 1f;
	[SerializeField]
	private float _temperatureModifier = 1f;
	[SerializeField]
	private float _maxDistanceToTravel = 10f;

	private List<IHitableBySunWave> _collisionOccured = new List<IHitableBySunWave>();

	#endregion

	#region Update

	private void Update()
	{
		transform.position += transform.up * _translationSpeed * Time.deltaTime;

		if (transform.position.magnitude > _maxDistanceToTravel)
		{
			gameObject.SetActive(false);
		}
	}

	#endregion

	#region Trigger

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IHitableBySunWave hitable = collision.GetComponent<IHitableBySunWave>();
		if (hitable != null && _collisionOccured.Contains(hitable) == false)
		{ 
			_collisionOccured.Add(hitable);

			hitable.HitBySunWave(_temperatureModifier);
		}
	}

	#endregion
}
