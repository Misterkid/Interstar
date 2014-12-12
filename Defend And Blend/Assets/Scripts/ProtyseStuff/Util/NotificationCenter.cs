using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NotificationCenter : MonoBehaviour
{

    // This class keeps track of the Observers and notification event of a given notification
    private class NotificationNode
    {
        public string Name { get; set; }
        public Action<Notification> NotifyEvents { get; set; }
    }

    private static NotificationCenter defaultCenter;
    private static bool appQuiting;

    private static NotificationCenter DefaultCenter
    {
        get
        {
            if (appQuiting)
                return null;
            if (!defaultCenter)
            {
                GameObject notificationObject = new GameObject("Default Notification Center");
                notificationObject.AddComponent<NotificationCenter>();
            }
            return defaultCenter;
        }
    }

    private void Awake()
    {
        if (!defaultCenter)
        {
            DontDestroyOnLoad(gameObject);
            defaultCenter = this;
            return;
        }
        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        appQuiting = true;
    }

    // Our hashtable containing all the notification nodes. Each notification node in the hash table has an ArrayList that contains all the observers and a notification event.
    Hashtable notifications = new Hashtable();

    public static void AddObserver(string name, Action<Notification> notifyEvent)
    {
        if (DefaultCenter == null)
            return;
        DefaultCenter.AddObserverToCenter(name, notifyEvent);
    }

	public static void RemoveObserver(string name, Action<Notification> notifyEvent)
	{
		if (DefaultCenter == null)
			return;
		
		DefaultCenter.RemoveObserverFromCenter(name, notifyEvent);
	}

	// PostNotification sends a notification object to all objects that have requested to receive this type of notification.
	// A notification can either be posted with a notification object or by just sending the individual components
	public static void PostNotification(string notificationName, object notificationSender, Hashtable aData)
	{
		Notification notification = new Notification(notificationSender, notificationName, aData);
		if (DefaultCenter == null)
			return;
		DefaultCenter.PostNotificationToCenter(notification);
	}
	
    private void AddObserverToCenter(string name, Action<Notification> notifyEvent)
    {
        // If the name isn't good, then throw an error and return.
        if (string.IsNullOrEmpty(name))
            return;
        
        // If this specific notification doesn't exist yet, then create it.
        if (notifications[name] == null)
            notifications[name] = new NotificationNode() { Name = name };

        NotificationNode notificationNode = notifications[name] as NotificationNode;

        if (notifyEvent != null)
            notificationNode.NotifyEvents += notifyEvent;
    }

    private void RemoveObserverFromCenter(string name, Action<Notification> notifyEvent = null)
    {
        NotificationNode notificationNode = notifications[name] as NotificationNode;

        // Assuming that this is a valid notification type, unassigned the notifyEvent or/and remove the observer from the list
        // If the list of observers is now empty, then remove that notification type from the notifications hash. This is for housekeeping purposes.
        if (notificationNode != null && notifyEvent != null)
        	notificationNode.NotifyEvents -= notifyEvent;
    }

    private void PostNotificationToCenter(Notification notification)
    {
        if (string.IsNullOrEmpty(notification.Name))
            return;

        // Obtain the notification node, and make sure that it is valid as well
        NotificationNode notificationNode = notifications[notification.Name] as NotificationNode;
        if (notificationNode == null)
            return;

        // Call all the methods assigned to this notification node
        if (notificationNode.NotifyEvents != null)
            notificationNode.NotifyEvents(notification);
    }    
}



