using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text; // byteに変換するために使う

public class ApiPost : MonoBehaviour
{
    
    private void PostMessage(string endpoint){
        string base_url = Env.GetBaseUrl();
        string sec_key = Env.GetSecKey();
        string post_data = "";
        // byteに変換してあげないとサーバーが認識しない
        byte[] postData = Encoding.UTF8.GetBytes(post_data);

        var post_request = new UnityWebRequest(base_url+endpoint, "POST"); 
        post_request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
        post_request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        post_request.SetRequestHeader("Cookie",sec_key);
        post_request.SetRequestHeader("Content-Type", "application/json");
    }
}
