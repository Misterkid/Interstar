using System.Collections.Generic;
using System.Text;

public class LogInfoModel
{
	private LogWriter log;
	private Dictionary<string, string> data;
	private string[] headers;

	public LogInfoModel(LogWriter log, string[] headers)
	{
		this.log = log;
		data = new Dictionary<string, string>();
		this.headers = headers;
	}

	public void SetValue(string header, string value)
	{
		// set current frame value for the desired column
		data[header] = value;
	}

	public void LogValues(string notificationName)
	{
		// Notify all listeners that we want the current value for all values that will be logged
		NotificationCenter.PostNotification(notificationName, this, null);

		foreach (string header in headers)
		{
			string value = string.Empty;
			bool valueFound = data.TryGetValue(header, out value);
			log.Write(string.Format("{0}: {1}", header, (valueFound) ? value : "undefined"));
		}
	}
}