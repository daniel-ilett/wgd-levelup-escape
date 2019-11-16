using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMech : MonoBehaviour
{
    // State variables.
    private bool isMech = false;
    private bool isTransforming = false;

    // Component caches.
    private Animator anim;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // If pressed, transform the player.
        if (Input.GetButtonDown("Fire1") && !isTransforming)
        {
            TransformMode();
        }
    }

    private void TransformMode()
    {
        if (isMech)
        {
            anim.SetTrigger("Robotify");
        }
        else
        {
            anim.SetTrigger("Tankify");
        }

        isMech = !isMech;
        isTransforming = true;

        audioSource.Play();
    }

    private void TransformEnded()
    {
        isTransforming = false;
    }
}
