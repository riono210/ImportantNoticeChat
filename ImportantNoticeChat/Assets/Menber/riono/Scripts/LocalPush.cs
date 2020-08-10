using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif

public static class LocalPush {

    public static void RegisterChannel (string cannelId, string title, string description) {
#if UNITY_ANDROID
        var c = new AndroidNotificationChannel () {
            Id = cannelId,
            Name = title,
            Importance = Importance.High,
            Description = description,
        };
        AndroidNotificationCenter.RegisterNotificationChannel (c);
#endif
    }

    // 通知の全削除
    public static void AllClear () {
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllScheduledNotifications ();
        AndroidNotificationCenter.CancelAllNotifications ();
#elif UNITY_IOS 
        iOSNotificationCenter.RemoveAllScheduledNotifications ();
        iOSNotificationCenter.RemoveAllDeliveredNotifications ();
        // バッジの削除
        iOSNotificationCenter.ApplicationBadge = 0;
#endif
    }

    // push通知の登録
    public static void AddSchedule (string title, string message, int badgeCount, int elapsedTime, string cannelId) {
#if UNITY_ANDROID
        SetAndroidMotification (title, message, badgeCount, elapsedTime, cannelId);
#elif UNITY_IOS
        SetIOSNotification (title, message, badgeCount, elapsedTime);
#endif
    }

    private static void SetIOSNotification (string title, string message, int badgeCount, int elapsedTime) {
#if UNITY_IOS
        // 通知の作成
        iOSNotificationCenter.ScheduleNotification (new iOSNotification {
            Identifier = $"_notification_{badgeCount}",
                Title = title,
                Body = message,
                ShowInForeground = false,
                Badge = badgeCount,
                Trigger = new iOSNotificationTimeIntervalTrigger () {
                    TimeInterval = new TimeSpan (0, 0, elapsedTime),
                        Repeats = false
                }
        });
#endif
    }

    private static void SetAndroidMotification (string title, string message, int badgeCount, int elapsedTime, string cannelId) {
#if UNITY_ANDROID
        // 通知を作成します。
        var notification = new AndroidNotification {
            Title = title,
            Text = message,
            Number = badgeCount,
            // ※ここでAndroidのアイコンを設定します。
            SmallIcon = "icon_small",
            LargeIcon = "icon_large",
            FireTime = DateTime.Now.AddSeconds (elapsedTime)
        };

        AndroidNotificationCenter.SendNotification (notification, cannelId);
#endif
    }
}