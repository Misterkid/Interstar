using UnityEngine;

public class MouseIdleController : MonoBehaviour {
	private const float idleTime = 3f;
	private float lastActivity;

	private void Update() {
		// reset activity if the mouse moved
		if (Util.MouseMoved())
			lastActivity = Time.time;

		// hide or show mouse cursor
		if (lastActivity + idleTime < Time.time)
			Screen.showCursor = false;
		else
			Screen.showCursor = true;
	}

	private void OnDestroy() {
		Screen.showCursor = true;
	}
}