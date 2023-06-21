using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;

public class AllInOneSDK
{
    #if UNITY_IOS
    [DllImport("__Internal")]
private static extern void _startPaymentForPaytm(string orderId, string merchantId, string amount, string transactionToken, string callbackUrl, bool isStaging, bool isAppInvokeRestricted); 
public static void startPaymentForPaytm(string orderId, string merchantId, string amount, string transactionToken, string callbackUrl, bool isStaging, bool isAppInvokeRestricted) { 
_setDelegate(delegateMessageReceived);
_startPaymentForPaytm(orderId, merchantId, amount, transactionToken, callbackUrl, isStaging, isAppInvokeRestricted);
}
    [DllImport("__Internal")]
    private static extern void _setDelegate(DelegateMessage callback);
    private delegate void DelegateMessage(string response);
    [MonoPInvokeCallback(typeof(DelegateMessage))] 
    private static void delegateMessageReceived(string response) {
      Debug.Log("Message received in AppInvokePlugin : " + response);
    }
    

#elif UNITY_ANDROID

    public static void startTransaction(string orderId, string mid, string amount, string txnToken, string callbackUrl, bool isStaging, bool isAppInvokeRestricted)
    {

        const string pluginName = "com.paytm.unityaioplugin.AIOSdkPluginHelper";

        AndroidJavaClass pluginClass = new AndroidJavaClass(pluginName);
        AndroidJavaObject pluginInstance = null;

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject mCurrentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log("Current activity " + mCurrentActivity);

        if (mCurrentActivity != null && pluginClass != null)
        {
            Debug.Log("Android pluginClass created");
            pluginClass.SetStatic<AndroidJavaObject>("mainActivity", mCurrentActivity);
        }

        if (pluginClass != null)
        {
            pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
            Debug.Log("Android pluginInstance created");

        }

        pluginInstance.Call("startTransactionApi", new object[] { mid, orderId, amount, txnToken, callbackUrl, isStaging, isAppInvokeRestricted, new TransactionCallback()});

    }

#endif
}
