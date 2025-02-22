
#if  UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
using System;

namespace Managers
{
    public class NotificationManager : MonoBehaviour
    {

        void Start()
        {
            
            if (PlayerPrefs.HasKey("NotificationSent"))return;
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            //SkinNotification();
            GameNotification();
            //MoneyNotification();
            //CompetitionNotification();
            
        }

        private void GameNotification()
        {
            PlayerPrefs.SetInt("NotificationSent", 1);
            var channel = new AndroidNotificationChannel()
            {
                Id = "game_channel",
                Name = "game Channel",
                Importance = Importance.Default,
                Description = "game notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            var notification = new AndroidNotification();
            notification.Title = "Alien Sniper";
            notification.Text = "Please Help! Alien's are Invading The Earth Abducting People!";
            notification.SmallIcon = "app_icon_small";
            notification.LargeIcon = "app_icon_large";
            notification.RepeatInterval = new TimeSpan(12,0,0);
            notification.FireTime = DateTime.Now.AddHours(12);

            var id = AndroidNotificationCenter.SendNotification(notification, "game_channel");

            var notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(id);

            if (notificationStatus == NotificationStatus.Scheduled)
            {
                // Remove the previously shown notification from the status bar.
                //AndroidNotificationCenter.CancelNotification(id);
                //AndroidNotificationCenter.SendNotification(notification, "game_channel");
            }
            else if (notificationStatus == NotificationStatus.Delivered)
            {
                // Remove the previously shown notification from the status bar.
                AndroidNotificationCenter.CancelNotification(id);
                AndroidNotificationCenter.SendNotification(notification, "game_channel");
            }

        }





        private void ONShootingRangeMoneyCount(int obj)
        {
            FortuneWheelNotification();
        }

        private void FortuneWheelNotification()
        {
            if (PlayerPrefs.HasKey("FortuneWheelNotificationSent"))
            {
                PlayerPrefs.DeleteKey("FortuneWheelNotificationSent");
                var i = PlayerPrefs.GetInt("FortuneWheelId");
                PlayerPrefs.DeleteKey("FortuneWheelId");
                AndroidNotificationCenter.CancelNotification(i);
            }
            var channel = new AndroidNotificationChannel()
            {
                Id = "FortuneWheel_channel_id",
                Name = "FortuneWheel Channel",
                Importance = Importance.High,
                Description = "FortuneWheel notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            
            var notification = new AndroidNotification();
            notification.Title = "Zombie Sniper Survival";
            notification.Text = "Use Your Free Daily Spin Now!";
            notification.SmallIcon = "app_icon_small";
            notification.LargeIcon = "wheel_icon_large";

            notification.FireTime = System.DateTime.Now.AddHours(24);

            var id = AndroidNotificationCenter.SendNotification(notification, "FortuneWheel_channel_id");
            PlayerPrefs.SetInt("FortuneWheelId",id);
            PlayerPrefs.SetInt("FortuneWheelNotificationSent",1);
        }

        private void SkinNotification()
        {
            if (PlayerPrefs.HasKey("SkinNotificationSent")) return;
            var channel = new AndroidNotificationChannel()
            {
                Id = "skin_channel_id",
                Name = "Skin Channel",
                Importance = Importance.High,
                Description = "Skin notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            
            var notification = new AndroidNotification();
            notification.Title = "Zombie Sniper Survival";
            notification.Text = "New skin is unlocked! Get it now!";
            notification.SmallIcon = "app_icon_small";
            notification.LargeIcon = "skin_icon_large";

            notification.FireTime = System.DateTime.Now.AddHours(36);

            AndroidNotificationCenter.SendNotification(notification, "skin_channel_id");
            PlayerPrefs.SetInt("SkinNotificationSent",1);
        }
        
        
        
        private void MoneyNotification()
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = "Money_channel_id",
                Name = "money Channel",
                Importance = Importance.Default,
                Description = "money notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            
            var notification = new AndroidNotification();
            notification.Title = "Zombie Sniper Survival";
            notification.Text = "You found a box full of money. Get your reward NOW!";
            notification.SmallIcon = "app_icon_small";
            notification.LargeIcon = "money_icon_small";

            notification.FireTime = System.DateTime.Now.AddHours(12);

            var id = AndroidNotificationCenter.SendNotification(notification, "Money_channel_id");
            
            var notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(id);
            
            if (notificationStatus == NotificationStatus.Scheduled)
            {
                // Replace the scheduled notification with a new notification.
                AndroidNotificationCenter.UpdateScheduledNotification(id, notification, "Money_channel_id");
            }
            else if (notificationStatus == NotificationStatus.Delivered)
            {
                // Remove the previously shown notification from the status bar.
                AndroidNotificationCenter.CancelNotification(id);
                AndroidNotificationCenter.SendNotification(notification, "Money_channel_id");
            }
            else if (notificationStatus == NotificationStatus.Unknown)
            {
                AndroidNotificationCenter.SendNotification(notification, "Money_channel_id");
            }
        }
        
        private void CompetitionNotification()
        {
            
            var channel = new AndroidNotificationChannel()
            {
                Id = "Competition_channel_id",
                Name = "Competition Channel",
                Importance = Importance.High,
                Description = "Competition notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            
            var notification = new AndroidNotification();
            notification.Title = "Zombie Sniper Survival";
            notification.Text = "Headshot competition has started! Try it now!";
            notification.SmallIcon = "app_icon_small";
            notification.LargeIcon = "zombie_icon_large";

            notification.FireTime = System.DateTime.Now.AddHours(28);

            var id =AndroidNotificationCenter.SendNotification(notification, "Competition_channel_id");
            var notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(id);
            
            if (notificationStatus == NotificationStatus.Scheduled)
            {
                // Replace the scheduled notification with a new notification.
                AndroidNotificationCenter.UpdateScheduledNotification(id, notification, "Competition_channel_id");
            }
            else if (notificationStatus == NotificationStatus.Delivered)
            {
                // Remove the previously shown notification from the status bar.
                AndroidNotificationCenter.CancelNotification(id);
                AndroidNotificationCenter.SendNotification(notification, "Competition_channel_id");
            }
            else if (notificationStatus == NotificationStatus.Unknown)
            {
                AndroidNotificationCenter.SendNotification(notification, "Competition_channel_id");
            }
        }
        

    }
}

#endif 
