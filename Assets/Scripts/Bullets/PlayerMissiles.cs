using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissiles : MonoBehaviour
{
    private GameObject[] _targets;
    public GameObject _target;
    private float _enemyClose = 100, _rotateAmount;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private int _damage;
    void Start()
    {
        _damage = 2;
        StartCoroutine(HideSoon());
        GetTarget();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void GetTarget()
    {
        
            _targets = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject target in _targets)
            {
                if (Mathf.Abs(transform.position.x - target.transform.position.x) < _enemyClose&& !target.GetComponent<BaseEnemy>().IsDeadCheck())
                {
                    _enemyClose = target.transform.position.x;
                    _target = target;
                }
            }
        
    }

    private void FixedUpdate()
    {
        
       GetTarget();
       if (_target == null || _target.tag != "Enemy")
        { 
                transform.Translate(Vector2.up * Time.deltaTime * 10);
        }
        
        else if(_target.CompareTag("Enemy"))
        {
            _direction = (Vector2)_target.transform.position - (Vector2)_rigidbody.position;
            _direction.Normalize();
            _rotateAmount = Vector3.Cross(_direction, transform.up).z;
            _rigidbody.angularVelocity = -_rotateAmount * 800f;
            transform.Translate(Vector3.up * Time.deltaTime * 8);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy")&& !other.GetComponent<BaseEnemy>().IsDeadCheck())
        {
            other.GetComponent<BaseEnemy>().TakeDamage(_damage);
            Hide();
        }
    }

    IEnumerator HideSoon()
    {
        yield return new WaitForSeconds(2f);
        Hide();
    }
    private void Hide()
    {

        this.gameObject.SetActive(false);
    }

}
