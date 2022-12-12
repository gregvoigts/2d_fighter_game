using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] float coldown = 1.0f;
    float _coldown = 0;
    public string attackButton;
    [SerializeField] float damage = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        attackButton = GetComponentInParent<Player>().controlles.Fire;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_coldown > 0)
        {
            _coldown -= Time.deltaTime;
        }
        else if (Input.GetButtonDown(attackButton))
        {
            _attack();
            _coldown = coldown;
        }
    }

    void _attack()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }
}
