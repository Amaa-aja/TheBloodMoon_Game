using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private Transform player;
    private float nextFireTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (Time.time >= nextFireTime)
        {
            ShootFireball();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootFireball()
    {
        if (fireballPrefab != null)
        {
            Transform spawnPoint = firePoint != null ? firePoint : transform;

            // Spawn peluru tanpa menggunakan parent sejak awal
            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);

            // Hitung arah ke player
            Vector2 direction = ((Vector2)player.position - (Vector2)spawnPoint.position).normalized;

            // JIKA arahnya entah bagaimana 0 (posisi persis sama), akali tembak ke arah hadap musuh
            if (direction == Vector2.zero)
            {
                direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            }

            FireballScript fireballLogic = fireball.GetComponent<FireballScript>();
            if (fireballLogic != null)
            {
                // Panggil fungsi ini untuk langsung melepaskan peluru
                fireballLogic.SetDirection(direction);
            }
        }
    }
}