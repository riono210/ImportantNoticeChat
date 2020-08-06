using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestButton : MonoBehaviour
{
    public GameObject myMessage;
    public GameObject othersMessage;

    public GameObject messages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
       this.appendMyMessage("testUser", "Hello");
       this.appendOthersMessage("testOthersUser", "World!");
    }

    void appendMyMessage(string userName, string message){
        GameObject newMessage = Instantiate(this.myMessage);
        newMessage.transform.SetParent(this.messages.transform);

        Text messageTextNode = newMessage.transform.Find("Message").gameObject.GetComponent<Text>();
        messageTextNode.text = message;

        Text userNameNode = newMessage.transform.Find("User/Name").gameObject.GetComponent<Text>();
        userNameNode.text = userName;
    }

    void appendOthersMessage(string userName, string message){
        GameObject newMessage = Instantiate(this.othersMessage);
        newMessage.transform.SetParent(this.messages.transform);

        Text messageTextNode = newMessage.transform.Find("Message").gameObject.GetComponent<Text>();
        messageTextNode.text = message;

        Text userNameNode = newMessage.transform.Find("User/Name").gameObject.GetComponent<Text>();
        userNameNode.text = userName;
    }
}
