using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanManager : MonoBehaviour
{
    public AudioClip moveAudio;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject pacman;
    private Tweener tweener;
    float[,] movementArray =
    {
        {6f, -1f},
        {6f, -5f},
        {1f, -5f},
        {1f, -1f}
    };
    int moveNo = 0;


    // Start is called before the first frame update
    void Start()
    {
        tweener = gameObject.GetComponent<Tweener>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveAudio;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Starting from 
        // top left     (1, -1) 
        // top right    (6, -1)
        // bottom right (6, -5)
        // bottom left  (1, -5)
        if (tweener.isNull())
        {
            tweener.AddTween(pacman.transform, pacman.transform.position, new Vector3(movementArray[moveNo, 0], movementArray[moveNo, 1], 0f), 1f);

            switch(moveNo)
            {
                case 0:
                    moveNo++;
                    pacman.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    break;
                case 1:
                    moveNo++;
                    pacman.transform.eulerAngles = new Vector3(0f, 0f, 270f);
                    break;
                case 2:
                    moveNo++;
                    pacman.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    break;
                case 3:
                    moveNo = 0;
                    pacman.transform.eulerAngles = new Vector3(0f, 180f, 90f);
                    break;
                default:
                    break;
            }
        }
        
    }

}
