using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private static SpriteRenderer bubbleImage;
    private Sprite[] soldierSprite;
    private Sprite[] tankSprite;
    private Sprite[] planeSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        soldierSprite = Resources.LoadAll<Sprite>("soldier");
        tankSprite = Resources.LoadAll<Sprite>("tank");
        planeSprite = Resources.LoadAll<Sprite>("plane");
        GameObject obj = GameObject.Find("bubbleImage");
        bubbleImage = obj.GetComponent<SpriteRenderer>();
    }

    public void EnableBubble()
    {
        gameObject.SetActive(true);
    }

    public void DisableBubble()
    {
        gameObject.SetActive(false);
    }

    public void SetBubbleImage(string unit)
    {
        switch (unit)
        {
            case "soldier":
                bubbleImage.sprite = soldierSprite[0];
                break;
            case "tank":
                bubbleImage.sprite = tankSprite[0];
                break;
            case "plane":
                bubbleImage.sprite = planeSprite[0];
                break;
            default:
                Debug.Log("something wrong");
                break;
        }
    }

}
