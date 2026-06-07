using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Komponen Audio Source yang memutar lagu
    public AudioSource bgmSource;

    // Fungsi untuk mengubah status Mute (Nyala/Mati bergantian)
    public void ToggleMute()
    {
        if (bgmSource != null)
        {
            // Jika tadinya mute, jadinya bersuara. Jika tadinya bersuara, jadinya mute.
            bgmSource.mute = !bgmSource.mute;

            Debug.Log("Status Mute Musik saat ini: " + bgmSource.mute);
        }
        else
        {
            Debug.LogWarning("Audio Source belum dimasukkan ke kolom Inspector!");
        }
    }
}