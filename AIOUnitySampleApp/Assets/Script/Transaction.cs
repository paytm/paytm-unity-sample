using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Transaction : MonoBehaviour
{
    public TMP_InputField etOrderId;
    public TMP_InputField etMid;
    public TMP_InputField etTxnToken;
    public TMP_InputField etCallbackUrl;
    public TMP_InputField etAmount;
    public Toggle environmentToggle;
    public Toggle restrictAppInvokeToggle;
    public GameObject responseTextObject;
    public static TMP_Text responseText;

    public static string responseUpdated = "Response";
    public static bool isResponseUpdated = false;

     void Start() {
        responseText = responseTextObject.GetComponent<TMP_Text>();
     }

     void Update(){
        if(isResponseUpdated){
            responseText.text = responseUpdated;
            isResponseUpdated = false;
        }
     }
    
    public static void setResponse(string message){
        Debug.Log("Final Response -> " + message);
        responseUpdated = " " + message;
        isResponseUpdated = true;
    }

    public void startPayment(){
        #if UNITY_IOS
            Debug.Log("Ios not implemented");
        #elif UNITY_ANDROID
            AllInOneSDK.startTransaction(etOrderId.text, etMid.text, etAmount.text, etTxnToken.text, etCallbackUrl.text, environmentToggle.isOn, restrictAppInvokeToggle.isOn);
        #endif
    }

}
