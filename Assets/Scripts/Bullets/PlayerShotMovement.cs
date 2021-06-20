using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotMovement : MonoBehaviour
{
    private float _speed= 8f;
    [SerializeField] private int _shotType;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ShotRecycler"))
        {
            Hide();
        }
        else if (other.CompareTag("Enemy"))
        {
            switch(_shotType)
            {
                case 0:
                other.GetComponent<BaseEnemy>().TakeDamage(3);
                Hide();
                    break;
                case 1:
                    other.GetComponent<BaseEnemy>().TakeDamage(5);
                    break;
                

                default:
                    break;
            }
        }
        
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }


    void Update()
    {
        if(_shotType != 2)
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
