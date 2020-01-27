using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int speed, rotationSpeed, zoomSpeed, yMax, yMin, zoom;

    private Vector3 velocity, goalPosition;
    private Quaternion smoothRot, rot;
    private float smoothZoom;
    private bool free = true;

    void Start() {
        Camera.main.transform.position = new Vector3(0f, zoom, 0f);
        smoothZoom = zoom;
        rot = Quaternion.Euler(60f,0,0);
        smoothRot = rot;
    }

    public void move(int dir)
    {
        switch(dir) {
            case 0 : velocity += new Vector3(0, 0, speed * Time.deltaTime); break;
            case 1 : velocity -= new Vector3(0, 0, speed * Time.deltaTime); break;
            case 2 : velocity -= new Vector3(speed * Time.deltaTime, 0, 0); break;
            case 3 : velocity += new Vector3(speed * Time.deltaTime, 0, 0); break;
        }
    }

    public void rotate(int dir)
    {
        float angle;
        switch(dir) {
            case 0 : angle = rotationSpeed * Time.deltaTime;
                rot = Quaternion.AngleAxis(angle, Vector3.down) * rot;
                break;
            case 1 : angle = rotationSpeed * Time.deltaTime;
                rot = Quaternion.AngleAxis(angle, Vector3.up) * rot;
                break;
        }
    }

    public void zoomCamera()
    {
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
    
    void LateUpdate()
    {
        if(free) {
            updateTransfrom();
        }
        else
            movingToColony();
    }

    public void moveToColony()
    {
        free = false;

        System.Collections.Generic.List<Tile> colonies = TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().colonies;
        Vector2 pos = colonies[colonies.Count-1].gridPosition;
        goalPosition = new Vector3(TileMapGenerator.tileSize*pos.x, Camera.main.transform.position.y,TileMapGenerator.tileSize*pos.y);

        Vector3 ang = Camera.main.transform.rotation.eulerAngles;

        goalPosition.x -= Mathf.Sin(ang.y * Mathf.Deg2Rad) * (zoom * 6 / 10);
        goalPosition.z -= Mathf.Cos(ang.y * Mathf.Deg2Rad) * (zoom * 6 / 10);

        movingToColony();
    }

    private void movingToColony()
    {
        transform.position = Vector3.Lerp(transform.position, goalPosition, .1f);

        if((transform.position - goalPosition).magnitude <= .5f)
            free = true;
    }
}
