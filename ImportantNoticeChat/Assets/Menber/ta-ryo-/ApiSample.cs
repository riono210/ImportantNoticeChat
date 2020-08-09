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
        string last_update_time = "";
        while(true){
            UnityWebRequest getRequest = UnityWebRequest.Get(base_url + endpoint + timestamp);
            yield return getRequest.SendWebRequest();
            Debug.Log($"StatusCode: {getRequest.responseCode}");
            var get_text = getRequest.downloadHandler.text;
            Debug.Log(get_text);


            InputFromJson json_data = JsonUtility.FromJson<InputFromJson>(get_text);
            messageManager.messagesLoaded( json_data ); // メッセージの表示

            // 空配列（更新されてない）の場合は次のループへ
            if (json_data.result.Length == 0){
                continue;
            }

            // 最終更新時間を設定
            GetDataFromKey last_message = json_data.result.Last();

            int second = int.Parse(last_message.timestamp.Substring(last_message.timestamp.Length-2, 2));
            last_update_time = last_message.timestamp.Substring(0, last_message.timestamp.Length-2) + (second+1).ToString("00");
            timestamp = "/after/" + last_update_time ;     
            Debug.Log(timestamp);

            yield return new WaitForSeconds(10);
        }    
        
        // classの箱を用意しての方法
        // 叩いたAPIのデータが文字列型なのでInputFromJsonで宣言した型でjsonとしてパースしてる
//        Debug.Log(json_data.result[0].content);
//        Debug.Log(json_data.result[1].content);
//        Debug.Log(json_data.result[2].content);
    }
}
