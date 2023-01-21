using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    override
    public void Attack()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

    public override void attackAnimation()
    {
        throw new System.NotImplementedException();
    }
}
