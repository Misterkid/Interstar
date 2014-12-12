using UnityEngine;

public enum Side { Left, Right }

public class CalibrationSettings {
#if UNITY_EDITOR
	public static bool emgEnabled = false;
#else
	public static bool emgEnabled = true;
#endif

	public static bool inputFlipped = false;
	public static float minLeft = 0;
	public static float minRight = 0;

	public static float maxLeft = 1f;
	public static float maxRight = 1f;

	public static float delay = 0;

	public static float leftBottomDeadzone = 0f;
	public static float rightBottomDeadzone = 0f;

	public static float leftTopDeadzone = 0f;
	public static float rightTopDeadzone = 0f;

	public static void SaveSettings() {
		/*PlayerPrefs.SetFloat("min_left", minLeft);
		PlayerPrefs.SetFloat("min_right", minRight);
		PlayerPrefs.SetFloat("max_left", maxLeft);
		PlayerPrefs.SetFloat("max_right", maxRight);*/
		PlayerPrefs.SetFloat("left_bottom_deadzone", leftBottomDeadzone);
		PlayerPrefs.SetFloat("right_bottom_deadzone", rightBottomDeadzone);
		PlayerPrefs.SetFloat("left_top_deadzone", leftTopDeadzone);
		PlayerPrefs.SetFloat("right_top_deadzone", rightTopDeadzone);
		PlayerPrefs.SetFloat("delay", delay);
		PlayerPrefs.Save();
	}
	
	public static void LoadSettings() {
		/*minLeft = PlayerPrefs.GetFloat("min_left", minLeft);
		minRight = PlayerPrefs.GetFloat("min_right", minRight);
		maxLeft = PlayerPrefs.GetFloat("max_left", maxLeft);
		maxRight = PlayerPrefs.GetFloat("max_right", maxRight);*/
		leftBottomDeadzone = PlayerPrefs.GetFloat("left_bottom_deadzone", leftBottomDeadzone);
		rightBottomDeadzone = PlayerPrefs.GetFloat("right_bottom_deadzone", rightBottomDeadzone);
		leftTopDeadzone = PlayerPrefs.GetFloat("left_top_deadzone", leftTopDeadzone);
		rightTopDeadzone = PlayerPrefs.GetFloat("right_top_deadzone", rightTopDeadzone);
		delay = PlayerPrefs.GetFloat("delay", delay);
	}

	public static float GetNormalizedValue(Side side, float value) {
		float deadzoneBottom = (side == Side.Left) ? leftBottomDeadzone : rightBottomDeadzone;
		float deadzoneTop = (side == Side.Left) ? leftTopDeadzone : rightTopDeadzone;
		float min = (side == Side.Left) ? minLeft : minRight;
		float max = (side == Side.Left) ? maxLeft : maxRight;
		float range = max - min;
		float newMin = min + (range * deadzoneBottom);
		float newMax = max - (range * deadzoneTop);

		if (value < newMin)
			return 0;

		float newRange = newMax - newMin;
		float newValue = value - newMin;
		return Mathf.Clamp01(newValue / newRange);
	}

	public static void Reset() {
		CalibrationSettings.minLeft = Mathf.Infinity;
		CalibrationSettings.maxLeft = -Mathf.Infinity;
		CalibrationSettings.minRight = Mathf.Infinity;
		CalibrationSettings.maxRight = -Mathf.Infinity;
	}

	public static void TraceDeadzone() {
		string traceString = string.Format("LeftBottom: {0} LeftTop: {1} RightBottom {2} RightTop {3}",
		              						leftBottomDeadzone, leftTopDeadzone, rightBottomDeadzone, rightTopDeadzone);
		Debug.Log(traceString);
	}
}