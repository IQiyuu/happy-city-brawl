using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    [SerializeField] private int    width, height;
    private bool    taken = false;
    private bool    placed = false;
    [SerializeField] private bool    move_again;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip placement;
    [SerializeField] GameManager gm;
    [SerializeField] private GameObject  road;
    [SerializeField] BuildingType   type;
    [SerializeField] Bonus bonus;
    public int baseHappiness, basePopulation, happiness, population;
    public int  handId = 0;

    void    Start() {
        Init();
    }

    void    Init() {
        happiness = baseHappiness;
        population = basePopulation;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    List<Building> nearbyBuildings(int x, int y, int width, int height) {
        List<Building> nearby = new List<Building>();
        Grid board = gm.getBoard();

        for(int i = x - 1; i < x + width; i++) {
            if (i >= 0) {
                for(int j = y - height - 1; j < y; j++) {
                    if (j >= 0) {
                        var tile = board.getTile(new Vector2(i, j)).getContent();
			    	    if (tile != null) {
			    	    	var build = tile.GetComponent<Building>();
			    	    	if (!nearby.Contains(build) && build != this)
			    	    		nearby.Add(build);
			    	    }
                    }
                }
            }
        }
        //print(nearby[0].bonus.search(type) + " " + nearby[0].bonus.values[nearby[0].bonus.search(type)].y);
        return nearby;
    }
/*
    bool    check_road(float startingX, float startingY) {  Probleme avec le check pour savoir si la route est posable / la tile qui est set est pas la bonne a chaque fois 
        switch (transform.eulerAngles.z) {
            case 90:
                for (int j = 0; j < height; j++) { // 0-1.5 = 0-3 -> 0-
                    var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + width, startingY * 2 - j - 1));
                    if (tile != null) {
                        if (tile.getContent() != null && tile.getContent().tag != "Road") {
                            print("ROADCHECK it is NOT an empty place, there is an " + tile.getContent().name + " at (" + (startingX * 2 + width) + "," + (startingY * 2 - j - 1) + ")");
                            return false;
                        }
                    } else {
                        print("Not in grid(" + (startingX + width) + "," + (startingY - j) + ")");
                        return false;
                    }
                }
                for (float j = 0; j < height; j++) {
                    print(" at (" + (startingX * 2 + width) + "," + (startingY * 2 - j - 1) + ")");
                    var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + width, startingY * 2 - j - 1));
                    var newRoad = Instantiate(road, new Vector3((float)(startingX + width / 2 + 0.25), (float)(startingY - j / 2 - 0.25), -1), Quaternion.identity);
                    tile.setContent(road);
                }
                break;
            case 180:
                for (int i = 0; i < width; i++) { // 0-1.5 = 0-3 -> 0-
                        var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 + height - 1));
                        if (tile != null) {
                            if (tile.getContent() != null && tile.getContent().tag != "Road") {
                                print("ROADCHECK it is NOT an empty place, there is an " + tile.getContent().name + " at (" + (startingX * 2 + i) + "," + (startingY * 2 + height - 1) + ")");
                                return false;
                            }
                        } else {
                            print("Not in grid(" + (startingX + i) + "," + (startingY - height) + ")");
                            return false;
                        }
                    }
                    for (float i = 0; i < width; i++) {
                        print(" at (" + (startingX * 2 + i) + "," + (startingY * 2 + height) + ")");
                        var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 + height));
                        var newRoad = Instantiate(road, new Vector3((float)(startingX + i / 2 + 0.25), (float)(startingY + height / 2 - 0.75), -1), Quaternion.identity);
                        tile.setContent(road);
                    }
                break;
            case 270:
                for (int j = 0; j < height; j++) { // 0-1.5 = 0-3 -> 0-
                        var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2, startingY * 2 - j - 1));
                        if (tile != null) {
                            if (tile.getContent() != null && tile.getContent().tag != "Road") {
                                print("ROADCHECK it is NOT an empty place, there is an " + tile.getContent().name + " at (" + (startingX * 2 - 1) + "," + (startingY * 2 - j - 1) + ")");
                                return false;
                            }
                        } else {
                            print("Not in grid(" + (startingX - 1) + "," + (startingY - j) + ")");
                            return false;
                        }
                    }
                    for (float j = 0; j < height ; j++) {
                        print(" at (" + (startingX * 2 + width) + "," + (startingY * 2 - j - 1) + ")");
                        var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2, startingY * 2 - j - 1));
                        var newRoad = Instantiate(road, new Vector3((float)(startingX - 0.25), (float)(startingY - j / 2 - 0.25), -1), Quaternion.identity);
                        tile.setContent(road);
                    }
                break;
            default:
                for (int i = 0; i < width; i++) { // 0-1.5 = 0-3 -> 0-
                        var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 - height - 1));
                        if (tile != null) {
                            if (tile.getContent() != null && tile.getContent().tag != "Road") {
                                print("ROADCHECK it is NOT an empty place, there is an " + tile.getContent().name + " at (" + (startingX * 2 + i) + "," + (startingY * 2 - height - 1) + ")");
                                return false;
                            }
                        } else {
                            print("Not in grid(" + (startingX + i) + "," + (startingY - height) + ")");
                            return false;
                        }
                    }
                    for (float i = 0; i < width; i++) {
                        print(" at (" + (startingX * 2 + i) + "," + (startingY * 2 - height + 1) + ")");
                        var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 - height - 1));
                        var newRoad = Instantiate(road, new Vector3((float)(startingX + i / 2 + 0.25), (float)(startingY - height / 2 - 0.25), -1), Quaternion.identity);
                        tile.setContent(newRoad);
                    }
                break;
        }
        return true;
    }*/

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
                    //print(tile.getContent() + " at (" + (startingX * 2 + i) + "," + (startingY * 2 - j - 1) + ")");
                    if (tile.getContent() != null) {
                        var camPos = Camera.main.transform.position;
                        print("it is NOT an empty place, there is an " + tile.getContent().name + " at (" + (startingX + i) + "," + (startingY - j) + ")");
                        transform.position = new Vector3(camPos.x - 9, camPos.y - 4 + (2 * handId), -2);
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return ;
                    }
                } else {
                    var camPos = Camera.main.transform.position;
                    print("Not in grid(" + (startingX + i) * 2 + "," + (startingY - j) * 2 + ")");
                    transform.position = new Vector3(camPos.x - 9, camPos.y - 4 + (2 * handId), -2);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    return ;
                }
            }
        }
       // if (!check_road(startingX, startingY))  return ;
        for(float i = 0; i < width; i++) {
            for (float j = 0; j < height; j++) {
                var tile = GameObject.Find("GridManager").GetComponent<Grid>().getTile(new Vector2(startingX * 2 + i, startingY * 2 - j - 1));
                tile.setContent(this.gameObject);
            }
        }
        placed = true;
        //print(startingX + " " + startingY);
        source.PlayOneShot(placement, 1);
        transform.position = new Vector3(startingX + ((float)(width) / 4), startingY - ((float)(height) / 4), -1);
        /* si c est un parc ajouter 1 pop A chaque maison autour
        si c est une maison ajouter 1 bonheur A chaque maison autour
        si c est une usine ajouter -1 bonheur POUR chaque batiment autour sauf usine
        si c est une ecole ajouter 1 bonheur AUX maisons a 3 cases autour
        si c est une epicerie gagne 1 bonheur POUR chaque maison autour
        */
        applyBonus(gm.getBoard(), (int)(startingX * 2), (int)(startingY * 2));
        gm.hand.replace(this);
    }

    void applyBonus(Grid board, int x, int y) {
        foreach(Building building in nearbyBuildings(x, y, width, height)){
            for(int i = 0; i < bonus.type.Count; i++) {
                if (bonus.type[i] == building.type) {
                    print(building.type);
                    building.happiness += (int)bonus.values[i].x;
                    building.population += (int)bonus.values[i].y;
                    happiness += (int)building.bonus.values[building.bonus.search(type)].x;
                    population += (int)building.bonus.values[building.bonus.search(type)].y;
                    break ;
                }
            }
        }
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
