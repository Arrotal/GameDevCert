using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _orbitalRotation = new Vector3(0, 0, 1);
    [SerializeField] private int _UnitNumber;
    public float _shotSize;
    private void Update()
    {
        transform.RotateAround(_player.transform.position, _orbitalRotation, 30* Time.deltaTime);
        
    }
    private WaitForSeconds _startTimer = new WaitForSeconds(0.1f); 
    private Vector3 _shotDirection = new Vector3();
    private GameObject _shot;
    private float _unitSize = 1;

    private float _damage,_incrementShot;

    public void SetShotSize(float percentage)
    {
        _shotSize += percentage;
    }

    IEnumerator Shoot()
    {
        yield return _startTimer;
        while (true)
        {

            
            switch (_UnitNumber)
            {
                case 100:
                    ShotTime(0, 0, 100);
                    break;

                case 101:
                    RequestShotPosition(0, 0.1f);
                    RequestShot(100);

                    RequestShotPosition(0, -0.1f);
                    RequestShot(100);
                    break;



                case 110:
                    RequestShotPosition(0, 0);
                    RequestShot(110);
                    yield return null;
                    break;


                default:
                  yield return null;
                    break;
        }
            yield return new WaitForSeconds(_incrementShot);
        }
    }
    
    private float _randExtraShot,_extraShotChance=0;
    private bool _extraShot;
    private void ShotTime(float x, float y, int unit)
    {
       
            for (int l = 1; l < _shotAmount; l++)
            {
                RequestShotPosition(x, y+(0.2f*l));
                RequestShot(unit);
            if (_extraShotChance >= 0.1f)
            {

                _randExtraShot = Random.Range(0f, 1f);
                Debug.Log(_randExtraShot + " " + _extraShotChance);
                if (_randExtraShot <= _extraShotChance)
                {
                    Debug.LogWarning("Extra Shot Spawned"+_randExtraShot);
                    RequestShotPosition(x, y - 1f);
                    RequestShot(unit);
                }
            }
        }

            
            
        
    }
    private void RequestShotPosition(float x, float y)
    {
        _shotDirection.x = transform.position.x + x;
        _shotDirection.y = transform.position.y + y;
    }
    private PlayerShotMovement _shotScript;
    private void RequestShot(int shotType)
    {
        _shot = ShotPoolManager.Instance.RequestShot(shotType);
        _shot.transform.position = _shotDirection;
        _shotScript = _shot.GetComponent<PlayerShotMovement>();
        _shotScript.SetSpeed(_shotSpeed);
        _shotScript.SetDamage(_damage);
        _shotScript.SetShotSize(_shotSize);

        GetComponent<UniqueBase>().DoUniqueThingOnShot(_shotScript);
    }
    private float  _shotSpeed;
    public void SetShotDamage(float damage)
    {
        _damage = damage ;
    }
    public void SetShotSpeed(float speed)
    {
        _shotSpeed = speed;
    }
    public float _shotAmount = 1;
    public void SetShotAmount(float amount)
    {
        _shotAmount = amount;
        
        _extraShotChance = _shotAmount % 1;
    }
  
    public void SetBetweenShot(float value)
    {
        _incrementShot = value;
    }
    public float GetBetweenShot()
    {
        return _incrementShot;
    }
    private void Start()
    {
        StartCoroutine(Shoot());
        _player = gameObject.transform.parent.gameObject;
    }
    public void SetScale(float size)
    {
        _unitSize = size;
        transform.localScale= new Vector3(_unitSize, _unitSize, _unitSize);
    }
}
