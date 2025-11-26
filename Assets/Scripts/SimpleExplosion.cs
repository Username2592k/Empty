using UnityEngine;


public class SimpleExplosion : MonoBehaviour
{
public float radius = 6f;
public float force = 200f;
public float damage = 50f;


void Start()
{
Collider[] cols = Physics.OverlapSphere(transform.position, radius);
foreach (var c in cols)
{
Rigidbody rb = c.GetComponent<Rigidbody>();
if (rb) rb.AddExplosionForce(force, transform.position, radius);
ShipController s = c.GetComponentInParent<ShipController>();
if (s) s.TakeDamage(damage);
}
Destroy(gameObject);
}
}