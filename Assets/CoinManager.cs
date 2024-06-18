using UnityEngine;
using UnityEngine.UI;
using TMPro;  
using UnityEngine.SceneManagement; 


public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText; 
    public AudioClip coinSound;
    public AudioSource audioSource;

    private int totalCoins;
    private int collectedCoins = 0;
    public bool win = false;
    public GameObject objectToCheck; 
    public GameObject objectToActivate;
    private bool objectDestroyed = false;


    void Start()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Moneda");
        totalCoins = coins.Length;
        UpdateCoinText();
        Time.timeScale = 1;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moneda"))
        {
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject);
            collectedCoins++;
            UpdateCoinText();
            if (collectedCoins == totalCoins)
            {
                win = true;
                Debug.Log("¡Has recolectado todas las monedas!");
            }
        }
    }
    public void Update()
    {
        if (objectToCheck != null && !objectToCheck.activeSelf)
        {
            objectDestroyed = true;
        }

        if (objectDestroyed == true && win == true)
        {
            if (objectToActivate != null)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                objectToActivate.SetActive(true);
            }
        }

    }
    void UpdateCoinText()
    {
        coinText.text = collectedCoins.ToString() + "/" + totalCoins.ToString();
    }

    public void RecargarEscena() 
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
     

    }

    public void CambiarDeEscena()
    {       
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
    }
}
