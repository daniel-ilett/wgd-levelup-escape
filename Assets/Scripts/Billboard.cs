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

    private void ResetParts()
    {
        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].ResetPart();
        }

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.0f);
        collider.enabled = true;
    }

    public void Hit(ExplosionData explosion)
    {
        collider.enabled = false;

        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].Hit(explosion);
        }

        Invoke("ResetParts", 10.0f);
    }
}

