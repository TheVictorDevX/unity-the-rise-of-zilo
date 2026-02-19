using UnityEngine;

public class EnemyProjectile : EnemyDamage //Will damage the player every time they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private bool hit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider2D.enabled = true;
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed*Time.deltaTime;
        transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        boxCollider2D.enabled = false;

        if (animator != null)
        {
            animator.SetTrigger("Explode"); //When the object is a fireball explode it
        }
        else
        {
            gameObject.SetActive(false); //When this hits any object deactivate
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
