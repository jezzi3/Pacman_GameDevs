﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /**
        0 – Empty (do not instantiate anything)
        1 - Outside corner (double lined corener piece in orginal game)
        2 - Outside wall (double line in original game)
        3 - Inside corner (single lined corener piece in orginal game)
        4 - Inside wall (single line in orginal game)
        5 - Standard pellet (see Visual Assets above)
        6 - Power pellet (see Visual Assets above)
        7 - A t junction piece for connecting with adjoining regions
    **/
    public GameObject[] mapSprite;
    GameObject newSprite;   // Temp container for newly instantiated sprites
    List<GameObject> spriteList = new List<GameObject>();   // List containing the instantiated sprite in the top left quadrant from levelMap
    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    // Start is called before the first frame update
    void Start()
    {
        instantiateSprite();
          
        Debug.Log("gameobject total count: " + gameObject.transform.childCount);
        Debug.Log("spriteList count: " + spriteList.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void instantiateSprite()
    {
        
        for (int y = 0; y < levelMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)
            {
                topLeftGenerator(levelMap[y, x], x, y);
            }
        }
        topRightGenerator();
        bottomLeftGenerator();
        bottomRightGenerator();
    }

    // Generates the top left quadrant using levelMap[,]
    void topLeftGenerator(int spriteNo, int x, int y)
    {
        if (spriteNo != 0)
        {
            newSprite = Instantiate(mapSprite[spriteNo], new Vector2(x, -y), Quaternion.identity);
            newSprite.transform.parent = gameObject.transform;
            switch (spriteNo)
            {
                case 1:
                    outerCorner(ref newSprite, spriteNo, x, y);
                    break;
                case 2:
                    outerWall(ref newSprite, spriteNo, x, y);
                    break;
                case 3:
                    innerCorner(ref newSprite, spriteNo, x, y);
                    break;
                case 4:
                    innerWall(ref newSprite, spriteNo, x, y);
                    break;
                default:
                    break;
            }
            spriteList.Add(newSprite);
        }
    }

    // Generates the top right quadrant by iterating spriteList[]
    void topRightGenerator()
    {
        // Iterate through spriteList and mirror vertically to the top right
        foreach (GameObject go in spriteList)
        {
            newSprite = Instantiate(go, new Vector2(27 - go.transform.position.x, go.transform.position.y), go.transform.rotation);
            newSprite.transform.parent = gameObject.transform;
            if (newSprite.transform.eulerAngles.z == 0f || newSprite.transform.eulerAngles.z == 180f)
                newSprite.GetComponent<SpriteRenderer>().flipX = true;
            else
                newSprite.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    // Generates the bottom left quadrant by iterating spriteList[]
    void bottomLeftGenerator()
    {
        foreach (GameObject go in spriteList)
        {
            newSprite = Instantiate(go, new Vector2(go.transform.position.x, -28 - go.transform.position.y), go.transform.rotation);
            newSprite.transform.parent = gameObject.transform;
            if (newSprite.transform.eulerAngles.z == 0f || newSprite.transform.eulerAngles.z == 180f)
                newSprite.GetComponent<SpriteRenderer>().flipY = true;
            else
                newSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Generates the bottom right quadrant by iterating spriteList[]
    void bottomRightGenerator()
    {
        foreach (GameObject go in spriteList)
        {
            newSprite = Instantiate(go, new Vector2(27 - go.transform.position.x, -28 - go.transform.position.y), go.transform.rotation);
            newSprite.transform.parent = gameObject.transform;
            newSprite.GetComponent<SpriteRenderer>().flipY = true;
            newSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void outerCorner(ref GameObject corner, int cornerSpriteNo, int x, int y)
    {
        int wallSpriteNo = cornerSpriteNo + 1;

        // Rotating corner by checking neighbouring walls/corners
        // Top Wall and right wall
        if ((y != 0 && levelMap[y - 1, x] == wallSpriteNo) && levelMap[y, x + 1] == wallSpriteNo)
            corner.transform.eulerAngles = new Vector3(0, 0, 90f);
        // Left Wall and bottom wall
        else if ((x != 0 && levelMap[y, x - 1] == wallSpriteNo) && (levelMap.GetLength(0) > y + 1 && levelMap[y + 1, x] == wallSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, -90f);
        // Top wall and left wall
        else if ((y != 0 && levelMap[y - 1, x] == wallSpriteNo) && (x != 0 && levelMap[y, x - 1] == wallSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, 180f);
    }

    void outerWall(ref GameObject wall, int wallSpriteNo, int x, int y)
    {
        if ((x != 0 && levelMap[y, x - 1] == wallSpriteNo) || (levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == wallSpriteNo))
            wall.transform.eulerAngles = new Vector3(0, 0, 90f);
    }

    void innerCorner(ref GameObject corner, int cornerSpriteNo, int x, int y)
    {
        int wallSpriteNo = cornerSpriteNo + 1;

        // Rotating corner by checking neighbouring walls/corners
        // Top corner, left/right/bottom wall
        if (levelMap[y - 1, x] == cornerSpriteNo && levelMap[y, x - 1] == wallSpriteNo &&
                (levelMap.GetLength(0) > y + 1 && levelMap[y + 1, x] == wallSpriteNo) &&
                (levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == wallSpriteNo))
            return;
        // Left wall and bottom wall
        else if (levelMap[y, x - 1] == wallSpriteNo && levelMap[y + 1, x] == wallSpriteNo)
            corner.transform.eulerAngles = new Vector3(0, 0, -90f);
        // Top wall and right wall
        else if (levelMap[y - 1, x] == wallSpriteNo && (levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == wallSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, 90f);
        // Top wall and left wall
        else if (levelMap[y - 1, x] == wallSpriteNo && levelMap[y, x - 1] == wallSpriteNo)
            corner.transform.eulerAngles = new Vector3(0, 0, 180f);
        // Top corner and right wall
        else if (levelMap[y - 1, x] == cornerSpriteNo && (levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == wallSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, 90f);
        // Left wall and bottom corner
        else if (levelMap[y, x - 1] == wallSpriteNo && (levelMap.GetLength(0) > y + 1 && levelMap[y + 1, x] == cornerSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, -90f);
        // Top corner and left wall
        else if (levelMap[y - 1, x] == cornerSpriteNo && levelMap[y, x - 1] == wallSpriteNo)
            corner.transform.eulerAngles = new Vector3(0, 0, 180f);
        // Top wall and right corner
        else if (levelMap[y - 1, x] == wallSpriteNo && (levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == cornerSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, 90f);
        // Left corner and bottom wall
        else if (levelMap[y, x - 1] == cornerSpriteNo && (levelMap.GetLength(0) > y + 1 && levelMap[y + 1, x] == wallSpriteNo))
            corner.transform.eulerAngles = new Vector3(0, 0, -90f);
        // Top wall and left corner
        else if (levelMap[y - 1, x] == wallSpriteNo && levelMap[y, x - 1] == cornerSpriteNo)
            corner.transform.eulerAngles = new Vector3(0, 0, 180f);
        // Top wall and right out of bounds
        else if (levelMap[y - 1, x] == wallSpriteNo && levelMap.GetLength(1) <= x + 1)
            corner.transform.eulerAngles = new Vector3(0, 0, 90f);

    }

    void innerWall(ref GameObject wall, int wallSpriteNo, int x, int y)
    {
        int cornerSpriteNo = wallSpriteNo - 1;

        // Inner wall sprite will rotate if it meets the following conditions
        /**
            1. if top and bottom piece of (x,y) are not wall sprites
            2. if top or bottom piece of (x,y) is not a corner sprite
            3. if 1 & 2 are satifies, the wall will rotate if left or right is a corner/wall sprite
        **/
        if (!(levelMap[y - 1, x] == wallSpriteNo && (levelMap.GetLength(0) > y + 1 && levelMap[y + 1, x] == wallSpriteNo)) &&
            !(levelMap[y - 1, x] == cornerSpriteNo || (levelMap.GetLength(0) > y + 1 && levelMap[y + 1, x] == cornerSpriteNo)) &&
            ((levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == cornerSpriteNo) || levelMap[y, x - 1] == cornerSpriteNo ||
            (levelMap.GetLength(1) > x + 1 && levelMap[y, x + 1] == wallSpriteNo) || levelMap[y, x - 1] == wallSpriteNo))

            wall.transform.eulerAngles = new Vector3(0, 0, -90f);

        // 4. Special Case: when left side is a wall and right is out of bounds
        if (levelMap[y, x - 1] == wallSpriteNo && levelMap.GetLength(1) <= x + 1)
            wall.transform.eulerAngles = new Vector3(0, 0, -90f);

    }
}
