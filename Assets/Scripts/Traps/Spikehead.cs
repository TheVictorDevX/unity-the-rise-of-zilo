//using UnityEngine;

//public class Spikehead : EnemyDamage
//{
//    [Header("SpikeHead Attributes")]
//    [SerializeField] private float speed;
//    [SerializeField] private float range;
//    [SerializeField] private float checkDelay;
//    [SerializeField] private LayerMask playerLayer;
//    private Vector3[] directions = new Vector3[4];
//    private Vector3 destination;
//    private float checkTimer;
//    private bool attacking;

//    private void OnEnable()
//    {
//        Stop();
//    }
//    private void Update()
//    {
//        //Move spikehead to destination only if attacking
//        if (attacking)
//        {
//            transform.Translate(destination*Time.deltaTime*speed);
//        }
//        else
//        {
//            checkTimer += Time.deltaTime;
//            if (checkTimer>checkDelay)
//            {
//                CheckForPlayer();
//            }
//        }
//    }

//    private void CheckForPlayer()
//    {
//        CalculateDirections();
//        //Check if spikehead sees player in all 4 directions
//        for (int i = 0; i < directions.Length; i++)
//        {
//            Debug.DrawRay(transform.position, directions[i], Color.red);
//            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

//            if (hit.collider != null && !attacking)
//            {
//                attacking = true;
//                destination = directions[i];
//                checkTimer = 0;
//            }
//        }
//    }
//    private void CalculateDirections()
//    {
//        directions[0] = transform.right * range; //Right direction
//        directions[1] = -transform.right * range; //Left direction
//        directions[2] = transform.up * range; //Up direction
//        directions[3] = -transform.up * range; //Down direction
//    }
//    private void Stop()
//    {
//        destination = transform.position; //Set destination as current position so it doesn't move
//        attacking = false;
//    } 
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        base.OnTriggerEnter2D(collision);
//        Stop();//Stop Spikehead once he hits something
//    }
//}
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private Vector3 initialPosition; // Store the start point
    private float checkTimer;
    private bool attacking;
    private bool returning; // New state

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void Awake()
    {
        initialPosition = transform.position; // Save the starting spot
    }

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else if (returning)
        {
            // Move back to the start
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

            // Check if we arrived back home
            if (transform.position == initialPosition)
                returning = false;
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i].normalized; // Normalized to keep speed consistent
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        attacking = false;
        returning = true; // Start heading back after hitting something
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't "Stop" if we just touched the player while returning
        //if (returning && collision.CompareTag("Player")) return;
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
