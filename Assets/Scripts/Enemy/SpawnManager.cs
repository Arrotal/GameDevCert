using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<WaveManager> _difficulty1, _difficulty2, _difficulty3, _difficulty4, _difficulty5;
    [SerializeField] private GameObject _altEnemy;
    private float _difficultyLevel;
    private readonly WaitForSeconds _spawnWait = new WaitForSeconds(.5f), _minibossWait = new WaitForSeconds(10f);

    private GameObject enemy;
    IEnumerator SpawnEnemiesInwave(WaveManager waveManager)
    {
        for (int loop = 0; loop < waveManager.GetNumberOfEnemies(); loop++)
        {
            if (waveManager.GetNumberOfEnemies() == 1|| loop < waveManager.GetNumberOfEnemies()-1)
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
    private void RestartSpawning()
    {
        StartCoroutine(SpawnAllEnemies());
    }
    private WaitForSeconds _WFS = new WaitForSeconds(1f);
    private int _checkpointWave;
    private List<WaveManager> _waveDifficulty;
    IEnumerator SpawnAllEnemies()
    {
        yield return _WFS;
        if (_resetting)
        {
            switch (_checkpoint)
            {
                case 0:
                    _checkpointWave = 0;
                    break;
                case 1:
                    _checkpointWave = 6;
                    break;
                case 2:
                    _checkpointWave = 12;
                    break;
                default:
                    break;
            }
        }
        switch (_difficultyLevel)
        {
            case 1:
                _waveDifficulty = _difficulty1;
                break;
            case 2:
                _waveDifficulty = _difficulty2;
                break;
            case 3:
                _waveDifficulty = _difficulty3;
                break;
            case 4:
                _waveDifficulty = _difficulty4;
                break;
            case 5:
                _waveDifficulty = _difficulty5;
                break;
            default:
                _waveDifficulty = _difficulty1;
                break;

        }
            for (int waveIndex = 0; waveIndex < _waveDifficulty.Count; waveIndex++)
            {
            if(_resetting)
            waveIndex = _checkpoint;
            _resetting = false;
           
                var currentWave = _waveDifficulty[waveIndex];
                yield return StartCoroutine(SpawnEnemiesInwave(currentWave));
            
            }
        
    }


}
