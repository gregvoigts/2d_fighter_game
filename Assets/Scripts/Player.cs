using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Burst.Intrinsics;
using System;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    [SerializeField] float deathTime = 3.0f;
    Shoulder shoulder;
    //BoxCollider2D coll;
    [SyncVar]public Weapon weapon;
    [SerializeField] float _maxHealth = 100;
    [SerializeField] RangeWeapon gunPrefab;
    [SyncVar(hook =nameof(DeathTimerChanged))] public float deathTimer;

    [SyncVar(hook =nameof(HealthChanged))]float _health;

    /// <summary>
    /// Value between 1 and 2 
    /// </summary>
    [SyncVar]public int team;
    [SerializeField]Shader playerShader;

    [SyncVar(hook = nameof(FlagChanged))] public bool hasFlag;
    GameObject flag;

    Color team1 = new Color(1.0f, 0.2039216f, 0.007843138f, 0.6156863f);
    Color team2 = new Color(0.007843138f, 0.7254902f, 1, 0.3529412f);

    float sinFaktor;
    const float sinAdd = -math.PI / 2;

    SpriteRenderer spriteRenderer;
    HealthBarInner healthBar;
    Animator animator;

    public Controlles controlles = new Controlles("Horizontal", "Jump", "Fire", "Vertical","Squat");
    // Start is called before the first frame update
    void Start()
    {
        sinFaktor = 2 * math.PI / deathTime;
        shoulder = GetComponentInChildren<Shoulder>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        flag = transform.Find("Flag")?.gameObject;
        //coll= GetComponent<BoxCollider2D>();
        healthBar= GetComponentInChildren<HealthBarInner>();
        if (isServer)
        {
            _health = _maxHealth;
            team = NetworkServer.connections.Count % 2 + 1;
            EquipWeapon();
        }
        else if (weapon != null)
        {
            EquipOnClients(weapon);
        }
        var mat = new Material(playerShader);
        mat.SetColor("_TintColor", team == 1 ? team1 : team2);
        mat.SetFloat("_Progress", -1);
        mat.SetColor("LaserColor", new Color(14, 0, 191, 0));
        mat.SetFloat("LaserThickness", 0.03f);
        spriteRenderer.material = mat;

        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void HealthChanged(float oldHealth, float newHealth)
    {
        if (healthBar != null)
        {
            healthBar.FillAmount = newHealth / _maxHealth;
        }
    }

    private void FlagChanged(bool oldValue, bool newValue)
    {
            flag.SetActive(newValue);
        Debug.Log($"Flag changed to:{newValue}");
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        Debug.Log("scene Changed");
        hasFlag= false;
        transform.position = SpawnPoints.instance.getSpawn(team);
    }


        private void DeathTimerChanged(float oldValue, float newValue)
    {
        if (isLocalPlayer)
        {
            DeathScreen.instance.UpdateDeathCounter(newValue);
        }
        if(newValue < deathTime /2 && oldValue > deathTime / 2)
        {
            transform.position = SpawnPoints.instance.getSpawn(team);
        }
        float prog;
        if (newValue == 0 || newValue == deathTime)
        {
            prog = -1;
        }
        else {
            prog = math.sin(newValue * sinFaktor + sinAdd);
        }
        spriteRenderer.material.SetFloat("_Progress", prog);
    }

    private void Respawn()
    {
        
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
        mW.transform.localPosition = new Vector3(-0.12f, -0.038f, 0);
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
        newWeapon.transform.localPosition = new Vector3(-0.12f, -0.038f, 0);
        newWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
        newWeapon.transform.localScale= new Vector3(4.88f, 4.88f, 1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isLocalPlayer && deathTimer <= 0)
        {
            var vert = Input.GetAxis(controlles.MoveArm);
            var hor = Input.GetAxis(controlles.Move);


            
            if (weapon != null && !weapon.HasColdown())
            {                
                if (Input.GetButtonDown(controlles.Fire))
                {
                    weapon.attackAnimation();
                    Attack();
                }
            }

            shoulder.RotateArm(vert, hor);
        }
        if(isServer)
        {
            //Debug.Log(deathTimer);
            if(deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
                if(deathTimer < 0)
                {
                    deathTimer = 0;
                    Respawn();
                }
            }
        }
    }

    [Server]
    public float hit(float power)
    {
        if(deathTimer > 0)
        {
            return 0;
        }
        print("hit " + power);
        _health -= power;
        if (_health < 0)
        {
            _health = 0;
            //Tod
            //killed();
            _health = _maxHealth;
            deathTimer = deathTime;
            //Drop Flag
            Debug.Log(hasFlag);
            if (hasFlag)
            {
                hasFlag = false;
                FlagController.instance.transform.position = transform.position;
                FlagController.instance.SetActive(true);
            }
        }
        return _health;
    }

    /*[ClientRpc]
    public void killed() {
        transform.position = team == 0 ? spawnTeam1 : spawnTeam2;

    }*/
}
