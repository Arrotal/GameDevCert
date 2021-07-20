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
    [SerializeField] private List<GameObject> _UnitShots;
    [SerializeField] private List<GameObject> _UnitExtraShots;

    [SerializeField] private GameObject _enemyShot;
    private List<GameObject> _enemyShotPool = new List<GameObject>();

    [SerializeField] private GameObject _enemyMiniBossShot;
    private List<GameObject> _enemyMiniBossShotPool = new List<GameObject>();


    [SerializeField] private GameObject _powerUpDrop;

    [SerializeField] private GameObject _bossLaser;

    private GameObject _parentContainer;
    void Start()
    {
        UnitPicker.completedWave += ClearShots;
    }

    private void ClearShots()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    //private void GenerateShots(List<GameObject> shotList, GameObject shotName, int shotAmount)
    //{
    //    for (int s = 0; s < shotAmount; s++)
    //    {
    //        GameObject shot = Instantiate(shotName, _parentContainer.transform);
    //        shot.SetActive(false);
    //        shotList.Add(shot);
    //    }

    //}
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
 
            //---------------------------------------//
            //Enemy Shots
            case 0:
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

            case 1:
                //Enemy Basic Alternate Shot
                return null;

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
                //Enemy shots
                return null;

            case 4:
                //Enemy Shots
                return null;



            //---------------------------------------//
            //Unit Shots
            //Fighter Pure Units
            case 100:
                //Raider shot
                GameObject RaiderShot = Instantiate(_UnitShots[0], _parentContainer.transform);
                RaiderShot.SetActive(true);
                return RaiderShot;
            case 101:
                //Viper
                return null;
            case 102:
                //Aurora
                return null;
            case 103:
                //Bebop
                return null;

            //Bomber Pure Units
            case 110:
                //Zero Shot
                GameObject ZeroShot = Instantiate(_UnitShots[1], _parentContainer.transform);
                ZeroShot.SetActive(true);
                return ZeroShot;
            case 111:
                //Rocket
                return null;
            case 112:
                //Icarus
                return null;
            case 113:
                //Excalibur
                return null;



                //Extra Projectiles
            case 510:
                //Zero Extra Shot
                GameObject explosionShot = Instantiate(_UnitExtraShots[0], _parentContainer.transform);
                explosionShot.SetActive(true);
                return explosionShot;


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
