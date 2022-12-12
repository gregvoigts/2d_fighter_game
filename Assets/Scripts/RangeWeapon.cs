using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    
    [SerializeField] float coldown = 1.0f;
    float _coldown = 0;
    public string fireButton;
    [SerializeField] float damage = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        fireButton = GetComponentInParent<Player>().controlles.Fire;
        Debug.Log(fireButton);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_coldown > 0)
        {
            _coldown -= Time.deltaTime;
        }        
        else if (Input.GetButtonDown(fireButton))
        {
            Debug.Log("shoot");
            _shoot();
            _coldown = coldown;
        }
    }

    void _shoot()
    {
        Bullet bullet = BulletHandler.instance.RetrieveInstance(transform.position, transform.rotation*Quaternion.Euler(0,0,90));
        bullet.direction = transform.rotation * Vector3.down;
        bullet.gameObject.transform.position += bullet.direction * transform.localScale.x;
        bullet.power = damage;
    }
}
