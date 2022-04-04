using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IHitableByAsteroid
{
    #region Components Variables
    private Rigidbody2D _rb;
    #endregion

    #region General Variables
    private Vector2 _workspace;
    [SerializeField] private float _rotationSpeed;
    private float _speed = 1f;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed);
    }
    #endregion

    #region Movement Functions
    public void ChangeDirection(float x, float y)
    {
        _workspace.Set(x, y);
    }

    public void SetSpeed(float speed)
	{
        _speed = speed;
	}

    public void ApplyVelocity()
	{
        _rb.velocity = _workspace * _speed;
    }
    #endregion

    #region Collision

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out IHitableByAsteroid hitableByAsteroid) == true)
		{
            hitableByAsteroid.HitByAsteroid();
            gameObject.SetActive(false);
        }
    }

	public void HitByAsteroid()
	{
        gameObject.SetActive(false);
	}

	#endregion
}
