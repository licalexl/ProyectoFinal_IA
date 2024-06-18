using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FuzzyLogic : MonoBehaviour
{
   // public GameObject CanvasWin;
    //public TextMeshProUGUI textMunicion;
    public Transform player; 
    public float rotationSpeed = 2.0f;
    public EnemyShoot enemyShoot;

    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private Vector3 dashDirection;



    public Slider slider;
    public enum State
    {
        MovingToHealth,
        MovingToAmmo,
        FleeingPlayer,
        MovingRandomly
    }

    public GameObject healthObject;
    public GameObject ammoObject;
    public GameObject playerObject;
    public float fleeDistance = 10f;
    public float health = 200f;
    public float ammo; 

    private NavMeshAgent agent;
    public State state;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.MovingRandomly;
        slider.value = health;
        StartCoroutine(Pensar());
    }

    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            //CanvasWin.SetActive(true);
           // Invoke("RecargarScene", 1.5f);

        }


        slider.value = health;
        ammo = enemyShoot.currentBullets;
       // textMunicion.text = "Ammo: " + ammo.ToString();

        if (canDash==true)
        {
            Dash();
        }

        
        switch (state)
        {
            case State.MovingToHealth:
                agent.SetDestination(healthObject.transform.position);
                break;
            case State.MovingToAmmo:
                agent.SetDestination(ammoObject.transform.position);
                break;
            case State.FleeingPlayer:
                Vector3 randomDirection = transform.position - playerObject.transform.position;                
                NavMeshHit navHit;

                if (NavMesh.SamplePosition(randomDirection, out navHit, fleeDistance, NavMesh.AllAreas))
                {
                    agent.SetDestination(navHit.position);
                }
                break;
            case State.MovingRandomly:
                if (!agent.hasPath)
                {
                    Vector3 randomPosition2 = Random.insideUnitSphere * 20f;
                    agent.SetDestination(randomPosition2);
                }
                break;
        }



        Vector3 direction2 = player.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction2, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);



       

    }

    public void MoveToHealth()
    {
        state = State.MovingToHealth;
    }

    public void MoveToAmmo()
    {
        state = State.MovingToAmmo;
    }

    public void FleePlayer()
    {
        state = State.FleeingPlayer;
    }

    public void MoveRandomly()
    {
        state = State.MovingRandomly;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Recarga"))
        {
            enemyShoot.Recarga();
        }

        if (other.CompareTag("Vida"))
        {
            health = health + 60;
            if (health >100 )
            {
                health = 100;
            }
        }

        if (other.CompareTag("Espada"))
        {
            health = health-30;
        }
    }
    IEnumerator Pensar() 
    {

        if (health <= 40)
        {
            state = State.MovingToHealth;
        }
        else if (health < 50 && Vector3.Distance(healthObject.transform.position, playerObject.transform.position) > 7 && ammo >=10f)
        {
            state = State.MovingToHealth;
        }
        else if (ammo <= 0)
        {
            state = State.MovingToAmmo;
        }
        else if (ammo <= 25 && Vector3.Distance(ammoObject.transform.position, playerObject.transform.position) > 7)
        {
            state = State.MovingToAmmo;
        }
        else if (Vector3.Distance(transform.position, playerObject.transform.position) < fleeDistance)
        {
            state = State.FleeingPlayer;
        }
        else if (health >=80 && ammo >= 35)
        {
            float distanceToHealth = Vector3.Distance(playerObject.transform.position, healthObject.transform.position);
            float distanceToAmmo = Vector3.Distance(playerObject.transform.position, ammoObject.transform.position);
            state = distanceToHealth > distanceToAmmo ? State.MovingToHealth : State.MovingToAmmo;
        }
        else
        {
            if (Random.value < 0.1f)
            {
                state = State.MovingRandomly;
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Pensar());
    }

    void Dash()
    {
        canDash = false;

        Vector3 dashDirection = agent.velocity.normalized;

        StartCoroutine(PerformDash(dashDirection * dashDistance, dashDuration));
       
    }

    IEnumerator PerformDash(Vector3 moveDistance, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.Translate(moveDistance * Time.deltaTime / duration, Space.World);

            elapsedTime += Time.deltaTime;
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public void RecargarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
