using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMech : MonoBehaviour
{
    // State variables.
    private bool isMech = false;

    // Component caches.
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(isMech)
            {
                anim.SetTrigger("Robotify");
            }
            else
            {
                anim.SetTrigger("Tankify");
            }

            isMech = !isMech;
        }
    }
}
