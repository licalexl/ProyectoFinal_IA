using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovementSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float stopDelay = 0.2f;

    private Vector3 lastPosition;
    public bool isMoving = false;
    private float stationaryTime = 0f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        lastPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            if (!isMoving)
            {
                audioSource.Play();
                isMoving = true;
                stationaryTime = 0f;
            }
            else
            {
                stationaryTime = 0f;
            }
            lastPosition = transform.position;
        }
        else
        {
            if (isMoving)
            {
                stationaryTime += Time.deltaTime;
                if (stationaryTime >= stopDelay)
                {
                    audioSource.Stop();
                    isMoving = false;
                }
            }
        }
    }
}
