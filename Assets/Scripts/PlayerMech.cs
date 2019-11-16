using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMech : MonoBehaviour
{
    // Input axes.
    [SerializeField]
    private string horizontalBtn;

    [SerializeField]
    private string verticalBtn;

    [SerializeField]
    private string lookRotateBtn;

    [SerializeField]
    private string morphBtn;

    [SerializeField]
    private string gunBtn;

    [SerializeField]
    private string missileBtn;

    [SerializeField]
    private float camSensitivity;

    [SerializeField]
    private float tankSpeed;

    [SerializeField]
    private float morphSpeed;

    [SerializeField]
    private float walkSpeed;

    private float currentSpeed;

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

    private Vector3 moveVector = Vector3.zero;
    private float camRotation = 0.0f;

    // Component caches.
    private Animator anim;
    private AudioSource audioSource;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();

        currentSpeed = walkSpeed;

        for(int i = 0; i < missileLocations.Count; ++i)
        {
            var newMissile = Instantiate(missilePrefab, missileLocations[i].position, missileLocations[i].rotation, missileLocations[i]);
            missiles.Add(newMissile);

            newMissile.SetMech(this);
            newMissile.SetTag("Player");
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
        // Spacebar will transform the player.
        if (Input.GetButtonDown(morphBtn) && !isTransforming)
        {
            TransformMode();
        }

        if(Input.GetButton(gunBtn))
        {
            FireGun();
        }

        // Right-click will fire a missile.
        if (Input.GetButtonDown(missileBtn) && missileTime > missileCooldown)
        {
            FireMissile();
        }

        var xMovement = Input.GetAxis(horizontalBtn) * transform.forward * currentSpeed;
        var zMovement = Input.GetAxis(verticalBtn) * transform.right * -currentSpeed; 
        var yRotate = Input.GetAxis(lookRotateBtn) * currentSpeed;

        moveVector = xMovement + zMovement;
        camRotation = yRotate;

        anim.SetBool("Walking", moveVector.magnitude > 0.01f);

        Debug.Log(moveVector.magnitude > 0.01f);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = (moveVector * 10.0f) + new Vector3(0.0f, rigidbody.velocity.y, 0.0f);
        transform.Rotate(0.0f, camRotation * camSensitivity, 0.0f);
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
        currentSpeed = morphSpeed;

        audioSource.Play();
    }

    private void TransformEnded()
    {
        isTransforming = false;

        currentSpeed = isMech ? tankSpeed : walkSpeed;
    }

    private void FireGun()
    {

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
