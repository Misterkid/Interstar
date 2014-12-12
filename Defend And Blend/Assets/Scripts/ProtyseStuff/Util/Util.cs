using UnityEngine;

public class Util {
	// network settings
	public static int udpPort = 29129;
	
	// game settings
	public static bool autoCloseHand = true;
    public static int numberOfObjectToSpawn = 25;
	public static int objectiveObjectsEasy = 15;
	public static int objectiveObjectsMedium = 20;
	public static int objectiveObjectsHard = 25;
    public static float timeBetweenSpawns = 4;
	public static float itemDropSpeed = -1f;
	public static float itemInterceptDropSpeed = -2f;
	public static float grabberSensitivity = 2.5f;
	public static float catcherSensitivity = 0.5f;

	public static float itemHardBreakForce = 1.50f;
	public static float itemMediumBreakForce = 1.0f;
	public static float itemWeakBreakForce = 0.50f;
	public static float handShakeTolerance = 1.5f;
	public static float handBreakTolerance = 1.75f;


	// game events
    public const string EventGameSceneLoaded = "EventGameSceneLoaded";
    public const string EventStartGame = "EventStartGame";
    public const string EventObjectSpawned = "EventObjectSpawned";
    public const string EventObjectCaught = "EventObjectCaught";
    public const string EventObjectDropped = "EventObjectDropped";
	public const string EventObjectDestroyed = "EventObjectDestroyed";
    public const string EventObjectSpawnerEmpty = "EventObjectSpawnerEmpty";
	public const string EventOnGameOver = "EventOnGameOver";

	// gui events
    public const string EventOnFadeToBlackCompleted = "EventOnFadeToBlackCompleted";
    public const string EventOnFadeFromBlackCompleted = "EventOnFadeFromBlackCompleted";
	public const string EventShowStartMenu = "EventShowStartMenu";
	public const string EventShowCalibration = "EventShowCalibration";
	public const string EventShowOptions = "EventShowOptions";

	public static void SaveSettings() {
		PlayerPrefs.SetInt("udp_port", udpPort);
		PlayerPrefs.SetFloat("item_speed_intercept", itemInterceptDropSpeed);
		PlayerPrefs.SetFloat("item_speed", itemDropSpeed);
		PlayerPrefs.SetFloat("grabber_speed", grabberSensitivity);
		PlayerPrefs.SetFloat("catcher_speed", catcherSensitivity);

		PlayerPrefs.SetFloat("item_hard_breakforce", itemHardBreakForce);
		PlayerPrefs.SetFloat("item_medium_breakforce", itemMediumBreakForce);
		PlayerPrefs.SetFloat("item_easy_breakforce", itemWeakBreakForce);
		PlayerPrefs.SetFloat("hand_shake_tolerance", handShakeTolerance);
		PlayerPrefs.SetFloat("hand_break_tolerance", handBreakTolerance);
	}

	public static void LoadSettings() {
		udpPort = PlayerPrefs.GetInt("udp_port", 29129);
		itemInterceptDropSpeed = PlayerPrefs.GetFloat("item_speed_intercept", -2f);
		itemDropSpeed = PlayerPrefs.GetFloat("item_speed", -1f);
		grabberSensitivity = PlayerPrefs.GetFloat("grabber_speed", 2.5f);
		catcherSensitivity = PlayerPrefs.GetFloat("catcher_speed", 0.5f);

		itemHardBreakForce = PlayerPrefs.GetFloat("item_hard_breakforce", 1.50f);
		itemMediumBreakForce = PlayerPrefs.GetFloat("item_medium_breakforce", 1.0f);
		itemWeakBreakForce = PlayerPrefs.GetFloat("item_easy_breakforce", 0.50f);
		handShakeTolerance = PlayerPrefs.GetFloat("hand_shake_tolerance", 1.5f);
		handBreakTolerance = PlayerPrefs.GetFloat("hand_break_tolerance", 1.75f);
	}

	public static float Sum(float[] inputs) {
		float result = 0;
		for(int i = 0; i < inputs.Length; i++)
			result += inputs[i];
		return result;
	}
		
	public static float Average(float[] inputs) {
		float sum = Sum(inputs);
		return sum / inputs.Length;
	}

	public static int GetObjective(int difficulty) {
		switch (difficulty) {
			case 1: return objectiveObjectsMedium;
			case 2: return objectiveObjectsHard;
			default: return objectiveObjectsEasy;
		}
	}

	public static float GetItemDropspeed (int difficulty) {
		switch (difficulty) {
		case 1: return itemDropSpeed - 0.5f;
		case 2: return itemDropSpeed - 1f;
		default: return itemDropSpeed;
		}
	}

	public static float GetInterceptItemDropspeed (int difficulty) {
		switch (difficulty) {
		case 1: return itemInterceptDropSpeed - 0.5f;
		case 2: return itemInterceptDropSpeed - 1f;
		default: return itemInterceptDropSpeed;
		}
	}
    /*
	public static bool IsFinalItem(Item item) {
		int finalId = numberOfObjectToSpawn - 1;
		return (item.ItemId == finalId);
	}
    */
	public static bool MouseMoved() {
		if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0)
			return true;
		else if (Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse Y") > 0)
			return true;
		return false;
	}
}
