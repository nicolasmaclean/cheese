using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float speed = 20f;
    public static float rotationSpeed = 20f;


    void Start() {
        Camera.main.transform.position = new Vector3(0f,40f,0f);
        Camera.main.transform.rotation = Quaternion.Euler(40f,0,0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        //Moving the Camera
        /*
        if(Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
        }
        if(Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if(Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if(Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }*/

        if(Input.GetKey("w"))
        {
            Camera.main.transform.position += Vector3.ProjectOnPlane(transform.forward * speed * Time.deltaTime, Vector3.up);
        }
        if(Input.GetKey("s"))
        {
            Camera.main.transform.position -= Vector3.ProjectOnPlane(transform.forward * speed * Time.deltaTime, Vector3.up);
        }
        if(Input.GetKey("a"))
        {
            Camera.main.transform.position -= transform.right * speed * Time.deltaTime;
        }
        if(Input.GetKey("d"))
        {
            Camera.main.transform.position += transform.right * speed * Time.deltaTime;
        }

        //Rotating the Camera
        if(Input.GetKey(KeyCode.LeftArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot = Quaternion.AngleAxis(angle, Vector3.down) * rot;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot = Quaternion.AngleAxis(angle, Vector3.up) * rot;
        }

        //Zooming the Camera
        Camera.main.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * speed * Time.deltaTime;

        //Camera.main.transform.position = pos;
        Camera.main.transform.rotation = rot;
    }
}
