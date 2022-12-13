using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Player : NetworkBehaviour
{
    public float speed = 3f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float jumpColdown = 3f;
    [SyncVar] float _jumpColdown = 0;
    Rigidbody2D rb;
    Body body;
    Shoulder shoulder;
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
        shoulder= GetComponentInChildren<Shoulder>();
        coll= GetComponent<BoxCollider2D>();
        _health = _maxHealth;
    }
    public void Move(float mov)
    {
        transform.position += speed * Time.deltaTime * new Vector3(mov, 0);

        if (mov > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (mov < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Jump()
    {
        if (_jumpColdown > 0)
        {
            _jumpColdown -= Time.deltaTime;
        }
        else
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpColdown = jumpColdown;
        }
        
    }

    public void SquatDown()
    {
        speed /= 2;
        jumpForce /= 2;
        transform.position += Vector3.down * squatHigh;
        body.SquatStart(squatHigh);
        coll.size += Vector2.down * squatHigh * 2;
        coll.offset += Vector2.up * squatHigh;
    }


    public void SquatUp()
    {
        speed *= 2;
        jumpForce *= 2;
        transform.position += Vector3.up * squatHigh;
        body.SquatEnd(squatHigh);
        coll.size += Vector2.up * squatHigh * 2;
        coll.offset += Vector2.down * squatHigh;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isLocalPlayer)
        {
            var mov = Input.GetAxis(controlles.Move);
            var armRot = Input.GetAxis(controlles.MoveArm);

            if (Input.GetButtonDown(controlles.Squat))
            {
                SquatDown();
            }
            else if (Input.GetButtonUp(controlles.Squat))
            {
                SquatUp();
            }

            //rb.AddForce(new Vector2(mov,0) * speed,ForceMode2D.Impulse);
            Move(mov);
            shoulder.RotateArm(armRot * Time.deltaTime);

            if (_jumpColdown <= 0 && Input.GetButtonDown(controlles.Jump))
            {
                Jump();
            }
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
