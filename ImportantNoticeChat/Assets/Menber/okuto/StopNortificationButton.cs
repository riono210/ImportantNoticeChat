using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopNortificationButton : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        GameObject message = this.transform.parent.gameObject;
        int messageId = message.GetComponent<OthersMessage>().messageId;

        // データベースの変更
        string endpoint = "messages/" + messageId.ToString();
        Debug.Log(endpoint);
        int priority = 1;

        GameObject messageManager = message.transform.parent.parent.Find("MessageManager").gameObject;
        ApiCall apiCall = messageManager.GetComponent<ApiCall>();
        StartCoroutine(apiCall.PutText(endpoint, priority));

        // "重要なメッセージ"という警告文と、ボタンを非表示
        message.transform.Find("Norticification").gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        messageManager.GetComponent<MessageManager>().removeImportantMessage(messageId);

    }
}
