using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Player : NetworkBehaviour
{
    public float speed = 3f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float jumpColdown = 3f;
    float _jumpColdown = 0;
    Rigidbody2D rb;
    Body body;
    BoxCollider2D coll;
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float squatHigh = 0.35f;

    float _health;

    public HealthBar healthBar { get; set; }
    public float Health
    {
        get => _health;
    }

    public Controlles controlles = new Controlles("Horizontal", "Jump", "Fire", "Vertical","Squat");
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        body= GetComponentInChildren<Body>();
        coll= GetComponent<BoxCollider2D>();
        _health = _maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        var mov = Input.GetAxis(controlles.Move);

        if (Input.GetButtonDown(controlles.Squat))
        {
            speed /= 2;
            jumpForce /=2;
            transform.position += Vector3.down * squatHigh;
            body.SquatStart(squatHigh);
            coll.size += Vector2.down * squatHigh * 2;
            coll.offset += Vector2.up * squatHigh; 
        }
        else if (Input.GetButtonUp(controlles.Squat)){
            speed *= 2;
            jumpForce *= 2;
            transform.position += Vector3.up * squatHigh;
            body.SquatEnd(squatHigh);
            coll.size += Vector2.up * squatHigh * 2;
            coll.offset += Vector2.down * squatHigh;
        }

        //rb.AddForce(new Vector2(mov,0) * speed,ForceMode2D.Impulse);
        transform.position += speed * Time.deltaTime * new Vector3(mov, 0);
        if (mov > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(mov < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (_jumpColdown > 0)
        {
            _jumpColdown -= Time.deltaTime;
        }
        else if (Input.GetButtonDown(controlles.Jump))
        {
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            _jumpColdown = jumpColdown;
        }
    }
    public float hit(float power)
    {
        print("hit " + power);
        _health -= power;
        if (_health < 0)
        {
            _health = 0;
        }
        Debug.Log(_health);
        healthBar.onHealthChanged(Mathf.Clamp(_health / _maxHealth, 0, 1.0f));
        return _health;
    }
}
