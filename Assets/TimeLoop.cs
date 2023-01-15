using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLoop : MonoBehaviour
{
    // Unit of time: seconds
    private float timer = 0.0f;

    // Time constants for bubble pulsing
    private const float startBubbleTime = 30.0f;
    private const float stopBubbleTime = 60.0f;

    private bool isBubblePulsing = false;

    private GameObject sphereObj;
    private Bubble bubbleScript;

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
    }
}
