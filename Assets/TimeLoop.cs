using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoop : MonoBehaviour
{
    // Unit of time: seconds
    private float timer = 0.0f;

    private const float startBubbleTime = 35.0f;
    private const float stopBubbleTime = 80.0f;
    private const float transitionTime = 80.1f;
    private const float endTime = 91.0f;

    private bool isBubblePulsing = false;
    private bool isTransitioning = false;
    private bool isEnding = false;

    private GameObject sphereObj;
    private Bubble bubbleScript;

    [SerializeField]
    private AudioSource transitionVoice;

    void Start()
    {
        sphereObj = GameObject.Find("Sphere");
        bubbleScript = sphereObj.GetComponent<Bubble>();
    }

    // Update is called once per frame
    void Update()
    {
        // Display the current timer value
        Debug.Log("[DEBUG] Timer value: " + timer.ToString());

        // Add elapsed time to timer
        timer += Time.deltaTime;

        // Check if the timer has passed the time limit
        if (timer >= startBubbleTime && !isBubblePulsing)
        {
            Debug.Log("[ALERT] Bubble has started pulsing!");
            bubbleScript.StartPulse();
            isBubblePulsing = true;
        }

        if (timer >= stopBubbleTime && isBubblePulsing)
        {
            Debug.Log("[ALERT] Bubble has stopped pulsing!");
            bubbleScript.EndPulse();
            isBubblePulsing = false;
        }

        if (timer >= transitionTime && !isTransitioning)
        {
            Debug.Log("[ALERT] Playing the transition voice clip!");
            transitionVoice.Play();
            isTransitioning = true;
        }

        if (timer >= endTime && !isEnding)
        {
            Debug.Log("[ALERT] Ending the current scene!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            isEnding = true;
        }
    }
}
