using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotMovement : MonoBehaviour
{
    private float _speed=4, _damage=3;
    public float _shotSize = 1;
    public Vector3 _scaleVector ;
    [SerializeField] private int _shotType;
    [SerializeField] private GameObject _explosion;
    private bool _doesHide = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Recycler"))
        {
            _doesHide = true;
            Hide();
        }
        else if (other.CompareTag("Enemy"))
        {
            switch(_shotType)
            {
                case 100:
                    Explosion(other);
                    break;
                case 110:
                    Explosion(other);
                    break;

                case 510:
                    Explosion(other);
                    break;


                default:
                    break;
            }
        }
        
    }
    private void Explosion(Collider2D other)
    {
        other.GetComponent<BaseEnemy>().TakeDamage((int)(_damage));
        GameObject miniexplosion = Instantiate(_explosion);
        miniexplosion.transform.position = this.gameObject.transform.position;
        Hide();
    }
    public void SetShotSize(float percentage)
    {
        _shotSize= percentage;
        _scaleVector.x = transform.localScale.x * _shotSize;
        _scaleVector.y = transform.localScale.y * _shotSize;
        this.gameObject.transform.localScale=_scaleVector;
    }
    public void SetDamage(float damage){ _damage = damage; }
    public void SetSpeed(float speed) { _speed = speed; }
    public float GetDamage() { return _damage; }
    public float GetSpeed() { return _damage; }

    public void DestroyOnHit(bool doesit)
    {
        _doesHide = doesit;
    }
    private void Hide()
    {
        if(_doesHide)
        Destroy(this.gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
