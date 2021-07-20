using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum PowerUpState
    {
        Base,
        Lasers,
        Missiles,
        Options
    }

    //Player Movement Controls
    private float _hInput, _vInput;
    private readonly float _hMax = 0, _hmin = -8.87f, _vMax = 3.7f, _vMin = -3.7f, _speed = 5f;
    private Vector2 _movementVector = new Vector2(), _limitVector;
    public PowerUpState _powerUpState;
    private float _health, _maxHealth;
    private Animator _playerAnimator;

    [SerializeField] private GameObject _thrusters, _explosion, _powerUpEffect;


    //Player Firing Controls

    private Coroutine _firingCoroutine;
    [SerializeField] private GameObject _orbital1, _orbital2, _losePowerUp;
    private bool canShoot = true;
    private MeshRenderer _meshRenderer;
    private BoxCollider2D _boxCollider2D;
    private Vector3 _startingPoint = new Vector3(-5,0,0);
    private void Start()
    {
        _losePowerUp.SetActive(false);
        UIManager.restart += Restart;
        UIManager.gameOver += GameOver;
        UIManager.reset += Reset;
        _thrusters.SetActive(true);
        _explosion.SetActive(false);
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = true;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.enabled = true;
        _playerAnimator = GetComponent<Animator>();
        _maxHealth = 10f;
        _health = _maxHealth;
        UIManager.Instance.UpdateHealth(_health);
        _limitVector = new Vector2();
        transform.position = _startingPoint;
        GetComponent<BoxCollider2D>().enabled = true;
        _powerUpEffect.SetActive(false);
    }
    private bool _noMoving = false;
    private void GameOver(int checkpoint)
    {
        _noMoving = true;
    }
    private void Reset()
    {
        _noMoving = false;
        canShoot = true;
        _losePowerUp.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
        _thrusters.SetActive(true);
        _explosion.SetActive(false);
        _meshRenderer.enabled = true;
        _boxCollider2D.enabled = true;
        _health = 2;
        UIManager.Instance.UpdateHealth(_health);
        transform.position = _startingPoint;
    }
    private void Restart()
    {
        _noMoving = false;
        canShoot = true;
        _losePowerUp.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
        _meshRenderer.enabled = true;
        _boxCollider2D.enabled = true;
        _thrusters.SetActive(true);
        _powerUpState = PowerUpState.Base;
        transform.position = _startingPoint;
    }
    private void Update()
    {
        if (!_noMoving)
        {
            MovementController();
        }
    }

    private void MovementController()
    {
        _hInput = Input.GetAxisRaw("Horizontal");
        _vInput = Input.GetAxisRaw("Vertical");
        _movementVector.x = _hInput * _speed * Time.deltaTime;
        _movementVector.y = _vInput * _speed * Time.deltaTime;
        transform.Translate(_movementVector);
        _limitVector.x = Mathf.Clamp(transform.position.x, _hmin, _hMax);
        _limitVector.y = Mathf.Clamp(transform.position.y, _vMin, _vMax);
        _playerAnimator.SetFloat("UpMovement",_movementVector.y);
        _playerAnimator.SetFloat("ForwardMovement", _movementVector.x);
        transform.position = _limitVector;

    }

    
    private void Respawn()
    {
        canShoot = false;
        _losePowerUp.SetActive(false);
        StartCoroutine(Respawned());
        _meshRenderer.enabled = false;
        _boxCollider2D.enabled = false ;
        _thrusters.SetActive(false);
        _explosion.SetActive(true);
    }

    private bool Invincibilty;
    IEnumerator Respawned()
    {
        yield return new WaitForSeconds(0.8f);
        _explosion.SetActive(false);
        Invincibilty = false;
        _meshRenderer.enabled = true;
        _boxCollider2D.enabled = true;
    }
    public void Damage(float damage)
    {
        if (!Invincibilty)
        {
            CameraShake.Instance.Shake();
            StartCoroutine(InvincibleTimer());
            if (_powerUpState == PowerUpState.Base)
            {
                //DeathState
                StopAllCoroutines();
                Respawn();

                _health -= damage;
                UIManager.Instance.UpdateHealth(_health);
            }
            else
            {
                //TakeDamage
            }
        }
    }
    private WaitForSeconds _wfsInvinc = new WaitForSeconds(2f);
    IEnumerator InvincibleTimer()
    {
        _losePowerUp.SetActive(true);
        Invincibilty = true;
        yield return _wfsInvinc;
        Invincibilty = false;
            _losePowerUp.SetActive(false);
        StopCoroutine(InvincibleTimer());
    }
   
}
