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
        // WASD keys or left stick to move camera
        if(Input.GetAxisRaw("Vertical") != 0)
            cam.move(Input.GetAxis("Vertical") > 0 ? 0 : 1);
        if(Input.GetAxisRaw("Horizontal") != 0)
            cam.move(Input.GetAxis("Horizontal") < 0 ? 2 : 3);

        // QE keys or right stick horizontal movement to rotate
        if(Input.GetAxisRaw("RightHorizontal") != 0)
            cam.rotate(Input.GetAxisRaw("RightHorizontal"));

        // mouse scroll or right stick vertical movement to zoom
        if(Input.GetAxisRaw("RightVertical") != 0) // might switch to use right/left bumpers through scroll wheel virtual axis
            cam.zoomCamera(Input.GetAxis("RightVerical"));
        else if(Input.GetAxis("Mouse ScrollWheel") != 0)
            cam.zoomCamera(Input.GetAxis("Mouse ScrollWheel"));

        // escape key or start button to pause game
        if(Input.GetKeyDown(KeyCode.P))
            clickSystem.addUnit();
        if(Input.GetKeyDown(KeyCode.B))
            clickSystem.buildColony();
        if(Input.GetAxisRaw("Cancel") != 0) {
            pauseManager.pauseGame();
        }
    }
}
