﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] bool Knockback;
    [HideInInspector]public  bool PowerUp;
    [SerializeField] ParticleSystem Effect;
    [SerializeField] AttackSoundsManager Sounds;
    [SerializeField] NPC thisNpcScript;
    string tagToAttack, secondTagToAttack;
    bool missed;
    ParticleSystem PowerUpEffect, PowerUpHitEffect;
    private void Start()
    {
        if (thisNpcScript.transform.tag == "Enemy")
        {
            tagToAttack = "People";
            secondTagToAttack = "Player";
        }
        else
        {
            PowerUpEffect = transform.parent.Find("PowerUpEffect").GetComponent<ParticleSystem>();
            PowerUpHitEffect = transform.parent.Find("PowerUpHit").GetComponent<ParticleSystem>();
            tagToAttack = "Enemy";
            secondTagToAttack = "Enemy";
        }

    }
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        
        if ( other.tag == tagToAttack || other.tag == secondTagToAttack)
        {
            if (Sounds != null)
            {
                Sounds.AttackHit();
            }
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            var attackDamage = thisNpcScript.Damage;
            if (PowerUp)
            {
                attackDamage = attackDamage * 2;
                Knockback = true;
                PowerUpHit();
                PowerUp = false;
            }

            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(attackDamage, true, 5,transform.parent.transform);

                Knockback = false;
            }
            else
            {
                EnemyNPC.TakeDamage(attackDamage);
            }
            //Set his Enemy to this
            EnemyNPC.AI_SetEnemy(thisNpcScript.gameObject);
            //If he's dead, then forget about him
            missed = false;
           
        }
    }
    private void OnEnable()
    {
        missed = true; 
        GameObject enem = thisNpcScript.AI_GetEnemy();
        if (Effect != null)
        {

            Debug.Log("EnemyParticle");
            Effect.Play();
        }
        if (enem != null)
        {
            if (enem.tag == "Building")
            {
                enem.GetComponent<NPC>().Life -= transform.root.GetComponent<NPC>().Damage;
            }
        }
        
        StartCoroutine(WaitandDisable());
    }
    
    IEnumerator WaitandDisable()
    {
        
        yield return new WaitForSeconds(0.1f);
        if (Sounds != null)
        {
            if (missed)
            {
                Sounds.AttackMiss();
            }
            
        }
        transform.gameObject.SetActive(false);
    }
    public void ActivateBoostAttack()
    {
        PowerUpEffect.Play();
        PowerUp = true;
    }
    public void DeactivateBoostAttack()
    {

        PowerUpEffect.Stop();
    }
    public void PowerUpHit()
    {
        DeactivateBoostAttack();
        PowerUpHitEffect.Play();
    }
}
