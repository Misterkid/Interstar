using System.IO;
using UnityEngine;
using System;

public class LogWriter
{
	private StreamWriter sw = null;

	public LogWriter(string file)
	{
		string path = Path.GetDirectoryName(file);
		if (!Directory.Exists(path)) 
			Directory.CreateDirectory(path);

		sw = new StreamWriter(file);
	}
	public void Write(string message)
	{
		sw.WriteLine(message);
		sw.Flush();
	}

	public void Close()
	{
		sw.Close();
	}

} 