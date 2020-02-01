using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public ClickSystem clickSystem;
    public UIPauseManager pauseManager;
    private CameraMovement cam;

    void Start() {
        cam = GetComponent<CameraMovement>();
    }

    public void move(InputAction.CallbackContext context)
    {
        cam.move(context.ReadValue<Vector2>());
    }

    public void rotate(InputAction.CallbackContext context)
    {
        cam.rotate(context.ReadValue<float>());
    }

    public void zoom(InputAction.CallbackContext context)
    {
        cam.zoomCamera(context.ReadValue<Vector2>().y);
    }

    public void cycleCamera(InputAction.CallbackContext context)
    {
        cam.cycleCamera(context.ReadValue<float>());
    }

    public void pauseGame(InputAction.CallbackContext context)
    {
        pauseManager.pauseGame();
    }
    public void look(InputAction.CallbackContext context)
    {
        ClickSystem.updateHover(context.ReadValue<Vector2>());
    }

    public void select(InputAction.CallbackContext context)
    {
        
    }
}
