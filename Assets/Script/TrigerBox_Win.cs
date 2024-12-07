using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public TMP_Text winText;
    public string winMessage = "You Win!";

    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            winText.text = winMessage;
            winText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}