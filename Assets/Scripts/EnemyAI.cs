using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
public float detectionRange = 100f;
public float engageRange = 40f;
public float fireInterval = 1.0f;


ShipController ship;
Transform target;
float nextFire;


void Awake()
{
ship = GetComponent<ShipController>();
}


void Start()
{
InvokeRepeating(nameof(UpdateTarget), 0.2f, 0.5f);
}


void UpdateTarget()
{
// Simple: find player by tag
GameObject p = GameObject.FindGameObjectWithTag("Player");
if (p) target = p.transform;
}


void FixedUpdate()
{
if (target == null) return;
Vector3 dir = (target.position - transform.position);
float dist = dir.magnitude;


if (dist > detectionRange) return;


// Turn towards target (simple steering)
Vector3 desired = Vector3.ProjectOnPlane(dir, transform.up).normalized;
Vector3 forward = Vector3.ProjectOnPlane(transform.forward, transform.up).normalized;
float angle = Vector3.SignedAngle(forward, desired, transform.up);
float turn = Mathf.Clamp(angle / 45f, -1f, 1f);
ship.GetComponent<Rigidbody>().AddTorque(transform.up * turn * ship.turnTorque * Time.deltaTime, ForceMode.Acceleration);


// Move forward if not too close
if (dist > engageRange)
{
ship.GetComponent<Rigidbody>().AddForce(transform.forward * ship.thrustForce * Time.deltaTime, ForceMode.Acceleration);
}
else
{
// In range — try to fire
if (Time.time > nextFire)
{
ship.FirePrimary();
nextFire = Time.time + fireInterval;
}
}


// Cap speed like ship controller
Rigidbody rb = ship.GetComponent<Rigidbody>();
Vector3 lateral = Vector3.ProjectOnPlane(rb.linearVelocity, transform.up);
if (lateral.magnitude > ship.maxSpeed)
{
rb.linearVelocity = lateral.normalized * ship.maxSpeed + Vector3.Project(rb.linearVelocity, transform.up);
}
}
}