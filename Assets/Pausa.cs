using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public GameObject pausa;
    // Start is called before the first frame update
    void Start()
    {
        pausa.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Cursor.visible = false;

    }
    public void RecargarEscena()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Cursor.visible = false;

    }
    public void Puse()
    {
        pausa.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void CambiarDeEscena()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
