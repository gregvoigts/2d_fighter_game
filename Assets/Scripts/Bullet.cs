using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour
{

    Vector3 _direction;
    public Vector3 direction
    {
        get => _direction;
        set => _direction = value.normalized;
    }

    public Vector3 positon
    {
        set
        {
            value.z = 0;
            transform.position = value;
        }
    }

    public Quaternion rotation
    {
        set { transform.rotation = value; }
    }

    public int  PoolIndex{get;set;}

    [SerializeField] float speed;
    public float power = 30;

    new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActiveAndEnabled)
        {
            transform.position += direction * speed;

            if (!renderer.isVisible)
            {
                BulletHandler.instance.DestroyBullet(this);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletHandler.instance.DestroyBullet(this);
        Player p;
        if(collision.gameObject.TryGetComponent<Player>(out p))
        {
            p.hit(this.power);
        }

    }
}
