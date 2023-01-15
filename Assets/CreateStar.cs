using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CreateStar : MonoBehaviour
{
    public GameObject FX_Sparkle;

    public void Generate() {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-1000, 1000), 5, Random.Range(-1000, 1000));
        Instantiate(FX_Sparkle, randomSpawnPosition, Quaternion.identity);
    }

    public void StartCreate()
    {
        InvokeRepeating("Generate", 1.0f, 1.0f);
    }

    public void StopCreate()
    {
        CancelInvoke();
    }
}


