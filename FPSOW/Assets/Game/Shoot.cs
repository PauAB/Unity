using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Command
{
    IEnumerator shootCoroutine;

    public void SetShoot(IEnumerator shoot)
    {
        shootCoroutine = shoot;
    }
}
