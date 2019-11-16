using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMech : MonoBehaviour
{
    // Prefabs.
    [SerializeField]
    private Missile missilePrefab;

    // Hierarchy transforms.
    [SerializeField]
    private List<Transform> missileLocations;

    private List<Missile> missiles = new List<Missile>();

    // State variables.
    private bool isMech = false;
    private bool isTransforming = false;
    private int missileID = 0;

    private float missileTime = 0.0f;
    private const float missileCooldown = 2.0f;

    // Component caches.
    private Animator anim;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        for(int i = 0; i < missileLocations.Count; ++i)
        {
            var newMissile = Instantiate(missilePrefab, missileLocations[i].position, missileLocations[i].rotation, missileLocations[i]);
            missiles.Add(newMissile);

            newMissile.SetMech(this);
        }
    }

    private void Update()
    {
        // Process timing events.
        TimingUpdate();

        // Process inputs.
        InputUpdate();
    }

    private void TimingUpdate()
    {
        missileTime += Time.deltaTime;
    }

    private void InputUpdate()
    {
        // Left-click will transform the player.
        if (Input.GetButtonDown("Fire1") && !isTransforming)
        {
            TransformMode();
        }

        // Right-click will fire a missile.
        if (Input.GetButtonDown("Fire2") && missileTime > missileCooldown)
        {
            FireMissile();
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

    private void FireMissile()
    {
        missiles[missileID].Fire();
        missileID = (++missileID) % missiles.Count;

        missileTime = 0.0f;
    }

    // Give the missile the default parent Transform and position.
    public void ReturnMissile(Missile missile)
    {
        var index = missiles.IndexOf(missile);
        missile.transform.parent = missileLocations[index];

        missile.transform.localPosition = Vector3.zero;
        missile.transform.localRotation = Quaternion.identity;
    }
}
