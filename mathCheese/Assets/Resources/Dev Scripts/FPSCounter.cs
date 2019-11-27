using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int FPS {get; set;}
    public int AverageFPS {get; set;}

    int[] fpsBuffer;
    int fpsBufferIndex;

    void initializeBuffer()
    {
        fpsBuffer = new int[60];
        fpsBufferIndex = 0;
    }

    void updateBuffer()
    {
        fpsBuffer[fpsBufferIndex++] = FPS;
        if(fpsBufferIndex >= 60)
            fpsBufferIndex = 0;
    }

    void calculateAverageFPS()
    {
        int sum = 0;
        foreach(int frame in fpsBuffer) {
            sum += frame;
        }
        AverageFPS = sum / 60;
    }

    void Update()
    {
        // FPS
        FPS = (int) (1f / Time.unscaledDeltaTime);

        // average FPS
        if(fpsBuffer == null)
            initializeBuffer();
        updateBuffer();
        calculateAverageFPS();
    }
}
