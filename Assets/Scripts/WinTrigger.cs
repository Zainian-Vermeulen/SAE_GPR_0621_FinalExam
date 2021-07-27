using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] GameObject _winUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        _winUI.SetActive(true);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
