using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cargarescenasss : MonoBehaviour
{
    public GameObject pausaCanvas;
    public GameObject creditosCanvas;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                PausarJuego();
            }
            else
            {
                ReanudarJuego();
            }
        }
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void PausarJuego()
    {
        Time.timeScale = 0;
        pausaCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        pausaCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RecargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void CambiarDeEscenaLevel1()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void CambiarMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void abrirCreditos()
    {
        creditosCanvas.gameObject.SetActive(true);      
    }

    public void cerrarCreditos()
    {
        creditosCanvas.gameObject.SetActive(false);
    }

    public void Salir()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else       
        Application.Quit();
        #endif
    }


}