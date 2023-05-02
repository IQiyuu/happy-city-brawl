using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    [SerializeField] int    width, height;
    private bool    taken = false;
    private bool    takable = true;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip placement;


    void    OnMouseDown() {
        if (!takable) return ;
        taken = !taken;
        if (!taken)
            placePiece();
    }

    void Update() {
        if (!takable || !taken) return ;
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, -2);
        if (Input.GetKeyDown("d")) {
            transform.Rotate(new Vector3(0, 0, 90));
            swap_size();
        }
        else if (Input.GetKeyDown("a")) {
            transform.Rotate(new Vector3(0, 0, -90));
            swap_size();
        }
    }

    void    swap_size() { 
        var tmp = height;
        height = width;
        width = tmp;
    }

    void    placePiece() {
        float valeurX = (float)transform.position.x - (((float)width) / 4);
        float startingX = (float)Math.Round(valeurX / 5, 1);
        float valeurY = (float)transform.position.y + (((float)height) / 4);
        float startingY = (float)Math.Round(valeurY / 5, 1);
        startingX = startingX * 5;
        startingY = startingY * 5;
         for(int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) { // 0-1.5 = 0-3 -> 0-
                print((startingX * 2 + i) + "-" + (startingY * 2 - j - 1));
                var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 - j - 1));
                if (tile != null) {
                    if (tile.getContent() != null) {
                        print("it is NOT an empty place, there is an " + tile.getContent().name + " at (" + (startingX + i) + "," + (startingY - j) + ")");
                        return ;
                    }
                } else {
                    print("Not in grid(" + (startingX + i) * 2 + "," + (startingY - j) * 2 + ")");
                    return ;
                }
            }
        }
        for(float i = 0; i < width; i++) {
            for (float j = 0; j < height; j++) {
                var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 - j - 1));
                tile.setContent(this.gameObject);
            }
        }
        takable = false;
        print(startingX + " " + startingY);
        source.PlayOneShot(placement, 1);
        transform.position = new Vector3(startingX + ((float)(width) / 4), startingY - ((float)(height) / 4), -1);
    }
}
