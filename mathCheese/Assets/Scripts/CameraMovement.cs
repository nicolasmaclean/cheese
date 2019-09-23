using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float speed = 20f;
    public static float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        //speed = 20f;
        // rotationSpeed = 20f;

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
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot *= Quaternion.AngleAxis(angle, Vector3.down);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot *= Quaternion.AngleAxis(angle, Vector3.up);
        }

        Camera.main.transform.position = pos;
        Camera.main.transform.rotation = rot;
    }
}
