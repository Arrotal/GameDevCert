using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _type;
    private int _shotDirection;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _damage;

    private Vector3 _leftShot = new Vector3(-1, 0, 0),_topShot= new Vector3(0,-1,0);
    private Vector3 _miniBossShot = new Vector3(-1, 0, 0);
    void Start()
    {
        
        if (_type == 0)

            
        {
            _leftShot.y = Random.Range(-0.05f, 0.09f);
            
        }
        if (_type == 1)
        {
            _miniBossShot.y = Random.Range(-0.4f, 0.5f);
        }
    }
    public void GettingShotDirection(int shotDirection)
    {

        _shotDirection = shotDirection;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Recycler"))
        {
            Hide();
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Damage(_damage);
            GameObject miniexplosion = Instantiate(_explosion);
            miniexplosion.transform.position = this.gameObject.transform.position;
            Hide();
        }
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
void Update()
    {
        if (_type == 0)
            switch (_shotDirection)
            {
                case 0:
                    transform.Translate(_leftShot * _speed * Time.deltaTime);
                    break;
                case 1:
                    transform.Translate(_topShot * _speed /3* Time.deltaTime);
                    break;
                case 2:
                    transform.Translate(-_topShot * _speed /3 * Time.deltaTime);
                    break;
                case 3:
                    transform.Translate(-_leftShot * _speed *.8f* Time.deltaTime);
                    break;
            }
        
        else if (_type == 1)
        {
            transform.Translate(_miniBossShot * _speed * Time.deltaTime);
        }
    }
}
