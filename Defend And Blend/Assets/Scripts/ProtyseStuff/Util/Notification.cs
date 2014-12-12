/// <summary>
/// The Notification class is the object that is send to receiving objects of a notification type.
/// This class contains the sending GameObject, the name of the notification, and optionally a hashtable containing data.
/// </summary>
using System.Collections;

public class Notification
{
	private object sender;
	public object Sender { get { return sender; } }
	
	private string name;
	public string Name { get { return name; } }
	
	private Hashtable data;
	public Hashtable Data { get { return data; } }
	
	public Notification(object notificationSender, string notificationName)
	{
		sender = notificationSender;
		name = notificationName;
		data = null;
	}
	
	public Notification(object notificationSender, string notificationName, Hashtable aData)
	{
		sender = notificationSender;
		name = notificationName;
		data = aData;
	}
}