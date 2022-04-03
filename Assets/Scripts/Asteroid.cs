using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    #region Components Variables
    private Rigidbody2D _rb;
    #endregion

    #region General Variables
    private Vector2 _workspace;
    [SerializeField] private float _rotationSpeed;
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
        _rb.velocity = _workspace;
    }
    #endregion

}
