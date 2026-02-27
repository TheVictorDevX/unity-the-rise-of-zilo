using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Only handle the input here
        if (Input.GetKeyDown(KeyCode.F))
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        // The gatekeeper is now inside the function itself
        if (cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            SoundManager.instance.PlaySound(fireballSound);
            anim.SetTrigger("Attack");
            cooldownTimer = 0;

            int fireballIndex = FindFireball(); // Store index to avoid calling the loop twice
            fireballs[fireballIndex].transform.position = firePoint.position;
            fireballs[fireballIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}