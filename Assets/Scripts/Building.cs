using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    [SerializeField] private int    width, height, baseHappiness, basePopulation;
    private bool    taken = false;
    private bool    placed = false;
    [SerializeField] private bool    move_again;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip placement;
    [SerializeField] GameManager gm;
    private int happiness,population;

    void    Start() {
        Init();
    }

    void    Init() {
        happiness = baseHappiness;
        population = basePopulation;
    }

    void    OnMouseDown() {
        if ((move_again && placed)) removePiece();
        if (placed) return ;
        taken = !taken;
        if (!taken)
            placePiece();
    }

    void Update() {
        if (placed || (!placed && !taken)) return ;
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
        placed = true;
        print(startingX + " " + startingY);
        source.PlayOneShot(placement, 1);
        transform.position = new Vector3(startingX + ((float)(width) / 4), startingY - ((float)(height) / 4), -1);
        gm.addPopulation(population);
        gm.addHappiness(happiness);
    }

    void removePiece() {
        float valeurX = (float)transform.position.x - (((float)width) / 4);
        float startingX = (float)Math.Round(valeurX / 5, 1);
        float valeurY = (float)transform.position.y + (((float)height) / 4);
        float startingY = (float)Math.Round(valeurY / 5, 1);
        startingX = startingX * 5;
        startingY = startingY * 5;
        for(float i = 0; i < width; i++) {
            for (float j = 0; j < height; j++) {
                var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 - j - 1));
                tile.setContent(null);
            }
        }
        placed = false;
        taken = false;
        gm.remPopulation(population);
        gm.remHappiness(happiness);
    }

    public int  getPopulation() { return population; }

    public int  getHappiness() { return happiness; }

    public void addHappiness(int n) { happiness += n; }

    public void addPopulation(int n) { population += n; }

    public void resetHapiness(int n) { happiness = baseHappiness; }

    public void resetPopulation(int n) { population = basePopulation; }
}
