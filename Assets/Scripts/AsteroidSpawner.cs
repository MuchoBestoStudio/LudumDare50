using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	#region General Variables
	private GameObjectPool _pool;
	[SerializeField] private AnimationCurve _spawningCurve;
	[SerializeField] private GameObject _asteroid;
	[SerializeField] private Transform _earth;
	[SerializeField] private float _speed;
	[SerializeField] private float _spawnRadius;
	private Asteroid _activeAsteroid;

	private float _time;

	#endregion

	#region Unity Callbacks
	private void Start()
	{
		_time = _spawningCurve.Evaluate(0f);
		_pool = new GameObjectPool(_asteroid,5);
	}

	private void Update()
	{
		_time -= Time.deltaTime;

		if (_time <= 0)
		{
			Vector2 position = GenerateRandomAround();
			Vector2 direction = ((Vector2)_earth.position - position).normalized;
			_activeAsteroid = _pool.Rescue().GetComponent<Asteroid>();
			_activeAsteroid.gameObject.SetActive(true);
			_activeAsteroid.transform.position = position;
			_activeAsteroid.ChangeDirection(direction.x,direction.y);


			_time += _spawningCurve.Evaluate(Time.timeSinceLevelLoad);
		}
	}
	#endregion

	#region Other Functions
	//Not working ;-;
	private Vector2 GenerateRandomAround()
	{
		float angle = Random.Range(0, 360);

		return Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.up * _spawnRadius;
	}

	#endregion

	#region Debug

	#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(Vector3.zero, _spawnRadius);
	}

	#endif

	#endregion
}