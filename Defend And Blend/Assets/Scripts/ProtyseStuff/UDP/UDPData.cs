public class UDPData {
	public string timeStamp;
	public float xRaw;
	public float yRaw;

	public UDPData(string timeStamp, float xRaw, float yRaw)
	{
		this.timeStamp = timeStamp;
		this.xRaw = xRaw;
		this.yRaw = yRaw;
	}

	public float XCalibrated {
		get { return xRaw; }
	}

	public float yCalibrated {
		get { return yRaw; }
	}
}