using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EclipseMaker : MonoBehaviour
{
	[SerializeField, Min(18)]
	private int _numberOfSegment = 100;
	[SerializeField, Range(0f, 360f)]
	private float _angle = 0;
	[SerializeField]
	private Vector3 _axis = Vector2.one;
	private Vector3[] _points = null;

	private LineRenderer _lineRenderer = null;

	[SerializeField, Min(1f)]
	private float _speed = 1f;
	private int _indexWay = 0;

	private void Awake()
	{
		_points = new Vector3[_numberOfSegment];
		_lineRenderer = GetComponent<LineRenderer>();
		_indexWay = 0;
		_lineRenderer.positionCount = _numberOfSegment;
		CreateEllipse();
		transform.position = _points[_indexWay];
	}

	private void Update()
	{
		CreateEllipse();
		Move();
		CheckCondition();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Vector3 direction = transform.position.normalized;
			Quaternion angleRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
			Vector3 angleDirection = angleRotation * Vector3.right;
			float dotPoint = Vector3.Dot(angleDirection, direction);

			if (Mathf.Abs(dotPoint) > .5f)
			{
				_axis.x += .5f;
				_axis.y -= .5f;
			}
			else
			{
				_axis.x -= .5f;
				_axis.y += .5f;
			}

			float angle = Mathf.LerpAngle(_angle, Vector3.Angle(direction, Vector3.right), .1f);

			_angle = angle;
			CreateEllipse();
		}
	}

	private void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, _points[_indexWay], _speed * Time.deltaTime);

		if (Vector3.Distance(transform.position, _points[_indexWay]) < .001f)
		{
			_indexWay = (_indexWay + 1) % _numberOfSegment;
		}
	}

	public void CreateEllipse()
	{
		for (int i = 0 ; i < _numberOfSegment ; ++i)
		{
			Vector3 point = Calculate(i / (float)_numberOfSegment);
			_points[i] = point;
		}

		_lineRenderer.SetPositions(_points);
	}

	private Vector3 Calculate(float t)
	{
		float angle = t * 360f * Mathf.Deg2Rad;
		float x = Mathf.Sin(angle) * _axis.x;
		float y = Mathf.Cos(angle) * _axis.y;
		Vector3 position = new Vector3(x, y, 0f);
		position = Quaternion.Euler(.0f, .0f, _angle) * position;

		return position;
	}

	private void CheckCondition()
	{
		float distance = transform.position.sqrMagnitude;

		if (distance > 30f || distance < 6f) // lose condition with distance
		{
			// Call GameOverEvent
		}
	}

	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			return;
		}

		_points = new Vector3[_numberOfSegment];
		_lineRenderer = GetComponent<LineRenderer>();

		_lineRenderer.positionCount = _numberOfSegment;
		CreateEllipse();
	}

	private void OnDrawGizmos()
	{
		// to do :  fixed the dot product

		Vector3 direction = transform.position.normalized;
		Quaternion angleRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
		Vector3 angleEllipse = angleRotation * Vector3.right;
		//angleEllipse *= _axis.x * .5f;

		float dotPoint = Vector3.Dot(Vector3.forward, transform.position);
		//float dotPoint = Vector3.Dot(angleEllipse, direction);
		//float dotPoint = Vector3.Dot(direction, angleEllipse);
		Debug.Log($"{dotPoint}");



		Debug.DrawLine(Vector3.zero, direction * transform.position.magnitude, Color.black);
		Debug.DrawLine(Vector3.zero, direction * transform.position.magnitude * dotPoint, Color.cyan);

		Debug.DrawLine(Vector3.zero, angleEllipse * _axis.x, Color.red);
		Debug.DrawLine(Vector3.zero, angleEllipse * _axis.x * dotPoint, Color.cyan);


	}
}
