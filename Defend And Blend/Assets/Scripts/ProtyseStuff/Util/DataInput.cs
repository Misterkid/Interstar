using UnityEngine;

public class DataInput {
	public float Timestamp;
	public float Left;
	public float Right;

	public DataInput(float timestamp, float left, float right) {
		this.Timestamp = timestamp;
		this.Left = left;
		this.Right = right;
	}

	public bool IsExpired() {
		if (Time.time >= Timestamp + CalibrationSettings.delay)
			return true;
		else 
			return false;
	}
}