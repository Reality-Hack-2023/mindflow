using io.neuos;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{

    [SerializeField]
    private InputField ipField;
    [SerializeField]
    private InputField portField;
    [SerializeField]
    private Text valuesTextField;
    [SerializeField]
    private Button connectButton;
    [SerializeField]
    private Button disconnectButton;
    [SerializeField]
    private string ApiKey;
    [SerializeField]
    private NeuosStreamClient neuosStreamClient;
    
    
    StringBuilder builder = new StringBuilder();
    StringBuilder arrayBuilder = new StringBuilder();
    private Dictionary<string, string> fields = new Dictionary<string, string>();
    /// <summary>
    /// Method to call that will connect to the Neuos Stream server
    /// </summary>
    public void ConnectToServer()
    {
        if (!neuosStreamClient.IsConnected)
        {
            neuosStreamClient.ApiKey = ApiKey;
            neuosStreamClient.ConnectToServer(ipField.text, int.Parse(portField.text));
        }
    }
    /// <summary>
    /// Method to call that will disconnect from the Neuos Stream server
    /// </summary>
    public void DisconnectFromServer()
    {
        if (neuosStreamClient.IsConnected)
        {
            neuosStreamClient.Disconnect();
        }
    }
    /// <summary>
    /// Callback for when the Neuos Stream server has connected
    /// </summary>
    public void OnServerConnected()
    {
        connectButton.gameObject.SetActive(false);
        disconnectButton.gameObject.SetActive(true);
        // gameObject.GetComponent<Renderer>().material.color = Color.green;  
    }
    /// <summary>
    /// Callback for when the Neuos Stream server has disconnected
    /// </summary>
    public void OnServerDisconnected()
    {
        connectButton.gameObject.SetActive(true);
        disconnectButton.gameObject.SetActive(false);

    }
    /// <summary>
    /// Callback for when the Neuos Stream server sends an updated value
    /// </summary>
    /// <param name="key">The key of the value</param>
    /// <param name="value">The actual value</param>
    public void OnValueChanged(string key, float value)
    {
        // here we store the value into our dictionary
        fields[key] = value.ToString();
        
        if (key == "focus" && value > 20) {
            gameObject.GetComponent<Renderer>().material.color = Color.red;   
        }
        // gameObject.GetComponent<Renderer>().material.color = Color.blue;  

        UpdateUI();
    }
    /// <summary>
    /// Callback for when the Neuos Stream server sends an updated value
    /// </summary>
    /// <param name="key">The key of the value</param>
    /// <param name="value">The actual value</param>
    public void OnArrayValueChanged(string key, float[] value)
    {
        // here we store the value into our dictionary
        arrayBuilder.Clear();
        foreach (var kvp in value)
        {
            // add each key value pair as a line to the string builder
            arrayBuilder.Append($"{kvp},");
        }
        // update the UI text value with the value of the new string builder
        arrayBuilder.Length--;
        fields[key] = arrayBuilder.ToString();
        UpdateUI();
    }
    /// <summary>
    /// Callback for when the connection status of the headband changes
    /// </summary>
    /// <param name="prev"></param>
    /// <param name="curr"></param>
    public void OnHeadbandConnectionChange(int prev, int curr)
    {
        // values defined in NeuosStreamConstants.ConnectionState
        fields["HeadbandConnection"] = $"Current : {curr} Previous : {prev}";
        UpdateUI();
    }
    /// <summary>
    /// Callback for when the Neuos Stream server updates its QA model
    /// </summary>
    /// <param name="passed">Did QA test pass</param>
    /// <param name="reason">If failed, what was the reason for failure</param>
    public void OnQAMessage(bool passed, int reason)
    {
        // reasons defined in NeuosStreamConstants.QAFailureType
        fields["QA"] = $"Passed : {passed} Reason : {reason}";
        UpdateUI();
    }
    /// <summary>
    /// Called whenever the Neuos Stream server reports an error
    /// </summary>
    /// <param name="message">The error message</param>
    public void OnError(string message)
    {
        fields["Last error"] = message;
        UpdateUI();
    }
    /// <summary>
    /// Updates the ui
    /// </summary>
    private void UpdateUI()
    {
        // clears the string builder
        builder.Clear();
        // iterate over the dictionary
        foreach (var kvp in fields)
        {
            // add each key value pair as a line to the string builder
            builder.AppendLine($"{kvp.Key} : {kvp.Value}");
        }
        // update the UI text value with the value of the new string builder
        valuesTextField.text = builder.ToString();
    }


}


