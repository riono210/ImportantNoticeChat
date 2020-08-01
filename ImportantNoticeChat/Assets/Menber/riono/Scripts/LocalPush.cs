using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.iOS;
using UnityEngine;

public static class LocalPush {

    // 通知の全削除
    public static void AllClear () {
        iOSNotificationCenter.RemoveAllScheduledNotifications ();
        iOSNotificationCenter.RemoveAllDeliveredNotifications ();
        // バッジの削除
        iOSNotificationCenter.ApplicationBadge = 0;
    }

    // push通知の登録
    public static void AddSchedule (string title, string message, int badgeCount, int elapsedTime) {
        SetIOSNotification (title, message, badgeCount, elapsedTime);
    }

    private static void SetIOSNotification (string title, string message, int badgeCount, int elapsedTime) {
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
    }

}