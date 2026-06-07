using TMPro;
using UnityEngine;

public class SkorManager : MonoBehaviour
{
    public static SkorManager instance;
    public TextMeshProUGUI skorText;
    private int skor = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateTampilanSkor();
    }

    public void TambahSkor(int nilai)
    {
        skor += nilai;
        UpdateTampilanSkor();
    }

    void UpdateTampilanSkor()
    {
        if (skorText != null)
        {
            skorText.text = "Skor: " + skor;
        }
    }
}