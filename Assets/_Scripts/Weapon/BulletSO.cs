using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Macho/Bullet", order = 0)]
public class BulletSO : ScriptableObject {
    [SerializeField] GameObject bullet;
    [SerializeField] float speed = 1f;
    [SerializeField] float lifeTime = 2f;

    public void StartDestroyTimer(GameObject obj) {
        Destroy(obj, lifeTime);
    }

    public void SpawnBullet(Vector3 position, float dir) {
        GameObject obj = Instantiate(bullet, position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dir * speed, 0);
        rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        StartDestroyTimer(obj);
    }
}