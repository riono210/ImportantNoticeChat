using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {
    public GameObject messages;
    public GameObject myMessage;
    public GameObject othersMessage;
    public GameObject reminderWindow;
    public GameObject selectedMessage;
    public ApiSample.InputFromJson importantMessages = new ApiSample.InputFromJson ();

    private int NORMAL_MESSAGE_PRIORITY = 1;

    // スクロールビューを一番下にする
    private bool firstGet = true;
    [SerializeField] private ScrollAutoDown autoDown;
    //    private ApiSample.InputFromJson newMessages;

    // Start is called before the first frame update
    void Start () { }

    // Update is called once per frame
    void Update () {
        //        this.loadMessagesFromDatabase();
    }

    void loadMessagesFromDatabase () {
        //        Debug.Log("load messages");
    }

    void displayNewMessages (ApiSample.InputFromJson newMessages) {
        foreach (ApiSample.GetDataFromKey newMessage in newMessages.result) {
            if (newMessage.from == Env.from) {
                this.appendMyMessage (newMessage.from, newMessage.content);
            } else if (true) {
                if (newMessage.priority > NORMAL_MESSAGE_PRIORITY) {
                    // 重要なメッセージは this.importantMessages に追加
                    this.appendNewImportantMessage (newMessage);
                }
                this.appendOthersMessage (newMessage.from, newMessage.content, newMessage.id, newMessage.priority);
            }
        }

        StartCoroutine (autoDown.AutoSetScroll ());
    }

    void appendNewImportantMessage (ApiSample.GetDataFromKey newMessage) {
        if (this.importantMessages.result.Length > 0) {
            this.importantMessages.result = this.importantMessages.result.Concat (new ApiSample.GetDataFromKey[] { newMessage }).ToArray ();
        } else {
            this.importantMessages.result = new ApiSample.GetDataFromKey[] { newMessage };
        }
    }

    void appendMyMessage (string userName, string content) {
        GameObject newMessage = Instantiate (this.myMessage);
        newMessage.transform.SetParent (this.messages.transform);

        Text messageTextNode = newMessage.transform.Find ("MessageText").gameObject.GetComponent<Text> ();
        messageTextNode.text = content;

        Text userNameNode = newMessage.transform.Find ("User/Name").gameObject.GetComponent<Text> ();
        userNameNode.text = userName;
    }

    void appendOthersMessage (string userName, string content, int id, int priority) {
        // Instantiate
        GameObject newMessage = Instantiate (this.othersMessage);
        // set userName
        Text userNameNode = newMessage.transform.Find ("User/Name").gameObject.GetComponent<Text> ();
        userNameNode.text = userName;
        // set content
        Text messageTextNode = newMessage.transform.Find ("MessageText").gameObject.GetComponent<Text> ();
        messageTextNode.text = content;
        // set id
        newMessage.GetComponent<OthersMessage> ().messageId = id;
        // set priority
        if (priority > NORMAL_MESSAGE_PRIORITY) {
            newMessage.transform.Find ("Norticification").gameObject.SetActive (true);
            newMessage.transform.Find ("StopNorticificationButton").gameObject.SetActive (true);
        }

        switch (priority) {
            case 10:
                newMessage.transform.Find ("Panel").gameObject.GetComponent<Outline> ().effectColor = ToRGB (0xF1F312);
                break;
            case 100:
                newMessage.transform.Find ("Panel").gameObject.GetComponent<Outline> ().effectColor = ToRGB (0xF12A2A);
                break;
            default:
                newMessage.transform.Find ("Panel").gameObject.GetComponent<Outline> ().effectColor = ToRGB (0x4FE722);
                break;
        }

        newMessage.transform.SetParent (this.messages.transform);
    }

    public ApiSample.InputFromJson getImportantMessages () {
        return this.importantMessages;
    }

    public void messagesLoaded (ApiSample.InputFromJson json_data) {
        //        Debug.Log("messages loaded");
        //        Debug.Log(json_data);
        //        this.newMessages = json_data;
        this.displayNewMessages (json_data);

    }

    public void removeImportantMessage (int removeId) {
        this.importantMessages.result = this.importantMessages.result.Where ((data, index) => data.id != removeId).ToArray ();
    }

    public void updatePriority () {
        int messageId = this.selectedMessage.GetComponent<OthersMessage> ().messageId;

        // データベースの変更
        string endpoint = "messages/" + messageId.ToString ();
        Debug.Log (endpoint);
        int priority = 1;

        ApiCall apiCall = this.GetComponent<ApiCall> ();
        StartCoroutine (apiCall.PutText (endpoint, priority));

        // "重要なメッセージ"という警告文と、ボタンを非表示
        this.selectedMessage.transform.Find ("Norticification").gameObject.SetActive (false);
        this.selectedMessage.transform.Find ("StopNorticificationButton").gameObject.SetActive (false);
        // 枠線を緑に変更
        this.selectedMessage.transform.Find ("Panel").gameObject.GetComponent<Outline> ().effectColor = ToRGB (0x4FE722);
        this.removeImportantMessage (messageId);
    }

    private Color ToRGB (uint val) {
        var inv = 1f / 255f;
        var c = Color.black;
        c.r = inv * ((val >> 16) & 0xFF);
        c.g = inv * ((val >> 8) & 0xFF);
        c.b = inv * (val & 0xFF);
        c.a = 1f;
        return c;
    }
}