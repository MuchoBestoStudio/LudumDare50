using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBehaviour : MonoBehaviour
{
	[SerializeField]
	private Transform _planet = null;
	[SerializeField]
	private Transform _star = null;
	[SerializeField, Range(0f, 1f)]
	private float _distance = .1f;
	[SerializeField]
	private Vector3 _offset = Vector3.zero;

	void Update()
	{
		if (_planet == null || _star == null)
		{
			return;
		}

		transform.position = Vector3.Lerp(_star.position, _planet.position, _distance) + _offset;
	}
}
