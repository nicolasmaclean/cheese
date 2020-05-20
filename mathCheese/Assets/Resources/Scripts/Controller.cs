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

        // QE keys or bumpers movement to rotate
        if(Input.GetAxisRaw("RightHorizontal") != 0)
            cam.rotate(Input.GetAxisRaw("RightHorizontal"));

        // mouse scroll or triggers vertical movement to zoom
        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0) // maybe switch it back to use right stick to zoom
            cam.zoomCamera(Input.GetAxis("Mouse ScrollWheel"));

        // escape key or start button to pause game
        //if(Input.GetKeyDown(KeyCode.P))
            //clickSystem.addUnit();
        if(Input.GetKeyDown(KeyCode.B))
            clickSystem.buildColony();
        if(Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("x"))
            Camera.main.GetComponent<CameraMovement>().cycleCamera(-1);
        if(Input.GetKeyDown("joystick button 5") || Input.GetKeyDown("c"))
            Camera.main.GetComponent<CameraMovement>().cycleCamera(1);
        if(Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("escape")) {
            pauseManager.pauseGame();
        }
    }
}
