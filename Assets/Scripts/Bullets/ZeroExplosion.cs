using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroExplosion : MonoBehaviour
{
    private PlayerShotMovement _shotMover;
    private int _amountOfShots =4;
    private void OnDestroy()
    {
        for (int l = 0; l <=_amountOfShots; l++)
        {
            GameObject explodeshot = ShotPoolManager.Instance.RequestShot(510);
           explodeshot.transform.Rotate(new Vector3(0,0,1), ((360 / 4 * l) + 45));
            explodeshot.transform.position = transform.position;
            _shotMover = explodeshot.GetComponent<PlayerShotMovement>();
            
        }
        
    }

    public void SetAmountOfShots(int shots)
    {
        _amountOfShots = shots;
    }
    private PlayerShotMovement _thisShot;
    private void Start()
    {
        _thisShot = GetComponent<PlayerShotMovement>();
    }
}
