using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Usar esta línea si usas TextMeshPro
using UnityEngine.SceneManagement; // Importar el espacio de nombres necesario


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
    }

    public void CambiarDeEscena()
    {       
            SceneManager.LoadScene("MainMenu");       
    }
}
