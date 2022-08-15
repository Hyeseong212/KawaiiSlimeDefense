using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrigger : MonoBehaviour 
{
    [SerializeField] Shooter shooter;
    public void Shoot()
    {
        shooter.Shootfunc();
    }
}
