using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageManager : MonoBehaviour
{

    public GameObject messages;
    public GameObject myMessage;
    public GameObject othersMessage;    
//    private ApiSample.InputFromJson newMessages;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        this.loadMessagesFromDatabase();
    }

    void loadMessagesFromDatabase(){
        Debug.Log("load messages");
    }

    void displayNewMessages(ApiSample.InputFromJson newMessages){
        foreach (ApiSample.GetDataFromKey newMessage in newMessages.result){
            if (newMessage.from == Env.from){
                this.appendMyMessage(newMessage.from, newMessage.content);
            }else if(true){
                this.appendOthersMessage(newMessage.from, newMessage.content);
            }
        }
    }
      void appendMyMessage(string userName, string content){
        GameObject newMessage = Instantiate(this.myMessage);
        newMessage.transform.SetParent(this.messages.transform);

        Text messageTextNode = newMessage.transform.Find("Message").gameObject.GetComponent<Text>();
        messageTextNode.text = content;

        Text userNameNode = newMessage.transform.Find("User/Name").gameObject.GetComponent<Text>();
        userNameNode.text = userName;
    }

    void appendOthersMessage(string userName, string content){
        GameObject newMessage = Instantiate(this.othersMessage);
        newMessage.transform.SetParent(this.messages.transform);

        Text messageTextNode = newMessage.transform.Find("Message").gameObject.GetComponent<Text>();
        messageTextNode.text = content;

        Text userNameNode = newMessage.transform.Find("User/Name").gameObject.GetComponent<Text>();
        userNameNode.text = userName;
    }



    void getImportantMessages(){

    }


    public void messagesLoaded(ApiSample.InputFromJson json_data ){
        Debug.Log("messages loaded");
        Debug.Log(json_data);
//        this.newMessages = json_data;
        this.displayNewMessages(json_data);

    }
}

