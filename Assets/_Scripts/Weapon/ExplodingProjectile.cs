using UnityEngine;
using System.Collections.Generic;

public class ExplodingProjectile : Projectile {
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] bool miniExplosives = false;
    [SerializeField] float explosionImpact = 5f;
    [SerializeField] LayerMask explosionLayerMask;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Vector2 explosionPosition = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius, explosionLayerMask);
        HashSet<Transform> impactedObjects = new HashSet<Transform>();

        foreach (var collider in colliders) {
            if (impactedObjects.Contains(collider.transform)) continue;
            else impactedObjects.Add(collider.transform);

            if (explosionImpact > 0) {
                if (collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) {
                    Vector2 direction = collider.transform.position - transform.position;
                    float distance = direction.magnitude;
                    if (distance > 0f) {
                        float forceMagnitude = explosionRadius / distance;
                        rb.AddForce(direction.normalized * forceMagnitude * explosionImpact, ForceMode2D.Impulse);
                    }
                }
            }
            if (collider.TryGetComponent<HealthManager>(out HealthManager hM)) {
                hM.TakeHealth(damage);
                Debug.Log(hM.Health);    
            }
        }
    }
}