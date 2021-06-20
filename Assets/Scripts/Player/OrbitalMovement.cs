using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Vector3 _orbitalRotation = new Vector3(0, 0, 1);
    private void Update()
    {
        transform.RotateAround(_player.transform.position, _orbitalRotation, 50* Time.deltaTime);
        
    }
    private WaitForSeconds _WFS = new WaitForSeconds(.2f);
    private Vector3 _shotDirection = new Vector3();
    private GameObject _shot;
    IEnumerator Shoot()
    {
        while (true)
        {
            _shotDirection.x = transform.position.x;
            _shotDirection.y = transform.position.y;
            _shot = ShotPoolManager.Instance.RequestShot(0);
            _shot.transform.position = _shotDirection;
            yield return _WFS;
        }
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }
}
