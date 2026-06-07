using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class FinishPoint : MonoBehaviour
{
    [Header("UI Kemenangan")]
    public GameObject winPanel;      // Tempat menaruh objek WinPanel (Canvas) di Inspector
    public TextMeshProUGUI winText;  // Tempat menaruh objek Teks (TMP) anak dari WinPanel

    private bool isFinished = false; // Pengaman agar fungsi tidak terpanggil berkali-kali saat disentuh

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Deteksi jika yang menyentuh titik finish adalah Player
        if (collision.CompareTag("Player") && !isFinished)
        {
            isFinished = true;
            string currentSceneName = SceneManager.GetActiveScene().name;

            // CEK: Apakah ini Level 3 (Level Terakhir)?
            if (currentSceneName.ToLower() == "level 3")
            {
                // JIKA TAMAT LEVEL 3: Progress menu level di-reset kembali dari awal
                PlayerPrefs.SetInt("UnlockedLevel", 1);
                PlayerPrefs.Save();

                // Jalankan efek selebrasi tamat game
                StartCoroutine(ProsesTamatGame());
            }
            else
            {
                // JIKA SELESAI LEVEL 1 ATAU LEVEL 2:
                string nextSceneName = "";
                int nextLevelId = 1;

                // Mencocokkan nama scene aktif untuk menentukan tujuan berikutnya
                if (currentSceneName.ToLower() == "level 1")
                {
                    nextSceneName = "level 2";
                    nextLevelId = 2;
                }
                else if (currentSceneName.ToLower() == "level 2")
                {
                    nextSceneName = "level 3";
                    nextLevelId = 3;
                }

                // Ambil data level tertinggi yang pernah dibuka sebelumnya
                int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

                // Jika level berikutnya belum pernah terbuka, update kuncinya di PlayerPrefs
                if (nextLevelId > currentUnlocked)
                {
                    PlayerPrefs.SetInt("UnlockedLevel", nextLevelId);
                    PlayerPrefs.Save();
                }

                // Jalankan transisi teks "Next Level" sebelum pindah level otomatis
                StartCoroutine(ProsesNextLevel(nextSceneName));
            }
        }
    }

    // Coroutine untuk transisi Level 1 dan Level 2 menuju level selanjutnya
    IEnumerator ProsesNextLevel(string nextScene)
    {
        if (winPanel != null) winPanel.SetActive(true);
        if (winText != null) winText.text = "Next Level";

        Time.timeScale = 0f; // Membekukan game sesaat agar tulisan sempat terbaca

        yield return new WaitForSecondsRealtime(2.5f); // Menunggu 2.5 detik waktu nyata

        Time.timeScale = 1f; // Kembalikan waktu normal sebelum ganti scene

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Coroutine untuk transisi tamat di Level 3 kembali ke Main Menu
    IEnumerator ProsesTamatGame()
    {
        if (winPanel != null) winPanel.SetActive(true);
        if (winText != null) winText.text = "Congrats You Win The Game!!";

        Time.timeScale = 0f; // Membekukan game sesaat untuk selebrasi

        yield return new WaitForSecondsRealtime(4f); // Menunggu 4 detik selebrasi tamat

        Time.timeScale = 1f; // Normalkan waktu sebelum balik ke Main Menu
        SceneManager.LoadScene("MainMenu");
    }
}