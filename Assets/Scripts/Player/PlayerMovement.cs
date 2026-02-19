using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip Player When Moving Left or Right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Apply horizontal movement
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Set Animator Parameters
        animator.SetBool("Running", horizontalInput != 0);
        animator.SetBool("Grounded", isGrounded());

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            SoundManager.instance.PlaySound(jumpSound);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            animator.SetTrigger("Jump");
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0, Vector2.down,
            0.1f,
            groundLayer
        );
        return hit.collider != null;
    }

    public bool canAttack()
    {
        return isGrounded();
    }
}
