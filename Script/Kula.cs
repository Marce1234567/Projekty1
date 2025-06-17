using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kula : MonoBehaviour
{
    [Header("Ustawienia Sterowania")]
    [SerializeField]
    private float Speed = 1f;

    [SerializeField]
    private float JumpForce = 5f;

    public float MaxAngularVelocity;

    private bool isRb;
    private Rigidbody Rb;

    private GameManager gameManager;

    [Header("DŸwiêki")]
    [SerializeField]
    private AudioClip collisionSound;
    private AudioSource audioSource;

    private bool isGrounded = true;

    void Start()
    {
        if (isRb = TryGetComponent<Rigidbody>(out Rb))
        {
            Rb.maxAngularVelocity = MaxAngularVelocity;
        }

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
        if (transform.position.y <= -10f)
        {
            gameManager.RestartPoziomu();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        float Hdirection;
        float Vdirection;

        if (isRb && (Hdirection = Input.GetAxis("Horizontal")) != 0)
        {
            Rb.AddTorque(0, 0, -Hdirection * Time.deltaTime * Speed);
        }
        if (isRb && (Vdirection = Input.GetAxis("Vertical")) != 0)
        {
            Rb.AddTorque(Vdirection * Time.deltaTime * Speed, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pod³oga") || collision.gameObject.CompareTag("Przeszkoda"))
        {
            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
            isGrounded = true;
        }
    }
}
