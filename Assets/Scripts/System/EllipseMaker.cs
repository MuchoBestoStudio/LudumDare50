// #define HARD_DEBUG

using UnityEngine;

public class EllipseMaker : MonoBehaviour
{
	public static System.Action OnCompleteEllipse = null;

	[SerializeField, Min(18)]
	private int _numberOfSegment = 100;
	[SerializeField, Range(0f, 360f)]
	private float _angle = 0;
	[SerializeField]
	private Vector3 _axis = Vector2.one;
	private Vector3[] _points = null;
	private int _indexWay = 0;

	private LineRenderer _lineRenderer = null;

	public Vector2 sizeRange = Vector2.one;

	[SerializeField, Min(.000001f)]
	private float _revolutionBySecond = 1f;
	private float t = 0f;

	private void Awake()
	{
		_points = new Vector3[_numberOfSegment];
		_lineRenderer = GetComponent<LineRenderer>();
		_indexWay = 0;
		_lineRenderer.positionCount = _numberOfSegment;
		CreateEllipse();
		transform.position = _points[_indexWay];

		GameplayManger.GameStateChangedEvent += OnGameStateChanged;
	}

	private void OnDestroy()
	{
		GameplayManger.GameStateChangedEvent -= OnGameStateChanged;
	}

	private void OnGameStateChanged(GameplayState previousState, GameplayState newState)
	{
		switch (newState)
		{
			case GameplayState.None:
			case GameplayState.GameOver:
				enabled = false;
				break;
			case GameplayState.Playing:
				enabled = true;
				break;
		}
	}

	private void Update()
	{
		CreateEllipse();
		Move();
		ChangeScale();
		CheckCondition();

		#if HARD_DEBUG
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ModifyEllipse();
		}
		#endif
	}

	private void ChangeScale()
	{
		float maxDistance = Mathf.Max(_axis.x, _axis.y);
		float lerpIndex = Mathf.InverseLerp(-maxDistance, maxDistance, transform.position.y);

		transform.localScale = Vector3.Lerp(Vector3.one * sizeRange.x, Vector3.one * sizeRange.y, lerpIndex);
	}

	public void ModifyEllipse()
	{
		Vector3 direction = transform.position.normalized;
		Quaternion angleRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
		Vector3 angleEllipse = angleRotation * Vector3.right;

		float angle = Vector3.Angle(direction, angleEllipse);
		float dot = Mathf.Cos(angle * Mathf.Deg2Rad) * transform.position.magnitude;
		dot = Mathf.Abs(dot / _axis.x);

		if (dot > .5f)
		{
			_axis.x += .5f;
			_axis.y -= .5f;
		}
		else
		{
			_axis.x -= .5f;
			_axis.y += .5f;
		}

		angle = Mathf.LerpAngle(_angle, Vector3.Angle(direction, Vector3.right), .1f);

		_angle = angle;
		CreateEllipse();
	}

	private void Move()
	{
		transform.position = _points[_indexWay];
		float duration = 1f / _revolutionBySecond;

		t += Time.deltaTime / duration;
		if (t > 1f)
		{
			t = 0f;
			OnCompleteEllipse?.Invoke();
		}
		_indexWay = Mathf.RoundToInt(t * (_numberOfSegment - 1));
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


	public bool DrawDebug = false;
	private void OnDrawGizmos()
	{
		if (DrawDebug == false)
		{
			return;
		}

		Vector3 direction = transform.position.normalized;
		Quaternion angleRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
		Vector3 angleEllipse = angleRotation * Vector3.right;

		float angle = Vector3.Angle(direction, angleEllipse);
		float dot = Mathf.Cos(angle * Mathf.Deg2Rad) * transform.position.magnitude;

		Debug.DrawLine(Vector3.zero, direction * transform.position.magnitude, Color.black);
		Debug.DrawLine(Vector3.zero, angleEllipse * _axis.x, Color.red);
		Debug.DrawLine(Vector3.zero, angleEllipse * dot, Color.cyan);
		Debug.DrawLine(transform.position, angleEllipse * dot, Color.green);
	}
}
