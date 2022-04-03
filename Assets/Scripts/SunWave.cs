using UnityEngine;

public class SunWave : MonoBehaviour
{
	#region Variables

	[SerializeField, Min(0.1f)]
	private float _translationSpeed = 1f;
	[SerializeField]
	private float _temperatureModifier = 1f;

	#endregion

	#region Update

	private void Update()
	{
		transform.position += transform.up * _translationSpeed * Time.deltaTime;
	}

	#endregion

	#region Trigger

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IHitableBySunWave hitable = collision.GetComponent<IHitableBySunWave>();
		if (hitable != null)
		{ 
			hitable.HitBySunWave(_temperatureModifier);
		}
	}

	#endregion
}
