using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEnemy
{
    [SerializeField]private int  _type,_shotDirection;
  
    private Vector2 _deathPosition;
    private GameObject _powerUp;
    public override void TakeDamage(int damage)
    {
        
        _health -= damage;
        if (_health <= 0)
        {
            _boxcollider = GetComponent<BoxCollider2D>();
            _boxcollider.enabled = false;
            if (_type == 1)
            {
                _powerUp = ShotPoolManager.Instance.RequestPowerUpDrop();
                _deathPosition.x = transform.position.x;
                _deathPosition.y = transform.position.y;
                _powerUp.transform.position = _deathPosition;
                
            }
            if (_type == 2)
            {
                UIManager.Instance.GiveCheckPoint(1);
            }
            if (_type == 3)
            {
                UIManager.Instance.GiveCheckPoint(2);
            }

            UIManager.Instance.UpdateScore(_score);
            SetAnimations(_type);
        }
       
    }
      
    private Vector2 _shootPosition = new Vector2();
    private GameObject _shot;

    protected override IEnumerator Shoot()
    {
        while (_meshRenderer.enabled)
        {
            _shootPosition.x = transform.position.x;
            _shootPosition.y = transform.position.y;
            if (_type == 0 || _type == 1)
            {
                _shot = ShotPoolManager.Instance.RequestShot(0);
                _shot.transform.position = _shootPosition;
                _shot.GetComponent<EnemyShotMovement>().GettingShotDirection(_shotDirection);
                yield return new WaitForSeconds(Random.Range(.8f, 1.2f));
            }
            else if (_type == 2||_type ==3)
            {
                _shot = ShotPoolManager.Instance.RequestShot(2);
                _shot.transform.position = _shootPosition;
                _shot = ShotPoolManager.Instance.RequestShot(2);
                _shootPosition.y += 0.1f;
                _shot.transform.position = _shootPosition;
                yield return new WaitForSeconds(Random.Range(.05f, .3f));

            }

        }
    }
}
