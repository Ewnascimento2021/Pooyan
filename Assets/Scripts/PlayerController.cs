using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 facingLeft;
    private float verticalInput;
    private float positionMax = 2.6f;
    private float positionMin = -3.2f;
    private int numberBullets = 1;
    private float elapsedTime;
    private Animator animator;

    [SerializeField]
    private Rigidbody2D projectile;
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float reloadTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        transform.localScale = facingLeft;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        UpDown();
    }
    private void Update()
    {
        CheckFire();
    }

    private void UpDown()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            verticalInput = Input.GetAxisRaw("Vertical");

            rb.MovePosition(rb.position + new Vector2(0, speed * verticalInput * Time.deltaTime));

            if (rb.position.y + speed * verticalInput * Time.deltaTime > positionMax)
            {
                rb.MovePosition(new Vector2(rb.position.x, positionMax));
            }
            if (rb.position.y + speed * verticalInput * Time.deltaTime < positionMin)
            {
                rb.MovePosition(new Vector2(rb.position.x, positionMin));
            }
        }
    }

    private void CheckFire()
    {
        if (Input.GetButtonDown("Fire1") && (numberBullets > 0))
        {
            Instantiate(projectile, spawn.position, spawn.rotation);
            animator.SetBool("Attack", true);
            GameController.Instance.audioTiro.Play();
            numberBullets -= 1;
        }

        else if (numberBullets == 0)
        {
            ReloadBullets();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Attack", false);
        }
    }
    private void ReloadBullets()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= reloadTime)
        {
            elapsedTime = 0f;
            numberBullets = 1;
        }
    }
}


