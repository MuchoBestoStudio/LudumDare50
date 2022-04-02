using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSystem : MonoBehaviour
{
	private List<CelestialBody> _allBodies = null;

	void Awake()
	{
		_allBodies = new List<CelestialBody>();
		_allBodies.AddRange(FindObjectsOfType<CelestialBody>());
		Time.fixedDeltaTime = Universe.PhysicsTimeStep;
		//Debug.Log("Setting fixedDeltaTime to: " + Universe.physicsTimeStep);
	}

	void FixedUpdate()
	{
		for (int i = 0 ; i < _allBodies.Count ; i++)
		{
			if (_allBodies[i].IsCenter())
			{
				continue;
			}
			Vector3 acceleration = CalculateAcceleration(_allBodies[i].Position, _allBodies[i]);
			_allBodies[i].UpdateVelocity(acceleration, Universe.PhysicsTimeStep);
			//bodies[i].UpdateVelocity (bodies, Universe.physicsTimeStep);
		}

		for (int i = 0 ; i < _allBodies.Count ; i++)
		{
			_allBodies[i].UpdatePosition(Universe.PhysicsTimeStep);
		}

	}

	public Vector3 CalculateAcceleration(Vector3 point, CelestialBody ignoreBody = null)
	{
		Vector3 acceleration = Vector3.zero;
		foreach (var body in _allBodies)
		{
			if (body != ignoreBody)
			{
				float sqrDst = (body.Position - point).sqrMagnitude;
				Vector3 forceDir = (body.Position - point).normalized;
				acceleration += forceDir * Universe.GravitationalConstant * body.Mass / sqrDst;
			}
		}

		return acceleration;
	}
}
