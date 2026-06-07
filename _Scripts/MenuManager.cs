using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panel UI")]
    public GameObject panelPause; // Tempat naruh Panel Pause

    private bool isPaused = false;
    private bool isMuted = false;

    void Start()
    {
        // Pastikan panel pause mati saat awal game
        if (panelPause != null) panelPause.SetActive(false);

        // Load status mute terakhir (0 = tidak mute, 1 = mute)
        isMuted = PlayerPrefs.GetInt("GameMuted", 0) == 1;
        AudioListener.pause = isMuted;
    }

    void Update()
    {
        // Pencet ESC di keyboard bisa buat Pause/Resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // --- LOGIKA PAUSE & RESUME ---
    public void PauseGame()
    {
        isPaused = true;
        if (panelPause != null) panelPause.SetActive(true);
        Time.timeScale = 0f; // Game berhenti
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (panelPause != null) panelPause.SetActive(false);
        Time.timeScale = 1f; // Game jalan lagi
    }

    // --- LOGIKA MUTE AUDIO ---
    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted; // Matikan/nyalakan semua suara di Unity

        // Simpan settingan biar gak reset pas ganti level
        PlayerPrefs.SetInt("GameMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log(isMuted ? "Game Di-MUTE!" : "Game Di-UNMUTE!");
    }

    // --- LOGIKA EXIT GAME ---
    public void ExitGame()
    {
        Debug.Log("Keluar ke Main Menu...");
        Time.timeScale = 1f; // Reset waktu ke normal sebelum pindah scene

        // Pindah ke scene MainMenu (Pastikan nama scene menumu "MainMenu")
        SceneManager.LoadScene("MainMenu");
    }
}