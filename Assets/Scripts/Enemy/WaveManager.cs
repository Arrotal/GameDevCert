using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class WaveManager : ScriptableObject
{

    [SerializeField] private GameObject _enemy, _path;
    [SerializeField] private int _numberOfEnemies, _moveSpeed, _difficulty, _spawnLoc;
    [SerializeField] private bool _bossType;

    public List<Transform> _waveWaypoints;
    public List<Transform> GetWaypoints()
    {
        _waveWaypoints.Clear();
        foreach (Transform child in _path.transform)
        {
            _waveWaypoints.Add(child);
        }
        return _waveWaypoints;
    }

    public GameObject GetEnemy() { return _enemy; }
    public int GetMoveSpeed(){ return _moveSpeed;}
    public int GetNumberOfEnemies() { return _numberOfEnemies; }
    public int GetDifficulty() { return _difficulty; }
    public int GetSpawnLoc() { return _spawnLoc; }
    public bool IsBoss() { return _bossType; }
}
