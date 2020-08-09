using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
public class ApiSample : MonoBehaviour
{
    // Jsonデータにパースするための中身を解析するための地図的な
   // [System.Serializable]
   // public class InputFromJson 
   // {
   //     public string[] result;
   // }

    public MessageManager messageManager;

    [System.Serializable]
    public class InputFromJson 
    {
        public GetDataFromKey[] result;
    }

    [System.Serializable]
    public class GetDataFromKey 
    {
        public int id;
        public string content;
        public string to;
        public string from;
        public int priority;
        public string timestamp;
    }

    void Start()
    {
        StartCoroutine(GetMessage("messages"));
    }

    IEnumerator GetMessage(string endpoint)
    {
        string base_url = Env.GetBaseUrl();
        string timestamp = "";
        while(true){
            UnityWebRequest getRequest = UnityWebRequest.Get(base_url + endpoint + timestamp);
            yield return getRequest.SendWebRequest();
            Debug.Log($"StatusCode: {getRequest.responseCode}");
            var get_text = getRequest.downloadHandler.text;
            Debug.Log(get_text);


            InputFromJson json_data = JsonUtility.FromJson<InputFromJson>(get_text);
            messageManager.messagesLoaded( json_data );
            string last_update_time = json_data.result.Last().timestamp;
            timestamp = "/after/" + last_update_time ;
            yield return new WaitForSeconds(10);

        }    
        
        // classの箱を用意しての方法
        // 叩いたAPIのデータが文字列型なのでInputFromJsonで宣言した型でjsonとしてパースしてる
//        Debug.Log(json_data.result[0].content);
//        Debug.Log(json_data.result[1].content);
//        Debug.Log(json_data.result[2].content);
    }
}
