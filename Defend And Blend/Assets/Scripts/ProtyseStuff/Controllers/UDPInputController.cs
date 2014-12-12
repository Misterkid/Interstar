using UnityEngine;
using System.Collections.Generic;

public class UDPInputController : Singleton<UDPInputController> {
	public const int StackSize = 30;

	public delegate void InputDelegate(float left, float right);
	public InputDelegate OnInput;

	private float lastInputLeft = 0;
	private float lastInputRight = 0;
	private bool receivedInput;

	private static Queue<DataInput> inputCache = new Queue<DataInput>();

	private void Start() {
		UDPDataReceiver.Instance.OnReceivedData += OnReceivedUPDData;
		UDPDataReceiver.Instance.OpenConnection();
	}

	protected override void OnDestroy() {
		UDPDataReceiver.Instance.OnReceivedData -= OnReceivedUPDData;
		UDPDataReceiver.Instance.CloseConnection();

		base.OnDestroy();
	}

	private void Update() {
		inputCache.Enqueue(new DataInput(Time.time, lastInputLeft, lastInputRight));

		if (inputCache.Count > 0)
			HandleCachedInputs();
	}

	private void OnReceivedUPDData(float inputLeft, float inputRight) {
		lastInputLeft = inputLeft;
		lastInputRight = inputRight;
	}
	
	private void HandleCachedInputs() {
		DataInput data = inputCache.Peek();
		do {
			if (data.IsExpired()) {
				PostOutput(data.Left, data.Right);
				inputCache.Dequeue();
				if (inputCache.Count == 0)
					return;
				
				data = inputCache.Peek();
			} else {
				data = null;
			}
		} while (data != null);
	}

	private void PostOutput(float left, float right) {
		if (OnInput != null && CalibrationSettings.emgEnabled) {
			if (CalibrationSettings.inputFlipped)
				OnInput(right, left);
			else
				OnInput(left, right);
		}
	}
}