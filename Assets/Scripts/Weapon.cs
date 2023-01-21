using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : NetworkBehaviour
{
    [SerializeField] protected float coldown = 1.0f;
    [SyncVar] protected float _coldown = 0;
    [SerializeField] protected float damage = 15.0f;
    public Animator animator;

    abstract public void Attack();

    abstract public void attackAnimation();

    // Update is called once per frame
    private void Update()
    {
        if (isServer)
        {
            if (_coldown > 0)
            {
                _coldown -= Time.deltaTime;
            }
        }
    }

    public bool HasColdown()
    {
        return _coldown > 0;
    }
}
