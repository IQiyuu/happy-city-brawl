using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=System.Random;

public class Hand : MonoBehaviour
{
    Dictionary<int, Building> piecesList = new Dictionary<int, Building>();
    [SerializeField] int handSize;
    [SerializeField] List<Building> buildings;
    Random rnd = new Random();

    public Building getRandomBuilding() {
        return (buildings[rnd.Next(buildings.Count)]);
    }

    public void generate() {
        for (int i = 0; i < handSize; i++) {
            piecesList.Add(i, Instantiate(getRandomBuilding()));
            piecesList[i].handId = i;
            piecesList[i].transform.position = new Vector3(-3, i - 3, -2);
        }
    }
}
