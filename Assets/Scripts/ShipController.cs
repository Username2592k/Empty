using UnityEngine;

public class ShipController : MonoBehaviour
{
    private ShipController owner;


    // 속도
    public float thrustForce = 20f;
    public float turnTorque = 5f;
    public float maxSpeed = 10f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 50f;
    public float fireCooldown = 0.5f;
    public float maxHealth = 100f;
    public float health = 100f;

    private float lastFireTime;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (CompareTag("Player"))
            HandlePlayerInput();
    }

    void HandlePlayerInput()
    {
        float forward = Input.GetAxis("Vertical");
        float turn = -Input.GetAxis("Horizontal");

        rb.AddForce(transform.forward * forward * thrustForce * Time.deltaTime, ForceMode.Acceleration);
        rb.AddTorque(transform.up * turn * turnTorque * Time.deltaTime, ForceMode.Acceleration);

        Vector3 vel = rb.linearVelocity;
        vel.y = 0f; // 2D plane constraint

        if (vel.magnitude > maxSpeed)
            vel = vel.normalized * maxSpeed;

        rb.linearVelocity = vel;

        if (Input.GetKey(KeyCode.Space) && Time.time > lastFireTime + fireCooldown)
            FirePrimary();
    }

    public void FirePrimary()
    {
        if (projectilePrefab ==  null || firePoint == null) return;

        lastFireTime = Time.time;

        GameObject p = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody prb = p.GetComponent<Rigidbody>();

        if (prb)
            prb.linearVelocity = transform.forward * projectileSpeed;

        Projectile proj = p.GetComponent<Projectile>();
        if (proj)
            proj.SetOwner(this);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
