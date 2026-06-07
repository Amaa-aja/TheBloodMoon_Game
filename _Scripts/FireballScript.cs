using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float destroyTime = 4f;

    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private bool directionSet = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
            // AKALAN 1: Pastikan deteksi tabrakan diset kontinu agar tidak menembus objek saat cepat
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    void Start()
    {
        // AKALAN 2: Pindahkan fungsi Destroy ke Start agar tidak bentrok saat Instantiate
        Destroy(gameObject, destroyTime);
    }

    // Fungsi menerima arah tembakan dari EnemyShooter secara presisi
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
        directionSet = true;

        // AKALAN 3: Langsung suntik kecepatan di sini begitu arah diterima, jangan nunggu FixedUpdate
        if (rb != null)
        {
            rb.linearVelocity = moveDirection * speed;
        }
    }

    void FixedUpdate()
    {
        // Jika karena suatu hal SetDirection terlambat, dorong ulang di FixedUpdate
        if (rb != null && directionSet)
        {
            rb.linearVelocity = moveDirection * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthAndBuff playerHealth = collision.GetComponent<PlayerHealthAndBuff>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        // Menyederhanakan deteksi tanah agar lebih akurat
        else if (collision.CompareTag("Ground") || collision.gameObject.name.ToLower().Contains("grass") || collision.gameObject.name.ToLower().Contains("tile"))
        {
            Destroy(gameObject);
        }
    }
}