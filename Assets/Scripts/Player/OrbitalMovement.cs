using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _orbitalRotation = new Vector3(0, 0, 1);
    [SerializeField] private int _UnitNumber;
    private void Update()
    {
        transform.RotateAround(_player.transform.position, _orbitalRotation, 30* Time.deltaTime);
        
    }
    private WaitForSeconds _startTimer = new WaitForSeconds(0.1f); 
    private Vector3 _shotDirection = new Vector3();
    private GameObject _shot;

    private float _damage,_incrementShot;
    IEnumerator Shoot()
    {
        yield return _startTimer;
        while (true)
        {

            
            switch (_UnitNumber)
            {
                case 100:
                    RequestShotPosition(0, 0);
                    RequestShot(100);
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
    public void SetBetweenShot(float value)
    {
        _incrementShot = value;
    }
    private void Start()
    {
        StartCoroutine(Shoot());
        _player = gameObject.transform.parent.gameObject;
    }
   
}
