using UnityEngine;

public class LogUnityInfo : MonoBehaviour {
	private void Awake() {
		NotificationCenter.AddObserver(LogController.EventRequestFrameData, OnRequestFrameData);
		NotificationCenter.AddObserver(LogController.EventRequestGameData, OnRequestGameData);
	}

	private void OnDestroy() {
		NotificationCenter.RemoveObserver(LogController.EventRequestFrameData, OnRequestFrameData);
		NotificationCenter.RemoveObserver(LogController.EventRequestGameData, OnRequestGameData);
	}


	private void OnRequestFrameData(Notification notification) {
		LogDataModel logModel = notification.Sender as LogDataModel;
		if (logModel != null) {
			logModel.SetValue(LogFrameDataColumns.CurrentFrame.ToString(), Time.frameCount.ToString());
			logModel.SetValue(LogFrameDataColumns.FrameTime.ToString(), Time.deltaTime.ToString("0.00"));
		}
	}

	private void OnRequestGameData(Notification notification) { 
		LogDataModel logModel = notification.Sender as LogDataModel;
		if (logModel != null) {
			logModel.SetValue(LogGameDataColumn.CurrentFrame.ToString(), Time.frameCount.ToString());
		}
	}
}