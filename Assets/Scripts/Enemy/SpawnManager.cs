using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SpawnManager didn't load");
            }
            return _instance;
        }
    }

    [SerializeField] private List<WaveManager> _waves, _miniBossWaves, _bossWaves;
    [SerializeField] private GameObject _spawningSpot;
    [SerializeField] private GameObject _altEnemy;
    private float _difficultyLevel;
    private readonly WaitForSeconds _spawnWait = new WaitForSeconds(.5f), _minibossWait = new WaitForSeconds(5f), _secondWait = new WaitForSeconds(.2f);
    private bool _secondhalf = false;
    private GameObject enemy;
    [SerializeField] private TMP_Text _enemyCountValue;
    IEnumerator SpawnEnemiesInwave(WaveManager waveManager)
    {
        for (int loop = 0; loop < waveManager.GetNumberOfEnemies(); loop++)
        {
            _startingWave = false;
            if (waveManager.GetNumberOfEnemies() == 1 || loop < waveManager.GetNumberOfEnemies() - 1)
            {
                enemy = Instantiate(waveManager.GetEnemy(), waveManager.GetWaypoints()[0].position, Quaternion.identity);
                enemy.GetComponent<BaseEnemy>().SetWaveManager(waveManager);
                enemy.transform.parent = this.gameObject.transform;
                if (waveManager.GetNumberOfEnemies() == 1)
                {
                    yield return _minibossWait;

                }
            }
            else
            {
                enemy = Instantiate(_altEnemy, waveManager.GetWaypoints()[0].position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().SetWaveManager(waveManager);
                enemy.transform.parent = this.gameObject.transform;
            }
            
                yield return _spawnWait;
            
        }

    }


    private void Start()
    {
        _difficultyLevel = PlayerPreferences.GetDifficulty();
        UIManager.gameOver += IsGameOver;
        UIManager.restart += RestartSpawning;
        UIManager.reset += RestartSpawning;
        UnitPicker.startWaves += StartSpawning;

    }


    private void StartSpawning(int level, int wave)
    {
        _currentLevel = level;
        _currentWave = wave;
        StartCoroutine(SpawnAllEnemies());
    }
    private int _checkpoint;
    private bool _resetting = false;
    private void IsGameOver(int checkpoint)
    {
        _checkpoint = checkpoint;
        _resetting = true;
        foreach (Transform child in this.gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        StopAllCoroutines();
        
    }
    private void OnDestroy()
    {
        UIManager.gameOver -= IsGameOver;
        UIManager.restart -= RestartSpawning;
        UIManager.reset -= RestartSpawning;
        UnitPicker.startWaves -= StartSpawning;
    }
    private void RestartSpawning()
    {
        StartCoroutine(SpawnAllEnemies());
    }
    private WaitForSeconds _WFS = new WaitForSeconds(1f);
    private int _checkpointWave;
    private List<WaveManager> _enemyWave = new List<WaveManager>();
    private GameObject _spawndisplay;
    public int _currentWave,_currentLevel,_wavesToSpawn,_enemyCount, _randomWave;
    private bool _startingWave = false;

    public void EnemyDead()
    {
        _enemyCount--;
        _enemyCountValue.text = _enemyCount.ToString();
        if (!_startingWave)
        {
            if (_enemyCount <= 0)
            {
                UnitPicker.Instance.OpenWaveComplete();
            }
        }
    }
    private void SetEnemiesForCurrentWave()
    {
        _enemyCount = 0;
        _startingWave = true;
        _enemyWave.Clear();
        switch (_currentLevel)
        {
            case 1:
                switch (_currentWave)
                {
                    case 1:
                        _wavesToSpawn = 2;
                        for (int l=0; l < _wavesToSpawn;l++)
                        {
                            _randomWave = Random.Range(0, _waves.Count);
                            _enemyWave.Add(_waves[_randomWave]);
                            _enemyCount += _waves[_randomWave].GetNumberOfEnemies();
                        }
                        break;
                    case 2:
                        _wavesToSpawn = 3;
                        for (int l = 0; l < _wavesToSpawn; l++)
                        {
                            _randomWave = Random.Range(0, _waves.Count);
                            _enemyWave.Add(_waves[_randomWave]);
                            _enemyCount += _waves[_randomWave].GetNumberOfEnemies();
                        }
                        break;
                    case 3:
                        _wavesToSpawn = 5;
                        for (int l = 0; l < _wavesToSpawn; l++)
                        {
                            _randomWave = Random.Range(0, _waves.Count);
                            _enemyWave.Add(_waves[_randomWave]);
                            _enemyCount += _waves[_randomWave].GetNumberOfEnemies();
                        }
                        break;
                    case 4:
                        _wavesToSpawn = 7;
                        for (int l = 0; l < _wavesToSpawn; l++)
                        {
                            _randomWave = Random.Range(0, _waves.Count);
                            _enemyWave.Add(_waves[_randomWave]);
                            _enemyCount += _waves[_randomWave].GetNumberOfEnemies();
                        }
                        break;
                    case 5:
                        _wavesToSpawn = 2;
                        for (int l = 0; l < _wavesToSpawn; l++)
                        {
                            _randomWave = Random.Range(0, _waves.Count);
                            _enemyWave.Add(_waves[_randomWave]);
                            _enemyCount += _waves[_randomWave].GetNumberOfEnemies();
                        }
                        _randomWave = Random.Range(0, _bossWaves.Count);
                        _enemyWave.Add(_bossWaves[_randomWave]);
                        _enemyCount += _bossWaves[_randomWave].GetNumberOfEnemies();

                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (_currentWave)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (_currentWave)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (_currentWave)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                switch (_currentWave)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;

        }
        _enemyCountValue.text = _enemyCount.ToString();
    }
    IEnumerator SpawnAllEnemies()
    {

        
            SetEnemiesForCurrentWave();
            for (int waveIndex = 0; waveIndex < _enemyWave.Count; waveIndex++)
            {
                

                var currentWave = _enemyWave[waveIndex];
                if (currentWave.IsBoss())
                {
                    _spawndisplay = Instantiate(_spawningSpot.transform.GetChild(3).gameObject);
                }
                else
                {
                    _spawndisplay = Instantiate(_spawningSpot.transform.GetChild(currentWave.GetSpawnLoc()).gameObject);
                }
                Destroy(_spawndisplay, 1f);

                yield return _WFS;
                yield return StartCoroutine(SpawnEnemiesInwave(currentWave));

            }


        
    }
    private void Awake()
    {
        _instance = this;
    }

}
