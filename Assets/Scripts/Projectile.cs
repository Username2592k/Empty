using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ShipController owner;  // 누가 쐈는지 기록
    public float damage = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetOwner(ShipController ship)
    {
        owner = ship;
    }

    private void OnCollisionEnter(Collision other)
    {
        ShipController target = other.collider.GetComponent<ShipController>();

        if (target && target != owner)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
