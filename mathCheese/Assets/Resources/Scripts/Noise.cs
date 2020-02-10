using UnityEngine;

public class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, int choiceAmt)
    {
        float[,] noiseMap = new float[height, width];

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
               noiseMap[y,x] = Random.Range(0f, 1f);
            }
        }

        return noiseMap;
    } 
}