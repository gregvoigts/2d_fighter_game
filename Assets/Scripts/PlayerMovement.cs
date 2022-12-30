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

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
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

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
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
    }

    public void onCrouching(bool isCrounching)
    {
        animator.SetBool("isCrouching", isCrounching);
    }
}
