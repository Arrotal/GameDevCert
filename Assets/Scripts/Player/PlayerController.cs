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
    private int _health;
    private Animator _playerAnimator;

    [SerializeField] private GameObject _thrusters, _explosion;

    //Player Firing Controls

    private Coroutine _firingCoroutine;
    [SerializeField] private GameObject _orbital1, _orbital2, _losePowerUp;

    private MeshRenderer _meshRenderer;
    private Vector3 _startingPoint = new Vector3(-5,0,0);
    private void Start()
    {
        UIManager.restart += Restart;
        UIManager.gameOver += GameOver;
        UIManager.reset += Reset;
        _thrusters.SetActive(true);
        _explosion.SetActive(false);
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = true;
        _playerAnimator = GetComponent<Animator>();
        _health = 2;
        UIManager.Instance.UpdateLives(_health);
        _limitVector = new Vector2();
        UIManager.Instance.UpdateCurrentWeapon(0);
        transform.position = _startingPoint;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void GameOver(int checkpoint)
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void Reset()
    {

        GetComponent<BoxCollider2D>().enabled = true;
        _thrusters.SetActive(true);
        _explosion.SetActive(false);
        _meshRenderer.enabled = true;
        _health = 2;
        UIManager.Instance.UpdateLives(_health);
        UIManager.Instance.UpdateCurrentWeapon(0);
        transform.position = _startingPoint;
    }
    private void Restart()
    {

        GetComponent<BoxCollider2D>().enabled = true;
        _meshRenderer.enabled = true;
        _thrusters.SetActive(true);
        _powerUpState = PowerUpState.Base;
        transform.position = _startingPoint;
    }
    private void Update()
    {
        if (_meshRenderer.enabled)
        {
            MovementController();
            Fire();
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
        _playerAnimator.SetFloat("UpMovement", Mathf.Abs(_movementVector.y));
        _playerAnimator.SetFloat("ForwardMovement", _movementVector.x);
        transform.position = _limitVector;

    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(_firingCoroutine);
        }


    }

    private readonly WaitForSeconds _wFS = new WaitForSeconds(0.2f);
    private Vector2 _shotDirection = new Vector2();
    private GameObject _shot, _laser;
    private bool _altShot = false;
    IEnumerator FireContinously()
    {
        while (true)
        {
            if (_powerUpState >= PowerUpState.Base)
            {
                if (_altShot)
                {
                    _shotDirection.x = transform.position.x;
                    _shotDirection.y = transform.position.y - 0.2f;
                    _shot = ShotPoolManager.Instance.RequestShot(0);
                    _shot.transform.position = _shotDirection;
                    _altShot = false;
                }
                else if (!_altShot)
                {

                    _shotDirection.x = transform.position.x;
                    _shotDirection.y = transform.position.y + 0.2f;
                    _shot = ShotPoolManager.Instance.RequestShot(0);
                    _shot.transform.position = _shotDirection;
                    _altShot = true;
                }
                if (_powerUpState >= PowerUpState.Lasers)
                {
                    //laser stuff
                    _shotDirection.x = transform.position.x;
                    _shotDirection.y = transform.position.y;
                    _laser = ShotPoolManager.Instance.RequestShot(3);
                    _laser.transform.position = _shotDirection;


                    if (_powerUpState >= PowerUpState.Missiles)
                    {
                        //missile stuff

                        _shotDirection.x = transform.position.x;
                        _shotDirection.y = transform.position.y + Random.Range(-2, 3);
                        _laser = ShotPoolManager.Instance.RequestShot(4);
                        _laser.transform.position = _shotDirection;

                    }
                }
                yield return _wFS;
            }

        }
    }
    private void Respawn()
    {
        StartCoroutine(Respawned());
        _meshRenderer.enabled = false;
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
    }
    public void Damage()
    {
        if (!Invincibilty)
        {
            StartCoroutine(InvincibleTimer());
            if (_powerUpState == PowerUpState.Base)
            {
                //DeathState
                StopAllCoroutines();
                Respawn();

                _health--;
                UIManager.Instance.UpdateLives(_health);
            }
            else
            {
                PowerUp(false);
                //Do damaged animation
            }
        }
    }
    private WaitForSeconds _wfsInvinc = new WaitForSeconds(2f);
    IEnumerator InvincibleTimer()
    {
        if(_powerUpState != PowerUpState.Base)
        _losePowerUp.SetActive(true);
        Invincibilty = true;
        yield return _wfsInvinc;
        Invincibilty = false;
        if (_powerUpState != PowerUpState.Base)
            _losePowerUp.SetActive(false);
        StopCoroutine(InvincibleTimer());
    }
    public void PowerUp(bool yes)
    {
        if (yes)
        {

            switch (_powerUpState)
            {
                case PowerUpState.Base:
                    _powerUpState = PowerUpState.Lasers;
                    UIManager.Instance.UpdateCurrentWeapon(1);
                    break;
                case PowerUpState.Lasers:
                    _powerUpState = PowerUpState.Missiles;
                    UIManager.Instance.UpdateCurrentWeapon(2);
                    break;
                case PowerUpState.Missiles:
                    _powerUpState = PowerUpState.Options;
                    UIManager.Instance.UpdateCurrentWeapon(3);
                    _orbital1.SetActive(true);
                    _orbital2.SetActive(true);
                    break;
                case PowerUpState.Options:
                    UIManager.Instance.UpdateScore(1000);
                    break;
                default:
                    break;


            }
        }
        else
        {
            _powerUpState = PowerUpState.Base;
            UIManager.Instance.UpdateCurrentWeapon(0);
            _orbital1.SetActive(false);
            _orbital2.SetActive(false);
        }
    }
}
