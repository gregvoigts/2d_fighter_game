using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using Mirror;

public class RangeWeapon :  Weapon
{
    override
    public void Attack()
    {
        if (_coldown > 0)
            return;
            _coldown = coldown;
        Bullet bullet = BulletHandler.instance.getNew(transform.position, transform.rotation*Quaternion.Euler(0,0,90));
        bullet.direction = transform.rotation * Vector3.down;
        bullet.gameObject.transform.position += bullet.direction * transform.localScale.x;
        bullet.power = damage;
        ClientShot(bullet);
    }

    [ClientRpc]
    void ClientShot(Bullet bullet)
    {
        if(bullet == null) return;
        bullet.direction = transform.rotation * Vector3.down;
        bullet.gameObject.transform.position = transform.position + bullet.direction * transform.localScale.x;
        bullet.gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
        bullet.power = damage;
    }
}
