using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Burst.Intrinsics;
using System;

public class Player : NetworkBehaviour
{
    Shoulder shoulder;
    //BoxCollider2D coll;
    [SyncVar]Weapon weapon;
    [SerializeField] float _maxHealth = 100;
    [SerializeField] RangeWeapon gunPrefab;
    [SyncVar]float deathTimer;

    [SyncVar(hook =nameof(HealthChanged))]float _health;

    HealthBarInner healthBar;

    public Controlles controlles = new Controlles("Horizontal", "Jump", "Fire", "Vertical","Squat");
    // Start is called before the first frame update
    void Start()
    {
        shoulder= GetComponentInChildren<Shoulder>();
        Debug.Log(shoulder);
        //coll= GetComponent<BoxCollider2D>();
        healthBar= GetComponentInChildren<HealthBarInner>();
        Debug.Log("Player Start");
        if (isServer)
        {
            _health = _maxHealth;
            EquipWeapon();
        }
        else if (weapon != null)
        {
            Debug.Log("Equip on Start");
            EquipOnClients(weapon);
        }
    }

    void HealthChanged(float oldHealth, float newHealth)
    {
        if (healthBar != null)
        {
            healthBar.FillAmount = newHealth / _maxHealth;
        }
    }

    [Command]
    public void Attack()
    {
        weapon.Attack();
    }

    [Server]
    public void EquipWeapon()
    {
        Weapon mW = Instantiate(gunPrefab);
        NetworkServer.Spawn(mW.gameObject);
        mW.transform.SetParent(shoulder.transform);
        mW.gameObject.SetActive(true);
        mW.transform.localPosition = new Vector3(-0.018f, -0.419f, 0);
        EquipRPC(mW);
        this.weapon = mW;
    }

    [ClientRpc]
    void EquipRPC(Weapon w)
    {
        Debug.Log(w);
        EquipOnClients(w);
    }

    [Client]
    void EquipOnClients(Weapon newWeapon)
    {
        if(shoulder == null)
        {
            shoulder = GetComponentInChildren<Shoulder>();
        }
        this.weapon = newWeapon;
        newWeapon.transform.SetParent(shoulder.transform);
        newWeapon.gameObject.SetActive(true);
        newWeapon.transform.localPosition = new Vector3(-0.018f, -0.419f, 0);
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale= new Vector3(0.144f, 0.342f, 1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isLocalPlayer)
        {
            var vert = Input.GetAxis(controlles.MoveArm);
            var hor = Input.GetAxis(controlles.Move);


            
            if (weapon != null && !weapon.HasColdown())
            {                
                if (Input.GetButtonDown(controlles.Fire))
                {
                    Attack();
                }
            }

            shoulder.RotateArm(vert, hor);
        }

    }

    [Server]
    public float hit(float power)
    {
        print("hit " + power);
        _health -= power;
        if (_health < 0)
        {
            _health = 0;
            //Tod
            this.gameObject.SetActive(false);
            changeActive(false);
            transform.position = new Vector3(-26.45f, 0.19f, 0);
            _health = _maxHealth;
        }
        return _health;
    }

    [ClientRpc]
    void changeActive(bool value)
    {
        this.gameObject.SetActive(value);
    }
}
