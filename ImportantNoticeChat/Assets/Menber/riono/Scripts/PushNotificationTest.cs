using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Notifications.iOS;
using UnityEngine;

public class PushNotificationTest : MonoBehaviour {
    private int badgeCount;

    //private TimerCallback timerCallback;
    [SerializeField] private MessageManager messageManager; // 重要メッセージを取得する
    private ApiSample.GetDataFromKey[] getDataFromKeys; // 重要メッセージを格納する

    void Start () {
        badgeCount = 1;
    }

    // バックグラウンド時の動作
    private void OnApplicationPause (bool pauseStatus) {
        if (pauseStatus) {
            // 重要メッセージ一覧を取得
            getDataFromKeys = messageManager.getImportantMessages ().result;
            Debug.Log ("重要メッセージ数:" + getDataFromKeys.Length);
            for (int i = 0; i < getDataFromKeys.Length; i++) {

                // 関数呼び出し
                PushNoticeSet (getDataFromKeys[i].content, getDataFromKeys[i].priority);
            }
            Debug.Log ("設定完了！！！");
        } else {
            LocalPush.AllClear ();
        }
    }

    // 重要度によって通知間隔を全てセット、
    private void PushNoticeSet (string contnt, int priority) {
        DateTime nowTime = DateTime.Now;
        DateTime todayMidnight = new DateTime (nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 00);
        TimeSpan ts = todayMidnight - nowTime;
        int totalMSecond = ((ts.Hours * 3600) + ts.Seconds) * 1000; // 真夜中までのミリ秒の差

        int moreInportantSecond = 2;
        int inportantSecond = 10;

        if (priority == 100) {
            int span = totalMSecond / (moreInportantSecond * 1000);
            for (int i = 0; i < span - 1; i++) {
                LocalPush.AddSchedule ("NoticeChat", contnt, badgeCount, i * moreInportantSecond + 1, "null");
                badgeCount++;
            }
        } else if (priority == 10) {
            int span = totalMSecond / (inportantSecond * 1000);
            for (int i = 0; i < span - 1; i++) {
                LocalPush.AddSchedule ("NoticeChat", contnt, badgeCount, i * inportantSecond + 1, "null");
                badgeCount++;
            }
        }
    }

    // private void BackgroundLoop (object o) {
    //     //LocalPush.AddSchedule ("プッシュ通知のテスト", "バックグラウンドです", badgeCount, 1);
    //     LocalPush.AddSchedule ("プッシュ通知のテスト", "重要！content", badgeCount, 1, "_inchat");
    //     badgeCount++;
    // }

    // public IEnumerator ConsecutiveNotice () {

    //     while (true) {
    //         LocalPush.AddSchedule ("プッシュ通知のテスト", "勝ったな...!", badgeCount, 1);
    //         badgeCount += 1;

    //         Debug.Log ("pushhhhhh");
    //         // LocalPush.AddSchedule ("プッシュ通知のテスト", "そのに", badgeCount, 1);
    //         // badgeCount += 1;

    //         if (!status) {
    //             Debug.Log ("Loop Finish!!!!");
    //             yield break;
    //         }

    //         yield return new WaitForSecondsRealtime (2f);

    //         Debug.Log ("next loop");
    //     }
    // }

    // public void WaitFunction (float time) {
    //     float waitTime = Time.realtimeSinceStartup + time;
    //     while (Time.realtimeSinceStartup < waitTime) {
    //         Thread.Sleep (100);
    //     }
    // }
}