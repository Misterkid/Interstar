using UnityEngine;
using System.Collections.Generic;

public class GamepadController : Singleton<GamepadController> {
	public delegate void InputDelegate(float left, float right);
	public InputDelegate OnInput;

	private Queue<DataInput> inputCache = new Queue<DataInput>();

	private void Update() {
		if (inputCache.Count > 0)
			HandleCachedInputs();

		float input = Input.GetAxis("Horizontal");
		float inputLeft = 0;
		float inputRight = 0;
		if (input > 0)
			inputRight = input;
		else if (input < 0)
			inputLeft = Mathf.Abs(input);

		inputCache.Enqueue(new DataInput(Time.time, inputLeft, inputRight));
	}

	private void HandleCachedInputs() {
		DataInput data = inputCache.Peek();
		do {
			if (data.IsExpired()) {
				if (OnInput != null && !CalibrationSettings.emgEnabled) {
					if (CalibrationSettings.inputFlipped) 
						OnInput(data.Right, data.Left);
					else
						OnInput(data.Left, data.Right);
				}
				inputCache.Dequeue();
				if (inputCache.Count == 0)
					return;

				data = inputCache.Peek();
			} else {
				data = null;
			}
		} while (data != null);
	}
}