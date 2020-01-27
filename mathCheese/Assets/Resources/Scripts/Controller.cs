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
    void LateUpdate() {
        //moves the camera
        if(Input.GetKey(KeyCode.W))
            cam.move(0);
        if(Input.GetKey(KeyCode.S))
            cam.move(1);
        if(Input.GetKey(KeyCode.A))
            cam.move(2);
        if(Input.GetKey(KeyCode.D))
            cam.move(3);

        //rotates the camera
        if(Input.GetKey(KeyCode.Q))
            cam.rotate(0);
        if(Input.GetKey(KeyCode.E))
            cam.rotate(1);

        //zooms the camera
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
            cam.zoomCamera();

        //keyboard input
        if(Input.GetKeyDown(KeyCode.P))
            clickSystem.addUnit();
        if(Input.GetKeyDown(KeyCode.B))
            clickSystem.buildColony();
        if(Input.GetKeyDown(KeyCode.Escape))
            pauseManager.pauseGame();
    }
}
