using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) // Keycode is better/safer than string names
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }

        else
        {
            StopRotating();
        }
    }

    void StartThrusting()

    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    void StopThrusting()

    {
        audioSource.Stop();
        thrustParticles.Stop();
    }


    void RotateLeft()

    {
        ApplyRotation(rotationThrust);

        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void RotateRight()

    {
        ApplyRotation(-rotationThrust);

        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopRotating()

    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rigidbody/physics system rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
