using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class MessageManager : MonoBehaviour
{

    public GameObject messages;
    public GameObject myMessage;
    public GameObject othersMessage;   

    public ApiSample.InputFromJson importantMessages = new ApiSample.InputFromJson();

    private int NORMAL_MESSAGE_PRIORITY = 1;
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
//        Debug.Log("load messages");
    }

    void displayNewMessages(ApiSample.InputFromJson newMessages){
        foreach (ApiSample.GetDataFromKey newMessage in newMessages.result){
            if (newMessage.from == Env.from){
                this.appendMyMessage(newMessage.from, newMessage.content);
            }else if(true){
                if (newMessage.priority > NORMAL_MESSAGE_PRIORITY){
                    // 重要なメッセージは this.importantMessages に追加
                    this.appendNewImportantMessage(newMessage);                    
                }
                this.appendOthersMessage(newMessage.from, newMessage.content);
            }
        }        
    }

    void appendNewImportantMessage(ApiSample.GetDataFromKey newMessage){
        if (this.importantMessages.result.Length > 0){
            this.importantMessages.result = this.importantMessages.result.Concat(new ApiSample.GetDataFromKey[] {newMessage}).ToArray();
        }else{
            this.importantMessages.result = new ApiSample.GetDataFromKey[] {newMessage};
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



    public ApiSample.InputFromJson getImportantMessages(){
        return this.importantMessages;
    }


    public void messagesLoaded(ApiSample.InputFromJson json_data ){
//        Debug.Log("messages loaded");
//        Debug.Log(json_data);
//        this.newMessages = json_data;
        this.displayNewMessages(json_data);

    }
}

