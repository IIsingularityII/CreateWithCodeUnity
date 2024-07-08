using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private Animator playerAnim;

    private AudioSource playerAudio;

    public float jumpForce = 10;
    public float gravityModifier;

    public ParticleSystem ExplosionParticle;
    public ParticleSystem DirtParticle;

    public AudioClip JumpSound;
    public AudioClip CrashSound;
    private bool isOnGround;
    private bool gameOver;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            DirtParticle.Stop();
            playerAudio.PlayOneShot(JumpSound, 1.0f);
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            DirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            ExplosionParticle.Play();
            DirtParticle.Stop();
            playerAudio.PlayOneShot(CrashSound, 1.0f);
        }
        
    }
    public bool isGameOver()
    {
        return gameOver;
    }
}
