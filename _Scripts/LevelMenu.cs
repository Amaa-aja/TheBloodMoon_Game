using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        // Mengambil data level mana yang terbuka, default adalah level 1 (angka 1)
        int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Mengunci seluruh tombol level terlebih dahulu
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Membuka tombol level sesuai dengan jumlah level yang sudah tidak terkunci
        for (int i = 0; i < unlockedlevel; i++)
        {
            if (i < buttons.Length)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void OpenLevel(int levelid)
    {
        string levelName = "level " + levelid; // Sesuaikan dengan penulisan scene kamu ("level 1", dsb)
        SceneManager.LoadScene(levelName);
    }

    public void KembaliKeMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}