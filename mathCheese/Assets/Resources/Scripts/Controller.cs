using UnityEngine;

public class Controller : MonoBehaviour
{
    public ClickSystem clickSystem;
    public UIPauseManager pauseManager;
    private CameraMovement cam;

    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // mouse input is inside of mouseover
    void FixedUpdate() {
        //moves the camera
        if(Input.GetAxisRaw("Vertical") != 0)
            cam.move(Input.GetAxis("Vertical") > 0 ? 0 : 1);
        if(Input.GetAxisRaw("Horizontal") != 0)
            cam.move(Input.GetAxis("Horizontal") < 0 ? 2 : 3);

        //rotates the camera
        if(Input.GetKey(KeyCode.Q)) // use right horizontal for rotation and make an axis for q and e keys
            cam.rotate(0);
        if(Input.GetKey(KeyCode.E))
            cam.rotate(1);

        //zooms the camera
        if(Input.GetAxisRaw("RightVertical") != 0) // might switch to use right/left bumpers through scroll wheel virtual axis
            cam.zoomCamera(Input.GetAxis("RightVerical"));
        else if(Input.GetAxis("Mouse ScrollWheel") != 0)
            cam.zoomCamera(Input.GetAxis("Mouse ScrollWheel"));

        //keyboard input
        if(Input.GetKeyDown(KeyCode.P))
            clickSystem.addUnit();
        if(Input.GetKeyDown(KeyCode.B))
            clickSystem.buildColony();
        if(Input.GetKeyDown(KeyCode.Escape))
            pauseManager.pauseGame();
    }
}
