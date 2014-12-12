using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPDataReceiver
{
	public delegate void ReceivedDataDelegate(float input1, float input2);
	public ReceivedDataDelegate OnReceivedData;

	private static UDPDataReceiver instance;
	public static UDPDataReceiver Instance {
		get { return instance = instance ?? new UDPDataReceiver(); }
	}

	public float PackagesReceived{ get; private set;}

	private UdpClient listener;
	private IPEndPoint endPoint;
	private float[] lastData;
	private bool dataLoopEnabled = false;
	private bool listenerActive = false;

	public void CloseConnection() {
		if (listener != null)
			listener.Close();

		listener = null;
		endPoint = null;
		listenerActive = false;
		dataLoopEnabled = false;
	}

	public void OpenConnection() {
		if (listenerActive)
			return;

		endPoint = new IPEndPoint(IPAddress.Any, Util.udpPort);
		listener = new UdpClient( endPoint );
		listenerActive = true;

		if (dataLoopEnabled)
			return;
		dataLoopEnabled = true;
		BeginReceive();
	}

	public void EndReceiveLoop()
	{
		dataLoopEnabled = false;
	}
 
	private void BeginReceive()	{
		listener.BeginReceive(new AsyncCallback(ReceiveCallback), listener);
	}

	public float[] GetLastData() {
		return lastData;
	}

	private void ReceiveCallback(IAsyncResult ar) {	
		try {
			UdpClient u = (UdpClient)ar.AsyncState;
	 		IPEndPoint e = endPoint;
	
			byte[] byteData = u.EndReceive(ar, ref e);
			
			if (!listenerActive) { 
				return; 
			}
			
			if(byteData == null) {
				Debug.LogWarning("Empty byte-data received!");
				if(dataLoopEnabled)
					BeginReceive();
				return;
			}
			
			string stringData = Encoding.ASCII.GetString(byteData, 0, byteData.Length);
			if(string.IsNullOrEmpty(stringData)) {
				Debug.LogWarning("Empty string-data received!");
				if(dataLoopEnabled)
					BeginReceive();
				return;
			}

			stringData = stringData.Trim();
			string[] words =  stringData.Split(' ');
			List<string> cleanedWords = new List<string>();
			foreach (string s in words) {
				if(string.IsNullOrEmpty(s)){ continue; }
				cleanedWords.Add(s);
			}
			
			int length = cleanedWords.Count;
			float[] floats = new float[length];
			double d = 0;
			for(int i = 0; i < length; i++) {
				if(Double.TryParse(cleanedWords[i], out d)) {
					floats[i] = (float)d;
				} else  {
					Debug.LogWarning("Could not parse: " + cleanedWords[i]);
				}
			}
			
			lastData = floats;
			if (OnReceivedData != null) {
				if (length >= 2)
					if (OnReceivedData != null)
						OnReceivedData(floats[0], floats[1]);
			}
			PackagesReceived++;
			if (dataLoopEnabled)
				BeginReceive();
		}
		catch (SocketException e)
		{
			Debug.LogWarning("Socket is closed! " + e.ToString());
		}
		catch (ObjectDisposedException e)
		{
			Debug.LogWarning("Listener is disposed! " + e.ToString());
		}
		catch(Exception e)
		{
			Debug.LogError("Something went wrong!" + e.ToString());
		}
	}
}
