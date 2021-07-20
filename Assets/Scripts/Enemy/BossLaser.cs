using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Damage(_damage);
        }
        if (collision.CompareTag("Shot"))
        {
            collision.gameObject.SetActive(false);
        }

    }
    private Rigidbody2D _rigidbody;
    private Vector2 _laserLookDirection;
    private float _rotatelaser;
    private bool _tracking=false;
    private Transform _player;

    private void FixedUpdate()
    {
        if (_tracking)
        {
            _laserLookDirection = (Vector2)_player.position - (Vector2)_rigidbody.position;
            _laserLookDirection.Normalize();
            _rotatelaser = Vector3.Cross(_laserLookDirection, transform.right).z;
            _rigidbody.angularVelocity = -_rotatelaser * Time.deltaTime * 8;
        }
    }
    public void StopTracking()
    {
        _tracking = false;
    }
    public void ShakeTime()
    {
        CameraShake.Instance.Shake();
    }
    public void DestroyThis(int yes)
    {
        UIManager.gameOver -= DestroyThis;
        Destroy(this.gameObject);
    }
    public void IsTracking()
    {
        _tracking = true;
    }
    private void Start()
    {
        UIManager.gameOver += DestroyThis;
        _player = FindObjectOfType<PlayerController>().gameObject.transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

}
