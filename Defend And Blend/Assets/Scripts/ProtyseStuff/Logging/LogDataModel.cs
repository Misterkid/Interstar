using System.Collections.Generic;
using System.Text;
using System.Collections;

public class LogDataModel
{
	private Dictionary<string, string> lastFrameValues;
	private string[] headers;
	private LogWriter log;

	public LogDataModel(LogWriter log, string[] headers) {
		this.log = log;
		lastFrameValues = new Dictionary<string, string>();
		this.headers = headers;

		LogHeaders();
	}

	private void LogHeaders() {
		// Loop through all string values and log them as headers
		StringBuilder builder = new StringBuilder();
		foreach (string header in headers)
			builder.AppendFormat("{0}\t", header);
		log.Write(builder.ToString());
	}

	public void SetValue(string header, string value) {
		// set current frame value for the desired column
		lastFrameValues[header] = value;
	}

	public void LogValues(string notificationName, Hashtable data) {
		// Notify all listeners that we want the current value for all values that will be logged
		NotificationCenter.PostNotification(notificationName, this, data);
		
		// build the current frame string and write it to the log file
		StringBuilder builder = new StringBuilder();
		foreach (string header in headers)
		{
			string value = string.Empty;
			bool valueFound = lastFrameValues.TryGetValue(header, out value);
			builder.AppendFormat("{0}\t", (valueFound) ? value : "undefined");
		}
		log.Write(builder.ToString());
	}
}