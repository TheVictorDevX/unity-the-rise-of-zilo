//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    [SerializeField] private float speed;
//    [SerializeField] private float jumpPower;
//    private Rigidbody2D body;
//    private Animator animator;
//    private BoxCollider2D boxCollider;
//    [SerializeField] private LayerMask groundLayer;
//    private float horizontalInput;

//    [Header("SFX")]
//    [SerializeField] private AudioClip jumpSound;

//    private void Awake()
//    {
//        body = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        boxCollider = GetComponent<BoxCollider2D>();
//    }

//    private void Update()
//    {
//        horizontalInput = Input.GetAxis("Horizontal");

//        // Flip Player When Moving Left or Right
//        if (horizontalInput > 0.01f)
//            transform.localScale = Vector3.one;
//        else if (horizontalInput < -0.01f)
//            transform.localScale = new Vector3(-1, 1, 1);

//        // Apply horizontal movement
//        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

//        // Set Animator Parameters
//        animator.SetBool("Running", horizontalInput != 0);
//        animator.SetBool("Grounded", isGrounded());

//        // Jump
//        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
//        {
//            SoundManager.instance.PlaySound(jumpSound);
//            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
//            animator.SetTrigger("Jump");
//        }
//    }

//    private bool isGrounded()
//    {
//        RaycastHit2D hit = Physics2D.BoxCast(
//            boxCollider.bounds.center,
//            boxCollider.bounds.size,
//            0, Vector2.down,
//            0.1f,
//            groundLayer
//        );
//        return hit.collider != null;
//    }

//    public bool canAttack()
//    {
//        return isGrounded();
//    }
//}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

    // Changed: This is now controlled by UI buttons/joystick
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //private void Update()
    //{
    //    // On Mobile, we stop calling Input.GetAxis here 
    //    // because the UI script will set horizontalInput for us.

    //    // Flip Player
    //    if (horizontalInput > 0.01f)
    //        transform.localScale = Vector3.one;
    //    else if (horizontalInput < -0.01f)
    //        transform.localScale = new Vector3(-1, 1, 1);

    //    body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

    //    animator.SetBool("Running", horizontalInput != 0);
    //    animator.SetBool("Grounded", isGrounded());
    //}
    private void Update()
    {
        // FIX: If the game is paused, do nothing and exit the update loop immediately
        if (UIManager.pause) return;

        // 1. Check Keyboard Input first
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        // 2. Use keyboard if it's being pressed, otherwise use mobileInput
        // This allows both to work simultaneously
        float finalInput = (keyboardInput != 0) ? keyboardInput : horizontalInput;

        // Flip Player
        if (finalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (finalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Move
        body.linearVelocity = new Vector2(finalInput * speed, body.linearVelocity.y);

        // Jump (Keyboard Support)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Animator
        animator.SetBool("Running", finalInput != 0);
        animator.SetBool("Grounded", isGrounded());
    }

    // --- NEW METHODS FOR MOBILE UI ---

    public void Move(float input)
    {
        horizontalInput = input;
    }

    public void Jump()
    {
        if (isGrounded())
        {
            SoundManager.instance.PlaySound(jumpSound);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            animator.SetTrigger("Jump");
        }
    }

    // ---------------------------------

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

    public bool canAttack() => isGrounded();
}