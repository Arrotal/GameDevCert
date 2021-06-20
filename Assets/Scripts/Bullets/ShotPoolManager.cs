using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPoolManager : MonoBehaviour
{
    private static ShotPoolManager _instance;
    public static ShotPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Shot Manager is not Loaded");
            }
            return _instance;
        }
    }
    [SerializeField] private GameObject _playerShot;
    [SerializeField] private List<GameObject> _playerShotPool;

    [SerializeField] private GameObject _enemyShot;
    [SerializeField] private List<GameObject> _enemyShotPool;

    [SerializeField] private GameObject _enemyMiniBossShot;
    [SerializeField] private List<GameObject> _enemyMiniBossShotPool;

    [SerializeField] private GameObject _playerLaser;
    [SerializeField] private List<GameObject> _playerLaserPool;

    [SerializeField] private GameObject _playerMissiles;
    [SerializeField] private List<GameObject> _playerMissilesPool;

    [SerializeField] private GameObject _powerUpDrop;

    [SerializeField] private GameObject _bossLaser;

    private GameObject _parentContainer;
    void Start()
    {
        UIManager.gameOver += ClearShots;
        GeneratePlayerShots(_playerShotPool, _playerShot, 20);
        GeneratePlayerShots(_enemyShotPool, _enemyShot, 30);
        GeneratePlayerShots(_enemyMiniBossShotPool, _enemyMiniBossShot, 10);
        GeneratePlayerShots(_playerLaserPool, _playerLaser, 20);
        GeneratePlayerShots(_playerMissilesPool, _playerMissiles, 10);
    }

    private void ClearShots(int redundant)
    {
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false) ;
        }
    }
    private void GeneratePlayerShots(List<GameObject> shotList, GameObject shotName, int shotAmount)
    {
        for (int s = 0; s < shotAmount; s++)
        {
            GameObject shot = Instantiate(shotName, _parentContainer.transform);
            shot.SetActive(false);
            shotList.Add(shot);
        }

    }
    public GameObject RequestBossLaser(bool isTracking)
    {
        
        GameObject bosslaser = Instantiate(_bossLaser, _parentContainer.transform);
        if (isTracking)
        {
            bosslaser.GetComponent<BossLaser>().IsTracking();
        }
        return bosslaser;
    }
    public GameObject RequestPowerUpDrop()
    {
        GameObject powerUp = Instantiate(_powerUpDrop, _parentContainer.transform);
        return powerUp;
    }
    public GameObject RequestShot(int shotType)
    {
        switch (shotType)
        {
            case 0:
                //Player Basic Shot
              foreach (var shot in _playerShotPool)
                {
                     if (!shot.activeInHierarchy)
                        {
                          shot.SetActive(true);
                            return shot;
                        }

               }
              GameObject NewShot = Instantiate(_playerShot, _parentContainer.transform);
              NewShot.SetActive(true);
              return NewShot;

            case 1:
                //Enemy Basic Shot
                foreach (var shot in _enemyShotPool)
                {
                    if (!shot.activeInHierarchy)
                    {
                        shot.SetActive(true);
                        return shot;
                    }

                }
                GameObject NewEShot = Instantiate(_enemyShot, _parentContainer.transform);
                NewEShot.SetActive(true);
                return NewEShot;
            case 2:
                //Enemy Mini Boss Shot
                foreach(var shot in _enemyMiniBossShotPool)
                {
                    if (!shot.activeInHierarchy)
                    {
                        shot.SetActive(true);
                        return shot;
                    }
                
                }
                GameObject NewMShot = Instantiate(_enemyMiniBossShot, _parentContainer.transform);
                NewMShot.SetActive(true);
                return NewMShot;

            case 3:
                //Player Laser
                foreach (var shot in _playerLaserPool)
                {
                    if (!shot.activeInHierarchy)
                    {
                        shot.SetActive(true);
                        return shot;
                    }

                }
                GameObject laser = Instantiate(_playerLaser, _parentContainer.transform);
                laser.SetActive(true);
                return laser;

            case 4:
                foreach (var shot in _playerMissilesPool)
                {
                    if (!shot.activeInHierarchy)
                    {
                        shot.SetActive(true);
                        return shot;
                    }
                }
                GameObject missile = Instantiate(_playerMissiles, _parentContainer.transform);
                missile.SetActive(true);
                return missile;
            default:
              return null;
    }

     
}

    private void Awake()
    {
        _instance = this;
        _parentContainer = this.gameObject;
    }
}
