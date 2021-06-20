using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseEnemy
{
    [SerializeField] private List<GameObject> _smoke;
    public override void TakeDamage(int damage)
    {

        _health -= damage;

        UIManager.Instance.UpdateBossHealth(_health);
        if (_health <450 && !_explosions[2].activeInHierarchy)
        {
            _explosions[2].SetActive(true);
            _smoke[2].SetActive(true);
        }
        else if (_health < 375 && !_explosions[3].activeInHierarchy)
        {
            _explosions[3].SetActive(true);
            _smoke[3].SetActive(true);
        }
        else if(_health < 300 && !_explosions[0].activeInHierarchy)
        {
            _explosions[0].SetActive(true);
            _smoke[0].SetActive(true);
        }
        else if (_health < 225 && !_explosions[5].activeInHierarchy)
        {
            _explosions[5].SetActive(true);
            _smoke[5].SetActive(true);
        }
        else if (_health < 150 && !_explosions[1].activeInHierarchy)
        {
            _explosions[1].SetActive(true);
            _smoke[1].SetActive(true);
        }
        else if (_health < 75 && !_explosions[4].activeInHierarchy)
        {
            _explosions[4].SetActive(true);
            _smoke[4].SetActive(true);
        }
 
        
        if (_health <= 0)
        {
            SetAnimations(5);

            _boxcollider = GetComponent<BoxCollider2D>();
            _boxcollider.enabled = false;
        }
    }
    private float _laserCooldown= 10f;
    private bool _laserFiring = false, _laserFired = false, _tracking =false;
    private float _laserOffset = 2;
    private Vector3 _laserVector = new Vector3();
    private Vector2 _shootPosition = new Vector2();
    private GameObject _shot,_laser;
    protected override IEnumerator Shoot()
    {
        while (_meshRenderer)
        {
            _shootPosition.x = transform.position.x;
            _shootPosition.y = transform.position.y;
            _shot = ShotPoolManager.Instance.RequestShot(2);
            _shot.transform.position = _shootPosition;
            _shootPosition.y -= 0.1f;
            _shot = ShotPoolManager.Instance.RequestShot(2);
            _shot.transform.position = _shootPosition;
            _shootPosition.x-= 0.1f;
            _shot = ShotPoolManager.Instance.RequestShot(2);
            _shot.transform.position = _shootPosition;
            _shootPosition.x -= 0.2f;
            _shot = ShotPoolManager.Instance.RequestShot(1);
            _shot.transform.position = _shootPosition;
            _shootPosition.y -= 0.2f; 
            _shot = ShotPoolManager.Instance.RequestShot(1);
            _shot.transform.position = _shootPosition;
            _shootPosition.x -= 0.2f;
            _shot = ShotPoolManager.Instance.RequestShot(2);
            _shot.transform.position = _shootPosition;
            _shootPosition.y -= 0.2f;
            _shot = ShotPoolManager.Instance.RequestShot(2);
            _shot.transform.position = _shootPosition;
            if (!_laserFiring)
            {

                yield return new WaitForSeconds(.5f);
                _laserCooldown -= .5f;
                if (_laserCooldown == 0)
                {
                    _laserFiring = true;
                }
            }
            else if (_laserFiring)
            {

                if (_health <= _healthMax / 2)
                { 
                    if (!_laserFired)
                
                    {
                        _tracking = true;
                    
                        _laserVector.x = transform.position.x -_laserOffset;
                        _laserVector.y = transform.position.y;
                        _laser = ShotPoolManager.Instance.RequestBossLaser(_tracking);
                        _laser.transform.position = _laserVector;

                        
                        StopMoving(true);
                        _laserFired = true;
                       
                        yield return new WaitForSeconds(4f);
                    }
                    yield return new WaitForSeconds(4f);
     
                    

                    yield return new WaitForSeconds(1f);
                    StopMoving(false);
                    _laserFiring = false;
                    _laserFired = false;
                    _laserCooldown = 10f;
                }
                else
                {
                    if (!_laserFired)

                    {
                        _tracking = false;

                        _laserVector.x = transform.position.x - _laserOffset;
                        _laserVector.y = transform.position.y;
                        _laser = ShotPoolManager.Instance.RequestBossLaser(_tracking);
                        _laser.transform.position = _laserVector;


                        StopMoving(true);
                        _laserFired = true;

                        yield return new WaitForSeconds(4f);
                    }
                    yield return new WaitForSeconds(4f);
          


                    yield return new WaitForSeconds(1f);
                    StopMoving(false);
                    _laserFiring = false;
                    _laserFired = false;
                    _laserCooldown = 20f;
                }
            }
        }
    }
    private void Awake()
    {
        UIManager.Instance.BossSpawning(_health);
    }

}
