using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private List<Debris> parts;

    private new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void ResetParts()
    {
        collider.enabled = true;

        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].ResetPart();
        }
    }

    public void Hit(ExplosionData explosion)
    {
        collider.enabled = false;

        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].Hit(explosion);
        }
    }
}

