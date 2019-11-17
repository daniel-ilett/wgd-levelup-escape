using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private List<BillboardData> parts;

    private new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();

        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].startPos = parts[i].rigidbody.position;
            parts[i].collider = parts[i].rigidbody.GetComponent<Collider>();
        }
    }

    public void ResetParts()
    {
        collider.enabled = true;
        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].rigidbody.isKinematic = true;
            parts[i].rigidbody.position = parts[i].startPos;
            parts[i].collider.enabled = false;
        }
    }

    public void Hit()
    {
        Debug.Log("HIT!");

        collider.enabled = false;

        for(int i = 0; i < parts.Count; ++i)
        {
            parts[i].rigidbody.isKinematic = false;
            parts[i].collider.enabled = true;
        }
    }

    [System.Serializable]
    private class BillboardData
    {
        public Rigidbody rigidbody;

        [HideInInspector]
        public Vector3 startPos;

        [HideInInspector]
        public Collider collider;
    }
}

