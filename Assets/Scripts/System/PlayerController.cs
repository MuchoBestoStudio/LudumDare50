using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody _rigidbodyComp = null;
	public float _minForce = 3f;
	public float _maxForce = 10f;

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log($"Touch: {hit.transform.name}");
				Vector3 touchPoint = hit.point;
				touchPoint.z = 0f;
				Vector3 direction = (transform.position - touchPoint).normalized;
				float force = Mathf.Lerp(_minForce, _maxForce, .5f);

				_rigidbodyComp.AddForce(direction * force, ForceMode.Impulse);
			}
		}
	}
}
