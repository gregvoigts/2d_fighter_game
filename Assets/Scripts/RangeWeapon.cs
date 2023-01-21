using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using Mirror;

public class RangeWeapon :  Weapon
{
    [SerializeField] float weaponLength = 0.8f;
    Animator shotAnimator;

    private void Start()
    {
        shotAnimator = transform.Find("ShotAnimation").GetComponent<Animator>();
    }

    override
    public void Attack()
    {
        if (_coldown > 0)
            return;
        _coldown = coldown;
        Bullet bullet = BulletHandler.instance.RetrieveInstance(transform.position, transform.rotation * (transform.lossyScale.x <= 0 ? Quaternion.Euler(0,0,180) : Quaternion.Euler(0,0,0)));
        Debug.Log(transform.lossyScale);
        bullet.direction = transform.rotation * (transform.lossyScale.x<=0?Vector3.left:Vector3.right);
        bullet.gameObject.transform.position += bullet.direction * weaponLength;
        bullet.power = damage;
        ClientShot(bullet);
    }

    override
    public void attackAnimation()
    {
        shotAnimator.SetTrigger("Shot");
    }

    [ClientRpc]
    void ClientShot(Bullet bullet)
    {
        if(bullet == null) return;
        bullet.direction = transform.rotation * (transform.lossyScale.x <= 0 ? Vector3.left : Vector3.right);
        bullet.gameObject.transform.position = transform.position + bullet.direction * weaponLength;
        bullet.gameObject.transform.rotation = transform.rotation * (transform.lossyScale.x <= 0 ? Quaternion.Euler(0, 0, 180) : Quaternion.Euler(0, 0, 0));
        bullet.power = damage;
    }
}
