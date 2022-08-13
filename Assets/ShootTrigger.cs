using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrigger : MonoBehaviour 
{
    Shooter shooter;
    private void Start()
    {
        shooter = GetComponentInParent<Shooter>();
    }
    public void Shoot()
    {
        shooter.Shootfunc();
    }
}
