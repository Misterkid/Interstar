using UnityEngine;
using System.Collections;

public class PlayerShip : Ship {

	// Use this for initialization
    public Vector3 cameraDistance = Vector3.zero;//Distance from this object and the camera. For the camera movement.
    private float squeezePressure = 0;
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void FixedUpdate()
    {
        Movement();//Move 
    }
    void Movement()
    {
        //Get X and Y axis!
        float x = (Input.GetAxis("Horizontal") * speed) * Time.deltaTime;
        float y = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;
        //Whole squeeze power again.
        //squeezePressure = 0;

        squeezePressure = (speed * Input.GetAxis("RTrigger"));//Get the trigger / pressure value.
        /*
        squeezePressure = (speed / 2) - squeezePressure;//convert to speed/2 minus the pressure making it (-speed/2) and (speed/2)
        squeezePressure *= -1;//Flip it!
        */
        //squeezePressure = squeezePressure * Time.deltaTime;
        transform.Translate(x, y, squeezePressure * Time.deltaTime);

        CameraMovement();//Move the damn Camera
        CameraBounds();//Have x and y boundries in the camera.
    }
    void CameraMovement()
    {
        GameObject camera = Camera.main.gameObject;
        //camera.transform.position = Vector3.MoveTowards(camera.transform.position, this.transform.position + cameraDistance, 2 * Time.deltaTime);
        float distance = Mathf.Abs(camera.transform.position.z - transform.position.z);
        //camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z + (cameraDistance.z - squeezePressure)), (speed ) * Time.deltaTime);
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z + cameraDistance.z), (speed) * Time.deltaTime);
        //Debug.Log(distance);
        /*
        if (distance > cameraDistance.z + 5 || distance > cameraDistance.z - 5)
        {
            //Debug.Log("?");/
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z + cameraDistance.z), (speed) * Time.deltaTime);
        }
        else
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z + cameraDistance.z), (speed / 3) * Time.deltaTime);
        }*/
         
    }
    void CameraBounds()
    {
        //float distance = Mathf.Abs(cameraDistance.z - transform.position.z);
        float distance = Mathf.Abs(cameraDistance.z);
        //Debug.Log(distance);
        //X
        if(transform.position.x > distance || transform.position.x < -distance)
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(/*transform.position.x -*/ distance, transform.position.y, transform.position.z); 
            }
            else
            {
                transform.position = new Vector3(/*transform.position.x +*/ -distance, transform.position.y, transform.position.z); 
            }
        }
        //Y
        if (transform.position.y > distance || transform.position.y < -distance)
        {
            if (transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x , /*transform.position.y -*/ distance, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, /*transform.position.y +*/  -distance, transform.position.z);
            }
        }
    }
}
