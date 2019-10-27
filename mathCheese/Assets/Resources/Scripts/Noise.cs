using UnityEngine;

public class Noise
{
    public static int[,] GenerateNoiseMap(int width, int height, int choiceAmt)
    {
        int[,] noiseMap = new int[height, width];

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
               noiseMap[y,x] = Random.Range(0, choiceAmt);
            }
        }

        return noiseMap;
    } 
}