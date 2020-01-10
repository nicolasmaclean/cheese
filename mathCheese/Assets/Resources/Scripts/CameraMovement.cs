using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int speed, rotationSpeed, zoomSpeed, yMax, yMin, zoom;

    private Vector3 velocity;
    private Quaternion smoothRot, rot;
    private float smoothZoom;


    void Start() {
        Camera.main.transform.position = new Vector3(0f, zoom, 0f);
        smoothZoom = zoom;
        rot = Quaternion.Euler(60f,0,0);
        smoothRot = rot;
    }

    void handleInput()
    {
        //moves camera
        if(Input.GetKey(KeyCode.W)) {
            velocity += new Vector3(0, 0, speed * Time.deltaTime);

        } if(Input.GetKey(KeyCode.S)) {
            velocity -= new Vector3(0, 0, speed * Time.deltaTime);

        } if(Input.GetKey(KeyCode.A)) {
            velocity -= new Vector3(speed * Time.deltaTime, 0, 0);

        } if(Input.GetKey(KeyCode.D)) {
            velocity += new Vector3(speed * Time.deltaTime, 0, 0);
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
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
            zoom += (int) (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);

        if(zoom > yMax)
            zoom = yMax;
        else if(zoom < yMin)
           zoom = yMin;
    }

    void updateTransfrom()
    {
        //lerps for smooth movements
        velocity = Vector3.Lerp(velocity, Vector3.zero, .1f);
        smoothRot = Quaternion.Lerp(smoothRot, rot, .1f);
        smoothZoom = Mathf.Lerp(smoothZoom, zoom, .1f);

        //assinments
        Vector3 tempPos = Camera.main.transform.position;
        Camera.main.transform.Translate(velocity);
        tempPos = new Vector3(Camera.main.transform.position.x, smoothZoom, Camera.main.transform.position.z);
        Camera.main.transform.position = tempPos;

        Camera.main.transform.rotation = smoothRot;
    }
    
    void FixedUpdate()
    {
        handleInput();
        updateTransfrom();
    }
}
