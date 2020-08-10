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
        Debug.Log(message);
        Debug.Log(message.transform.parent);
        Debug.Log(message.transform.parent.parent);
        MessageManager messageManager = message.transform.parent.parent.Find("MessageManager").GetComponent<MessageManager>();

        Debug.Log(messageManager);

        messageManager.selectedMessage = message;

        GameObject reminderWindow = messageManager.reminderWindow;
        ReminderWindow temp = reminderWindow.GetComponent<ReminderWindow>();        
        reminderWindow.gameObject.SetActive(true);
    }
}
