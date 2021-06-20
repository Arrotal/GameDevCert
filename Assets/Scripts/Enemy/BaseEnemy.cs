using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    private List<Transform> _waypoints;
    private WaveManager _waveManagement;
    private int waypointIndex = 0, _speed;
    private Animator _animator;
    [SerializeField] protected int _health, _score;
    protected MeshRenderer _meshRenderer;
    [SerializeField] protected List<GameObject> _explosions, _thrusters;
    [SerializeField] private GameObject _sparkle;
    protected BoxCollider2D _boxcollider;
    protected int _difficulty;

    protected int _healthMax;
    public void SetWaveManager(WaveManager _waveManager)
    {
        this._waveManagement = _waveManager;
        _waypoints = _waveManagement.GetWaypoints();
        SetSpeed(_waveManagement.GetMoveSpeed());
        _difficulty = _waveManagement.GetDifficulty();
    }
    protected void Start()
    {
        switch (_difficulty)
        {
            case 0:
               
                break;
            case 1:
                _health = (int)(_health * 1.1f);
                break;
            case 2:
                _health = (int)(_health * 1.3f);
                break;
            case 3:
                _health = (int)(_health * 1.5f);
                break;
            case 4:
                _health = (int)(_health * 2f);
                break;
            default:
                break;
        }

        _healthMax = _health;
        _meshRenderer = GetComponent<MeshRenderer>();
        foreach (GameObject explosion in _explosions)
        {
            explosion.SetActive(false);
        }
        foreach (GameObject thruster in _thrusters)
        {
            thruster.SetActive(true);
        }
        StartCoroutine(Shoot());
        transform.rotation = Quaternion.Euler(0, -180, 0);
        _animator = GetComponent<Animator>();
    }
    protected void SetAnimations(int type)
    {
        UIManager.Instance.UpdateScore(_score);
        _meshRenderer.enabled = false;
        if (type == 1)
        {
            _sparkle.SetActive(false);
        }
        if (type == 5)
        {
            UIManager.Instance.LevelComplete();
        }
        foreach (GameObject explosion in _explosions)
        {
            explosion.SetActive(true);
        }
        foreach (GameObject thruster in _thrusters)
        {
            thruster.SetActive(false);
        }
        StartCoroutine(TimeBeforDestroy());
        
    }
    
    protected IEnumerator TimeBeforDestroy()
    {
        
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }
    Vector3 targetPosition = new Vector3();
    float movementThisFrame;
    private bool _stopMoving = false;
    protected virtual void Move()
    {
        if (_health > 0)
            {
            if (waypointIndex < _waypoints.Count)
            {
                targetPosition = _waypoints[waypointIndex].transform.position;
                movementThisFrame = _speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                _animator.SetFloat("UpMovement", transform.position.y - targetPosition.y);
                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else if (waypointIndex >= _waypoints.Count)
            {
                waypointIndex = 3;
            }
        }
    }
    public abstract void TakeDamage(int damage);

    protected void Update()
    {
        if (!_stopMoving)
        {
            Move();
        }
    }
    protected abstract IEnumerator Shoot();
    protected void StopMoving(bool yes)
    {
        _stopMoving = yes;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Recycler")&& _score < 1000)
        {
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<PlayerController>().Damage();
            Destroy(this.gameObject);
        }
        
    }
}
