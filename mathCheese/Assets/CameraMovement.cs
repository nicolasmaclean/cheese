using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

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
        if(Input.GetKey(KeyCode.UpArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot *= Quaternion.AngleAxis(angle, Vector3.right);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot *= Quaternion.AngleAxis(angle, Vector3.left);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot *= Quaternion.AngleAxis(angle, Vector3.down);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            float angle = rotationSpeed * Time.deltaTime;
            rot *= Quaternion.AngleAxis(angle, Vector3.up);
        }

        transform.position = pos;
        transform.rotation = rot;
    }
}
