using System;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer=Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;
    
    //References
    private Animator animator;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol=GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // 1. Attack logic (Only runs when player is in sight)
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        // 2. Patrol logic (Runs EVERY frame)
        if (enemyPatrol != null)
        {
            // This will correctly re-enable the script when PlayerInSight() is false
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,0,playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //If player still in range damage him
        if (PlayerInSight())
        {
            //Damage player health
            playerHealth.TakeDamage(damage);
        }
    }
}
