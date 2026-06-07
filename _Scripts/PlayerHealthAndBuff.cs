using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PlayerHealthAndBuff : MonoBehaviour
{
    [Header("Sistem Darah")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI Tampilan")]
    public TextMeshProUGUI darahText;
    public GameObject gameOverPanel;

    [Header("Sistem Buff Lompat")]
    public float tambahanLompat = 4f;
    private PlayerController playerController;

    // Menyimpan posisi awal player untuk reset posisi saat kena duri
    private Vector2 posisiAwal;

    void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();

        // Simpan titik spawn awal player saat level dimulai
        posisiAwal = transform.position;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateDarahUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateDarahUI(); // Langsung update teks di layar biar teksnya berkurang!

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // PERBAIKAN: Jangan restart scene! Cukup kembalikan posisi player ke awal map
            Debug.Log("Darah berkurang! Mengembalikan player ke posisi awal...");
            transform.position = posisiAwal;

            // Opsional: Reset kecepatan gerak fisik biar tidak mental/terseret gaya sebelumnya
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }

    void UpdateDarahUI()
    {
        if (darahText != null)
        {
            darahText.text = "Darah: " + currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Kehabisan Darah! Game Over.");
        StartCoroutine(ProsesGameOver());
    }

    IEnumerator ProsesGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Panel Game Over muncul megah!
        }

        Time.timeScale = 0f; // Bekukan game

        yield return new WaitForSecondsRealtime(3f); // Tampilkan selama 3 detik

        Time.timeScale = 1f; // Normalkan waktu kembali
        SceneManager.LoadScene("MainMenu"); // Pulang ke Main Menu
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff"))
        {
            if (playerController != null)
            {
                playerController.jumpForce += tambahanLompat;
            }
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Spike"))
        {
            TakeDamage(1);
        }
    }
}