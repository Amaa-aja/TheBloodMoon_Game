using UnityEngine;
using System.Collections;

public class DuriBergerak : MonoBehaviour
{
    [Header("Pengaturan Jarak")]
    public float jarakMuncul = 1.5f;       // Seberapa tinggi duri keluar ke atas

    [Header("Pengaturan Waktu (Detik)")]
    public float waktuSembunyi = 2f;      // Berapa lama duri ngumpet di bawah
    public float waktuNongol = 2f;         // Berapa lama duri diam di atas sebelum turun lagi
    public float kecepatanGerak = 5f;      // Kecepatan pas duri naik/turun

    [Header("Pengaturan Damage")]
    public int damage = 1;

    private Vector2 posisiSembunyi;
    private Vector2 posisiMuncul;

    void Start()
    {
        // Posisi saat kamu taruh di map adalah posisi sembunyinya
        posisiSembunyi = transform.position;
        // Posisi muncul adalah posisi awal ditambah tinggi jarak munculnya
        posisiMuncul = new Vector2(posisiSembunyi.x, posisiSembunyi.y + jarakMuncul);

        // Jalankan siklus muncul-hilang selamanya
        StartCoroutine(SiklusPerangkapDuri());
    }

    IEnumerator SiklusPerangkapDuri()
    {
        while (true) // Looping selamanya
        {
            // 1. Durinya sembunyi di bawah, tunggu selama waktuSembunyi
            yield return new WaitForSeconds(waktuSembunyi);

            // 2. Jret! Gerak naik ke posisi muncul
            while (Vector2.Distance(transform.position, posisiMuncul) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, posisiMuncul, kecepatanGerak * Time.deltaTime);
                yield return null;
            }
            transform.position = posisiMuncul; // Pasin posisinya

            // 3. Durinya nongol di atas, diemin dulu biar bisa ngelukain player
            yield return new WaitForSeconds(waktuNongol);

            // 4. Gerak turun lagi ke bawah tanah (sembunyi)
            while (Vector2.Distance(transform.position, posisiSembunyi) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, posisiSembunyi, kecepatanGerak * Time.deltaTime);
                yield return null;
            }
            transform.position = posisiSembunyi; // Pasin posisinya
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
        }
    }
}