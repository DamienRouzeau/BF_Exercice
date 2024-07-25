using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("EMPLACEMENT")]
    public Transform[] placements; // id 0 = Left | id 1 = Middle | id 2 = Right
    int currentPosition;
    public bool shouldGoLeft;
    public bool shouldGoRight;

    [Header("PHYSICS")]
    public Rigidbody rgbd;
    public Animator animator;

    [Header("JUMP")]
    public bool isOnground = true;
    public float jumpStrength;
    public AudioSource jumpSound;

    [Header("MISCELANEOUS")]
    public GameObject deathEffect;
    public GameObject visual;
    public MyGameManager gameManager;

    void Start()
    {
        //Set the player to the middle position
        transform.position = placements[1].position;
        currentPosition = 1;
    }


    void Update()
    {
        if(shouldGoLeft)
        {
            transform.position = new Vector3(placements[currentPosition - 1].position.x, transform.position.y, placements[currentPosition - 1].position.z);
            shouldGoLeft = false;
            currentPosition -= 1;
        }
        else if(shouldGoRight)
        {
            transform.position = new Vector3(placements[currentPosition + 1].position.x, transform.position.y, placements[currentPosition + 1].position.z);
            shouldGoRight = false;
            currentPosition += 1;
        }
    }

    public void OnGoLeft()
    {
        print("left");
        if(currentPosition > 0 && !shouldGoRight)
        {
            shouldGoLeft = true;
        }
    }

    public void OnGoRight()
    {
        if (currentPosition < 2 && !shouldGoLeft)
        {
            shouldGoRight = true;
        }
    }

    public void OnJump()
    {
        if(isOnground)
        {
            jumpSound.Play();
            rgbd.AddForce(0, jumpStrength, 0, ForceMode.Impulse); // Only is player is on ground
            animator.SetTrigger("Jump");
        }
    }

    public void Die()
    {
        GameObject _deathEffect = Instantiate(deathEffect, transform);
        _deathEffect.transform.parent = null;
        gameManager.EndGame();
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Road"))
        {
            isOnground = true;
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Road"))
        {
            isOnground = false;
        }
    }
}
