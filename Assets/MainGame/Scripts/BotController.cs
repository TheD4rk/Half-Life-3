using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class BotController : MonoBehaviour
{
    public Camera cam;
    private NavMeshAgent agent;

    public ThirdPersonCharacter character;

    private int hp;
    public Transform healthbar;

    private float spotPlayerRange;
    private bool playerSpotted;
    private float shootPlayerRange;
    private bool playerInShootRange;
    private float shootCooldown;
    private bool attacking;

    private PlayerController player;

    public LayerMask playerLayermask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        hp = 3;
        spotPlayerRange = 20f;
        shootPlayerRange = 10f;
        shootCooldown = 3f;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpotted = Physics.CheckSphere(transform.position, spotPlayerRange, playerLayermask);
        playerInShootRange = Physics.CheckSphere(transform.position, shootPlayerRange, playerLayermask);

        if (playerSpotted && !playerInShootRange)
        {
            ChasePlayer();
        }

        if (playerSpotted && playerInShootRange)
        {
            ShootPlayer();
        }
        

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false , false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void ShootPlayer()
    {
        agent.SetDestination(transform.position);
        if (!attacking)
        {
            attacking = true;
            player.GetHit();
            Invoke("ResetAttack", shootCooldown);
            
        }
    }

    private void ResetAttack()
    {
        attacking = false;
    }

    public void GetDamage()
    {
        if (hp > 1)
        {
            hp--;
            RefreshHealthbar();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RefreshHealthbar()
    {
        healthbar.localScale -= new Vector3(0.3f,0f,0f);
    }
}
