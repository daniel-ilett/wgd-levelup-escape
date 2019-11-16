using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMech : MonoBehaviour
{
    // Prefabs.
    [SerializeField]
    private GameObject missilePrefab;

    // Hierarchy transforms.
    [SerializeField]
    private Transform[] missileLocations;

    private GameObject[] missiles;

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

        missiles = new GameObject[missileLocations.Length];
        for(int i = 0; i < missileLocations.Length; ++i)
        {
            var newMissile = Instantiate(missilePrefab, missileLocations[i].position, transform.rotation, missileLocations[i]);
            missiles[i] = newMissile;
        }
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
