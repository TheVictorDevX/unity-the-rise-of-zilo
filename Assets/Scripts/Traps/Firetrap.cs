using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered; //when the trap gets trigger
    private bool active; //when the trap active and can hurt the player

    private Health playerHealth;

    private void Awake()
    {
        animator=GetComponent<Animator>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth=collision.GetComponent<Health>();
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player
        triggered = true;
        spriteRenderer.color = Color.red; 

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRenderer.color = Color.white; //turn the sprite back to its initial color
        active = true;
        animator.SetBool("Activated",true);

        //Wait until X seconds, deactivate the trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("Activated", false);
    }
}
