using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class TransactionCallback  : AndroidJavaProxy
    {

        private const string pluginName = "com.paytm.unityaioplugin.AIOSdkPluginHelper";

        public TransactionCallback() : base(pluginName + "$onTransactionCallback")
        {
        }
        public void onTransactionSuccess(string msg, string data)
        {
            Debug.Log("TransactionCallback: onTransactionSuccess:" + msg);
            Debug.Log("TransactionCallback: Data:" + data);
            Transaction.setResponse("Message: " + msg +"\nResponse:"+data);

        }

        public void onTransactionError(string msg)
        {
            Debug.Log("TransactionCallback: onTransactionError" + msg);
            Transaction.setResponse("Message: " + msg);

        }
    }

