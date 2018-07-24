using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {

    public int width;
    public int height;

    public GameObject[] wallLeft;
    public GameObject[] wallUp;
    public GameObject[] wallRight;
    public GameObject[] wallDown;
    public GameObject[] cornerUpRight;
    public GameObject[] cornerUpLeft;
    public GameObject[] cornerDownRight;
    public GameObject[] cornerDownLeft;
    public GameObject[] floor;
    public GameObject[] outerFloor;

    public Vector2 startingPos;
    public Vector2 startPosOuter;

    float spriteWidth;
    float spriteHeight;

	public void GenerateMap()
    {
        Sprite floorSprite = wallUp[0].GetComponent<SpriteRenderer>().sprite;
        spriteWidth = floorSprite.bounds.size.x;
        Vector2 posOuter = startPosOuter;
        Vector2 pos = startingPos;
        float startY = startingPos.y;
        float startYouter = startPosOuter.y;
        GameObject clone;
        for (int i = 0; i < width * 2; i++)
        {
            posOuter.y = startYouter;

            for (int j = 0; j < height * 2; j++)
            {
                clone = Instantiate(outerFloor[Random.Range(0, outerFloor.Length)], posOuter, Quaternion.identity);
                posOuter.y -= spriteWidth;
                clone.transform.parent = transform;
            }

            posOuter.x += spriteWidth;
        }

        for (int i = 0; i < width; i++)
        {
            pos.y = startY;
            for (int j = 0; j < height; j++)
            {
                if (j == 0 && i == 0)
                {
                    clone = Instantiate(cornerUpLeft[Random.Range(0,cornerUpLeft.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (j == height - 1 && i == 0)
                {
                    clone = Instantiate(cornerDownLeft[Random.Range(0, cornerDownLeft.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (j == 0 && i == width - 1)
                {
                    clone = Instantiate(cornerUpRight[Random.Range(0, cornerUpRight.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (j == height - 1 && i == width - 1)
                {
                    clone = Instantiate(cornerDownRight[Random.Range(0, cornerDownRight.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (i == 0)
                {
                    clone = Instantiate(wallLeft[Random.Range(0, wallLeft.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (j == 0)
                {
                    clone = Instantiate(wallUp[Random.Range(0, wallUp.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (i == width - 1)
                {
                    clone = Instantiate(wallRight[Random.Range(0, wallRight.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else if (j == height - 1)
                {
                    clone = Instantiate(wallDown[Random.Range(0, wallDown.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
                else
                {
                    clone = Instantiate(floor[Random.Range(0, floor.Length)], pos, Quaternion.identity);
                    pos.y -= spriteWidth;
                    clone.transform.parent = transform;
                }
            }
            pos.x += spriteWidth;
        }
    }
}
