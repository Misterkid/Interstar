using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    private float damage;
    private Defendable target;
	// Update is called once per frame
	void Update () 
    {
	    if(target != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);//Position to shoot at
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, 10 * Time.deltaTime);

            if (gameObject.transform.position == targetPosition)
            {
                target.DoDamage(damage);
                Destroy(this.gameObject);
            }
        }
	}
    public void Shoot(float bulletDamage, Defendable targetObject)
    {
        damage = bulletDamage;
        target = targetObject;
    }
}
