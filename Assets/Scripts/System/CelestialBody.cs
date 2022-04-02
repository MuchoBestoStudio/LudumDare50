using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : MonoBehaviour
{
	[SerializeField]
	private bool _center = false;
	[SerializeField]
	private float _radius = 1;
	[SerializeField, Min(0.0001f)]
	private float _radiusImpact = .7f;
	[SerializeField]
	private float _surfaceGravity = 5;
	[SerializeField]
	private Vector3 _initialVelocity = Vector3.zero;
	private Rigidbody _rigidBodyComp = null;

	public Vector3 InitialVelocity => _initialVelocity;
	public Vector3 Velocity { get; private set; }
	public Vector3 Position => _rigidBodyComp.position;
	public float Mass { get; private set; }


	private void Awake()
	{
		_rigidBodyComp = GetComponent<Rigidbody>();
		Velocity = _initialVelocity;
		_rigidBodyComp.mass = _surfaceGravity * _radius * _radius * _radiusImpact * _radiusImpact / Universe.GravitationalConstant;
	}

	private void OnValidate()
	{
		Mass = _surfaceGravity * _radius * _radius * _radiusImpact * _radiusImpact / Universe.GravitationalConstant;
		transform.localScale = Vector3.one * _radius;
	}

	public void UpdateVelocity(Vector3 acceleration, float deltaTime)
	{
		Velocity += acceleration * deltaTime;
	}

	public void UpdatePosition(float deltaTime)
	{
		if (_center)
		{
			return;
		}
		_rigidBodyComp.MovePosition(_rigidBodyComp.position + Velocity * deltaTime);
	}

	public bool IsCenter() => _center;
}
