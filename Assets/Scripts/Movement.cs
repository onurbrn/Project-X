using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;
    [SerializeField] AudioClip audioThrust;
    [SerializeField] ParticleSystem particlesThrust;
    [SerializeField] ParticleSystem particlesRotationLeft;
    [SerializeField] ParticleSystem particlesRotationRight;
    
    Rigidbody _rigidbody;
    AudioSource _audioSource;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
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
            StartRotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            StartRotateRight();
        }
        else
            StopRotating();
    }

    void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!_audioSource.isPlaying)

        {
            _audioSource.PlayOneShot(audioThrust);
        }
        if (!particlesThrust.isPlaying)
        {
            particlesThrust.Play();
        }
    }

     void StopThrusting()
    {
        _audioSource.Stop();
        particlesThrust.Stop();
    }

    void StartRotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!particlesRotationLeft.isPlaying)
        {
            particlesRotationLeft.Play();
        }
    }

    void StartRotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!particlesRotationRight.isPlaying)
        {
            particlesRotationRight.Play();
        }
    }

    void StopRotating()
    {
        particlesRotationLeft.Stop();
        particlesRotationRight.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

}
