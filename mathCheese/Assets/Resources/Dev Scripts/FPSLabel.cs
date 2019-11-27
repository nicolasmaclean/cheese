using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (FPSCounter))]
public class FPSLabel : MonoBehaviour
{
    public Text fpsLabel;
    public Text averagefpsLabel;
    FPSCounter fpsCounter;

    void Awake() {
        fpsCounter = GetComponent<FPSCounter>();
    }

    void Update()
    {
        fpsLabel.text = Mathf.Clamp(fpsCounter.FPS, 0, 99).ToString();
        averagefpsLabel.text = Mathf.Clamp(fpsCounter.AverageFPS, 0, 99).ToString();
    }
}
