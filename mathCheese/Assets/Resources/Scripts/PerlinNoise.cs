using UnityEngine;
using System.Collections;

public static class PerlinNoise {

	public enum NormalizeMode {Local, Global};

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, float lacunarity, float persistence, int octaves, Vector2 offset, NormalizeMode normalizeMode) {
		float[,] noiseMap = new float[mapWidth,mapHeight];

		System.Random prng = new System.Random(seed);
		float maxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;

		Vector2[] octaveOffsets = new Vector2[octaves];
		for(int i = 0; i < octaves; i++){
			float xOffset = prng.Next(-100000, 100000) + offset.x;
			float yOffset = prng.Next(-100000, 100000) - offset.y;
			octaveOffsets[i] = new Vector2(xOffset, yOffset);

			maxPossibleHeight += amplitude;
			amplitude *= persistence;
		}

		if (scale <= 0) {
			scale = 0.0001f;
		}

		float maxLocalNoiseHeight = float.MinValue;
		float minLocalNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth/2;
		float halfHeight = mapHeight/2;

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				amplitude = 1;
				frequency = 1;
				float noiseHeight = 0;

				for(int i = 0; i < octaves; i++){
					float sampleX = (x - halfWidth + octaveOffsets[i].x) / scale * frequency;
					float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistence;
					frequency *= lacunarity;
				}

				if(noiseHeight > maxLocalNoiseHeight){
					maxLocalNoiseHeight = noiseHeight;
				} else if(noiseHeight < minLocalNoiseHeight){
					minLocalNoiseHeight = noiseHeight;
				}

				noiseMap[x,y] = noiseHeight;
			}
		}

		for(int y = 0; y < mapHeight; y++){
			for(int x = 0; x < mapWidth; x++){
				if(normalizeMode == NormalizeMode.Local){
					noiseMap[x,y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x,y]);
				} else {
					float normalizeHeight = (noiseMap[x, y] + 1) / (2f * maxPossibleHeight / 2f);
					noiseMap[x, y] = Mathf.Clamp(normalizeHeight, 0, int.MaxValue);
				}
			}
		}

		return noiseMap;
	}

}