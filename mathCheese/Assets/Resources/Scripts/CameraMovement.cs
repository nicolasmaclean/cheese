using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int speed, rotationSpeed, zoomSpeed, yMax, yMin, zoom;

    private Vector3 velocity, goalPosition;
    private Quaternion smoothRot, rot;
    private float smoothZoom;
    private bool free = true;

    void Start()
    {
        Camera.main.transform.position = new Vector3(0f, zoom, 0f);
        smoothZoom = zoom;
        rot = Quaternion.Euler(60f,0,0);
        smoothRot = rot;
    }

    public void move(Vector2 delta)
    {
        if(!free && UIPauseManager.paused) return;
        velocity += new Vector3(delta.x*speed*Time.deltaTime, 0, delta.y*speed*Time.deltaTime);
    }

    public void rotate(float delta)
    {
        if(!free && UIPauseManager.paused) return;
        float angle = rotationSpeed * Time.deltaTime;
        rot = Quaternion.AngleAxis(angle, new Vector3(0, delta, 0)) * rot;
    }

    public void zoomCamera(float delta)
    {
        if(!free && UIPauseManager.paused) return;
        zoom += (int) (delta * zoomSpeed * Time.deltaTime);

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
        if(free && !UIPauseManager.paused) {
            updateTransfrom();
        }
        else if(!UIPauseManager.paused)
            movingToColony();
    }

    public void moveToColony()
    {
        free = false;

        Player cur = TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>();
        Vector2 pos = cur.colonies[cur.currentColony].gridPosition;
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

    public void cycleCamera(float i)
    {
        if(!free && UIPauseManager.paused) return;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        Player cur = TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>();
        if(GeometryUtility.TestPlanesAABB(planes, cur.colonies[cur.currentColony].groundT.GetComponent<Renderer>().bounds)) // if the current colony is visible cycle to next
            TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().cycleColony(i > 0 ? 1 : -1);
        moveToColony();

    }
}
