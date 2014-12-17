using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Collections;

public class LogController : MonoBehaviour
{
	public const string EventRequestFrameData = "EventRequestFrameData";
	public const string EventRequestGameData = "EventRequestGameData";

	private bool loggingEnabled;

	private LogDataModel frameDataLogModel;
	private LogDataModel gameDataLogModel;

	private LogWriter frameFile;
	private LogWriter gameFile;

	private void LateUpdate() {
		// write last frame values
		if (loggingEnabled) {
			frameDataLogModel.LogValues(EventRequestFrameData, null);
		}
	}

	private string GetPath() {

		string userId = "User_" + "0000";/* GameStateController.UserID;*/
        string sessionId = "Session_" + "0001"; /*GameStateController.SessionID;*/

		string time = DateTime.Now.ToString("HH.mm.ss");

		// get personal folder for saving the log files
		string personalPath = string.Empty;
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			personalPath = Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData);
		else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			personalPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal) + "/Library/Application Support";
		else
			personalPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);

		return string.Format("{0}/Defend And Blend/Logs/{1}/{2}/{3}_",personalPath, userId, sessionId, time);
	}
/*
	public void LogGameData(Item item) {
		Hashtable table = new Hashtable();
		table["item"] = item;
		gameDataLogModel.LogValues(EventRequestGameData, table);
	}
*/
	public void StartLog() {
		string path = GetPath();
		// Setup data models for the logger
		frameFile = new LogWriter(path +"framedata.txt");
		gameFile = new LogWriter(path +"gamedata.txt");
		frameDataLogModel = new LogDataModel(frameFile, Enum.GetNames(typeof(LogFrameDataColumns)));
		gameDataLogModel = new LogDataModel(gameFile, Enum.GetNames(typeof(LogGameDataColumn)));

		// set logging file to enabled
		loggingEnabled = true;
	}

	public void StopLog() {
		if (!loggingEnabled)
			return;

		frameFile.Close();

		// stop current logger
		loggingEnabled = false;
		frameDataLogModel = null;
	}
}