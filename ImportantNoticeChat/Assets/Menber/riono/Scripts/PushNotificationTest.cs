using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Notifications.iOS;
using UnityEngine;

public class PushNotificationTest : MonoBehaviour {
    int badgeCount = 1;
    bool status;

    // バックグラウンド時の動作
    private void OnApplicationPause (bool pauseStatus) {
        status = pauseStatus;
        if (pauseStatus) {
            while (true) {
                LocalPush.AddSchedule ("プッシュ通知のテスト", "勝ったな...!", badgeCount, 1);
                badgeCount += 1;

                Debug.Log ("pushhhhhh");
                // LocalPush.AddSchedule ("プッシュ通知のテスト", "そのに", badgeCount, 1);
                // badgeCount += 1;

                if (!status) {
                    Debug.Log ("Loop Finish!!!!");
                }

                WaitFunction (2f);

                Debug.Log ("next loop");
            }
        }
    }

    // ゲームがフォーカスされているか
    private void OnApplicationFocus (bool pauseStatus) {
        status = !pauseStatus;
        iOSNotificationCenter.ApplicationBadge = 0;
    }

    public IEnumerator ConsecutiveNotice () {

        while (true) {
            LocalPush.AddSchedule ("プッシュ通知のテスト", "勝ったな...!", badgeCount, 1);
            badgeCount += 1;

            Debug.Log ("pushhhhhh");
            // LocalPush.AddSchedule ("プッシュ通知のテスト", "そのに", badgeCount, 1);
            // badgeCount += 1;

            if (!status) {
                Debug.Log ("Loop Finish!!!!");
                yield break;
            }

            yield return new WaitForSecondsRealtime (2f);

            Debug.Log ("next loop");
        }
    }

    public void WaitFunction (float time) {
        float waitTime = Time.realtimeSinceStartup + time;
        while (Time.realtimeSinceStartup < waitTime) {
            Thread.Sleep (100);
        }
    }
}