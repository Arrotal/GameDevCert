using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _type;

    private Vector3 _leftShot = new Vector3(-1, 0, 0);
    private Vector3 _miniBossShot = new Vector3(-1,0,0);
    void Start()
    {
        if (_type == 0)
        {
            _leftShot.y = Random.Range(-0.1f, 0.2f);
        }
        if (_type == 1)
        {
            _miniBossShot.y = Random.Range(-0.4f, 0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Recycler"))
        {
            Hide();
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Damage();
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
            transform.Translate(_leftShot * _speed * Time.deltaTime);
        else if (_type == 1)
        {
            transform.Translate(_miniBossShot * _speed * Time.deltaTime);
        }
    }
}
