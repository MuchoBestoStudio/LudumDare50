using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
	#region Variables

	[Header("Aim")]
	[SerializeField]
	private Transform _aimPivot = null;
	[SerializeField, Range(0, 360f)]
	private float _rotationSpeed = 45f;

	[Header("Shoot")]
	[SerializeField]
	private GameObject _projectilePrefab = null;
	[SerializeField]
	private Transform _aimExit = null;

	private GameObjectPool _projectilePool = null;

	#endregion

	#region Awake

	private void Awake()
	{
		_projectilePool = new GameObjectPool(_projectilePrefab, 4);
	}

	#endregion

	#region Update

	private void Update()
	{
		UpdateMovements();

		UpdateShoot();
	}

	private void UpdateMovements()
	{
		float angleDirection = 0f;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			angleDirection += 1f;
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			angleDirection -= 1f;
		}

		_aimPivot.rotation *= Quaternion.AngleAxis(angleDirection * _rotationSpeed * Time.deltaTime, Vector3.forward);
	}

	private void UpdateShoot()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			ShootSunWave();
		}
	}

	#endregion

	#region Shoot

	private void ShootSunWave()
	{
		GameObject projectile = _projectilePool.Rescue();
		projectile.transform.position = _aimExit.position;
		projectile.transform.rotation = _aimExit.rotation;
		projectile.gameObject.SetActive(true);
	}

	#endregion
}
