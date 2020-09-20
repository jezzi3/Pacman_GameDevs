using System.Collections;
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
    GameObject newSprite;

    //Level Array
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
        int x, y;        
        for(y = 0; y < levelMap.GetLength(0); y++)
        {
            for(x = 0; x < levelMap.GetLength(1); x++)
            {
                instantiateSprite(levelMap[y,x], x, y);
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void instantiateSprite(int sprite, int x, int y)
    {
        switch(sprite)
        {   
            // 1 and 3 are corners, 2 and 4 are walls
            case 1:
            case 3:
                newSprite = Instantiate(mapSprite[sprite], new Vector2(x, -y), Quaternion.identity);
                newSprite.transform.parent = gameObject.transform;
                break;
            case 2:
            case 4:
                newSprite = Instantiate(mapSprite[sprite], new Vector2(x, -y), Quaternion.identity);
                newSprite.transform.parent = gameObject.transform;
                break;
            case 5:
            case 6:
                newSprite = Instantiate(mapSprite[sprite], new Vector2(x, -y), Quaternion.identity);
                newSprite.transform.parent = gameObject.transform;
                break;
            default:
                break;
        }
    }

    /** Instantiates top left section of the level
    void topLeftGenerator(int sprite, int x, int y)
    {
        switch(sprite)
        {
            case 1:
            case 2:
                newSprite = Instantiate(mapSprite[sprite], new Vector2(x, -y), Quaternion.identity);
                newSprite.transform.parent = gameObject.transform;
                break;
            case 5:
            case 6:
                newSprite = Instantiate(mapSprite[sprite], new Vector2(x, -y), Quaternion.identity);
                newSprite.transform.parent = gameObject.transform;
                break;
            default:
                break;
        }
    }
    // Instantiates top right section of the level
    void topRightGenerator(int sprite, int x, int y)
    {}

    // Instantiates bottom left section of the level
    void bottomLeftGenerator(int sprite, int x, int y)
    {}

    // Instantiates bottom right section of the level
    void bottomRightGenerator(int sprite, int x, int y)
    {}**/
}
