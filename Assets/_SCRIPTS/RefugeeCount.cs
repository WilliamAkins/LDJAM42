using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefugeeCount : MonoBehaviour {

    public Sprite[] seatSprites = new Sprite[3];

    public GameObject seat = null;

    private int seatNum = 0;
    private int filledSeats = 0;

    private GameObject[] emptySeats;

    private ResourceList resourceList;

    // Use this for initialization
    void Start () {
        resourceList = GameObject.FindGameObjectWithTag("boat").GetComponent<ResourceList>();
        seatNum = resourceList.getMaxAmountOfRefugees();
        filledSeats = seatNum - resourceList.getCurrentRemainingSpace();

        emptySeats = new GameObject[seatNum];

        //Debug.Log(seatNum + " " + filledSeats);

        float spriteX = -50.0f;
        float spriteY = -336.5f;

        for (int i = 0; i < seatNum; i++)
        {
            //create the object and set it as a child of a parent
            emptySeats[i] = Instantiate(seat, Vector3.zero, Quaternion.identity) as GameObject;
            emptySeats[i].transform.parent = GameObject.Find("UI/UI_boatSeats").transform;

            //get the rect transform and scale and position the sprite
            RectTransform seatSpriteRect = emptySeats[i].GetComponent<RectTransform>();
            seatSpriteRect.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            seatSpriteRect.localPosition = new Vector3(spriteX, spriteY, 0.0f);

            //set the sprite image for the sprite
            Image seatSprite = emptySeats[i].GetComponent<Image>();

            int spriteID = 0;
            if (i < filledSeats)
                spriteID = Random.Range(1, 3);
            else
                spriteID = 0;

            seatSprite.sprite = seatSprites[spriteID];

            //move the sprite positions
            spriteX *= -1;
            if (spriteX < 0)
                spriteY += 50.0f;
        }
    }

    // Update is called once per frame
    void Update () {}

    public void updateSeatSprites()
    {
        seatNum = resourceList.getMaxAmountOfRefugees();
        filledSeats = seatNum - resourceList.getCurrentRemainingSpace();

        for (int i = 0; i < seatNum; i++)
        {
            //set the sprite image for the sprite
            Image seatSprite = emptySeats[i].GetComponent<Image>();

            if (seatSprite.sprite.name == "ui_seat_empty")
            {
                int spriteID = 0;
                if (i < filledSeats)
                    spriteID = Random.Range(1, 3);
                else
                    spriteID = 0;

                seatSprite.sprite = seatSprites[spriteID];
            }
        }
    }

    public void resetSeatSprites()
    {
        seatNum = resourceList.getMaxAmountOfRefugees();

        for (int i = 0; i < seatNum; i++)
        {
            //set the sprite image for the sprite
            Image seatSprite = emptySeats[i].GetComponent<Image>();

            seatSprite.sprite = seatSprites[0];
        }
    }
}
