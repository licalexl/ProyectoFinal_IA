using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI Tvida;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public bool isAttacking = false;
    public float vida = 100;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Tvida.text = "Vida: "+vida.ToString();
        if (vida <=0)
        {
            gameObject.SetActive(false);
            Invoke("RecargarScene", 0.5f);
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            animator.SetBool("Atacando", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            animator.SetBool("Atacando", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            vida = vida - 5F;
        }
    }

    public void RecargarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
