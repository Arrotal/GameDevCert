using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {

            if (_instance == null)
            {
                Debug.LogError("Missing UIManager");
            }
            return _instance;
        }

    }

    private float _difficulty;
    [SerializeField] private TMP_Text _scoreNumber, _currentPower, _gameOverScore,_pauseScore, _levelScore;
    [SerializeField] private GameObject _gameOverCanvas, _levelCompleteCanvas, _gameCanvas, _pauseCanvas,_bossWarning;
    private string format = "000000.##";
    private int _score;
    private int _previousScore;
    [SerializeField]private List<GameObject> _lives;

    private int _checkpoint = 0;


    private bool _pause;

    public void UpdateScore(int score)
    {
        _score += score;
        StartCoroutine(TickingScore());
    }
    private WaitForEndOfFrame _WFEF = new WaitForEndOfFrame();
    IEnumerator TickingScore()
    {
        while (_previousScore <= _score)
        {
            _scoreNumber.text = _previousScore.ToString(format);
            yield return _WFEF;
            _previousScore++;
        }
    }
    public void UpdateCurrentWeapon(int weapon)
    {
        switch (weapon)
        {
            case 0:
                _currentPower.text = "Dual Guns";
                break;
            case 1:
                _currentPower.text = "Laser";
                break;
            case 2:
                _currentPower.text = "Missiles";
                break;
            case 3:
                _currentPower.text = "Orbitals";
                break;
            default:
                break;
        }    
    }
    public delegate void Gameover(int checkPoint);
    public static event Gameover gameOver;
    private int _livesLeft;
    public void UpdateLives(int lives)
    {
        if (lives < 2)
            
        PlayerDeath(lives);
        if (lives >= 0)
        {
            _lives[0].SetActive(false);
            _lives[1].SetActive(false);
            _lives[2].SetActive(false);
            for (int loop = 0; loop <= lives; loop++)
            {
                _lives[loop].SetActive(true);
            }
        }
        else
        {
            //Wait for explosion then Gameover Screen
        }
    }
    public delegate void Restart();
    public static event Restart restart;
    [SerializeField] private GameObject _fadeToBlack;
    IEnumerator RespawnTime()
    {
        _fadeToBlack.SetActive(true);
        gameOver(_checkpoint);

        yield return new WaitForSeconds(2);
        restart();
        
        _fadeToBlack.SetActive(false);
    }
    private bool _gameOver = false;


    private void PlayerDeath(int lives)
    {

        if (lives < 0)
        {

            gameOver(_checkpoint);
            _gameOver = true;
            StartCoroutine(FlashMenu());
            _gameOverScore.text = _scoreNumber.text;
            _gameCanvas.SetActive(false);
        }
        else if(lives>=0)
        {
            StopAllCoroutines();
            StartCoroutine(RespawnTime());
        }
        
    }
    public void GiveCheckPoint(int checkpoint)
    {
        _checkpoint = checkpoint;
    }
    private bool _levelComplete= false;
    public void LevelComplete()
    {
        _levelComplete = true;
        _gameOver = true;
        StartCoroutine(WaitForExplosions());
       
    }
    IEnumerator WaitForExplosions()
    {
        _levelScore.text = _score.ToString();
        _levelCompleteCanvas.SetActive(true);
        _gameCanvas.SetActive(false);
        yield return new WaitForSeconds(.7f);

        gameOver(0);
    }

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _pause = false;
        UpdateScore(0);
        _difficulty = PlayerPreferences.GetDifficulty();

    }
    private void Awake()
    {
        _instance = this;
    }
    private WaitForSeconds _WFSF = new WaitForSeconds(.5f);
    private bool _menuOn = false, _bosstimerdone=false;
    private float _bossTimer =4f;
    [SerializeField]private AudioClip _bossAudio;
    IEnumerator FlashMenu()
    {
        while (true)
        {
            
            _gameOverCanvas.SetActive(_menuOn);
            _menuOn = !_menuOn;
            yield return _WFSF;
        }

    }
    [SerializeField]private Slider _bossHealth;
    [SerializeField]private GameObject _bossCanvas;
    public void BossSpawning(int health)
    {
        audioSource.Play();
        StartCoroutine(BossWarning());
        _bossCanvas.SetActive(true);
        _bossHealth.maxValue = health;
        _bossHealth.value = health;
    }
    private WaitForSeconds _WFSB = new WaitForSeconds(.6f);
    IEnumerator BossWarning()
    {
        while (!_bosstimerdone)
        {
            
            if (_bossTimer < 0)
            {

                _bosstimerdone = true;
            }
                _bossWarning.SetActive(!_bossWarning.activeSelf);
            yield return _WFSB;
            if (!audioSource.isPlaying)
            {
                _bosstimerdone = true;
            }
        }
        _bossWarning.SetActive(false);
    }

    public delegate void Reset();
    public static event Reset reset;

    public void UpdateBossHealth(float health)
    {
        _bossHealth.value = health;
    }

    private void Update()
    {
        if (_gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOver)
        {
            if (!_pause)
            { 
            Time.timeScale = 0;
                _pause = true;
                _gameCanvas.SetActive(false);
                _pauseCanvas.SetActive(true);
                _pauseScore.text = _scoreNumber.text;
                return;
            }
            if (_pause)
            {
                Time.timeScale = 1;
                _pause = false;
                _gameCanvas.SetActive(true);
                _pauseCanvas.SetActive(false);
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && _levelComplete && _difficulty<5)
        {
            PlayerPreferences.SetMasterDifficulty(_difficulty+1);
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Q)&&_pause)
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(0);
        }
    }
}
