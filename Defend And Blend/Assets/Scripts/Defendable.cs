using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//IMPLEMENTED HEALTHBAR TUTORIAL:
// REFERENCE BELOW!
// http://www.youtube.com/watch?v=NgftVg3idB4


public class Defendable : MonoBehaviour 
{
    public float health = 100;
    
    //Testing some shizzle
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;

    public int maxHealth = 100;
    private int currentHealth;

    private int CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value;
            HandleHealth();
        }
    }

    private float healthBarlenght;

    public Text healthText;
    public Image visualHealth;

    public float cooldown;
    private bool onCooldown;
    //End of Testing some hpbars.

	// Use this for initialization
	void Start () 
    {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        currentHealth = maxHealth;

        onCooldown = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    private void HandleHealth()
    {
        healthText.text = "Health: " + currentHealth;

        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);

        healthTransform.position = new Vector3(currentXValue, cachedY);

        if (currentHealth > maxHealth / 2) // I have more than 50% health.
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else // I have less than 50% health. 
        {
            visualHealth.color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }

    IEnumerator CooldownDMG()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }


    public void DoDamage(float pain)
    {
        if (!onCooldown && currentHealth > 0)
        {
            StartCoroutine(CooldownDMG());
            health -= pain;
            CurrentHealth -= 1;
        }
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
