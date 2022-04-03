using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    #region General Variables
    private GameObjectPool _pool;
    [SerializeField] private Camera _main;
    [SerializeField] private int _secondsToSpawn;
    [SerializeField] private GameObject _asteroid;
    [SerializeField] private Transform _earth;
    [SerializeField] private float _speed;
    private Vector2 _spawnPos;
    private Asteroid _activeAsteroid;

    private float _time;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        _time = _secondsToSpawn;
        _pool = new GameObjectPool(_asteroid,5);
    }

    private void Update()
    {
        if (_time >= _secondsToSpawn) {
            Invoke("GenerateRandomAround",0);
            Vector2 direction = ((Vector2)_earth.position - _spawnPos).normalized;
            _activeAsteroid = _pool.Rescue().GetComponent<Asteroid>();
            _activeAsteroid.gameObject.SetActive(true);
            _activeAsteroid.transform.position = _spawnPos;
            _activeAsteroid.ChangeDirection(direction.x,direction.y);
            _time = 0;
        }
        else _time += Time.deltaTime;
    }
    #endregion

    #region Other Functions
    //Not working ;-;
    private Vector2 GenerateRandomAround()
    {
        _spawnPos = new Vector2(Random.Range(-Screen.width - 1, Screen.width + 1), 
            Random.Range(-Screen.height - 1, Screen.height + 1));
        if (_spawnPos.x > -Screen.width - 1 || _spawnPos.x < Screen.width + 1
            && _spawnPos.y > -Screen.height - 1 || _spawnPos.y < Screen.height + 1) GenerateRandomAround();
        return _spawnPos;
    }
    #endregion
}