using UnityEngine;

public class Koin : MonoBehaviour
{
    public AudioClip suaraKoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah yang menabrak memiliki tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Putar suara jika file audio dimasukkan di Inspector
            if (suaraKoin != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(suaraKoin, Camera.main.transform.position, 1f);
            }

            // Tambah skor via SkorManager
            if (SkorManager.instance != null)
            {
                SkorManager.instance.TambahSkor(1);
            }

            // Hancurkan koin dari map
            Destroy(gameObject);
        }
    }
}