using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            if(player.deathTimer> 0)
            {
                return;
            }
            handleInput();
        }
    }
    private void FixedUpdate()
    {    
        controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void handleInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        player.weapon.animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
            player.weapon.animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Squat"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Squat"))
        {
            crouch = false;
        }
    
        if(Input.GetKey(KeyCode.M)){
            SceneManager.LoadScene("CharacterTest_2");
        }
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
        player.weapon.animator.SetBool("isJumping", false);
    }

    public void onCrouching(bool isCrounching)
    {
        animator.SetBool("isCrouching", isCrounching);
        player.weapon.animator.SetBool("isJumping", isCrounching);
    }
}
