using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoop2 : MonoBehaviour
{
    // Unit of time: seconds
    private float timer = 0.0f;

    private const float startCreateTime = 30.0f;
    private const float stopCreateTime = 45.0f;
    private const float transitionTime = 45.1f;
    private const float endTime = 55.0f;

    private bool allowCreate = false;
    private bool isTransitioning = false;
    private bool isEnding = false;

    private GameObject gameObj;
    private CreateStar starScript;

    [SerializeField]
    private AudioSource transitionVoice;

    void Start()
    {
        gameObj = GameObject.Find("GameObject");
        starScript = gameObj.GetComponent<CreateStar>();
    }

    // Update is called once per frame
    void Update()
    {
        // Display the current timer value
        Debug.Log("[DEBUG] Timer value: " + timer.ToString());

        // Add elapsed time to timer
        timer += Time.deltaTime;

        // Check if the timer has passed the time limit
        if (timer >= startCreateTime && !allowCreate)
        {
            Debug.Log("[ALERT] Star creation has started!");
            starScript.StartCreate();
            allowCreate = true;
        }

        if (timer >= stopCreateTime && allowCreate)
        {
            Debug.Log("[ALERT] Star creation has stopped!");
            starScript.StopCreate();
            allowCreate = false;
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
