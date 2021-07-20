using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotMovement : MonoBehaviour
{
    private float _speed=4, _damage=3;
    [SerializeField] private int _shotType;
    [SerializeField] private GameObject _explosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Recycler"))
        {
            Hide();
        }
        else if (other.CompareTag("Enemy"))
        {
            switch(_shotType)
            {
                case 100:
                other.GetComponent<BaseEnemy>().TakeDamage((int)(_damage));
                    GameObject miniexplosion = Instantiate(_explosion);
                    miniexplosion.transform.position = this.gameObject.transform.position;
                    Hide();
                    break;
                case 110:
                    other.GetComponent<BaseEnemy>().TakeDamage((int)(_damage));
                    miniexplosion = Instantiate(_explosion);
                    miniexplosion.transform.position = this.gameObject.transform.position;
                    Hide();
                    break;




                case 510:
                    other.GetComponent<BaseEnemy>().TakeDamage((int)(_damage));
                    miniexplosion = Instantiate(_explosion);
                    miniexplosion.transform.position = this.gameObject.transform.position;
                    Hide();
                    break;


                default:
                    break;
            }
        }
        
    }

    public void SetDamage(float damage){ _damage = damage; }
    public void SetSpeed(float speed) { _speed = speed; }
    public float GetDamage() { return _damage; }
    public float GetSpeed() { return _damage; }

    private void Hide()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
