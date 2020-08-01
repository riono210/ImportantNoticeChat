using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushNotificationTest : MonoBehaviour {
    // // Start is called before the first frame update
    // void Start () {
    //     LocalPush.AddSchedule ("プッシュ通知のテスト", "korehatestdesuka?", 1, 10);
    // }

    // // Update is called once per frame
    // void Update () {

    // }

    private void OnApplicationPause (bool pauseStatus) {
        if (pauseStatus) {
            LocalPush.AddSchedule ("プッシュ通知のテスト", "勝ったな...!", 1, 10);
        }
    }
}