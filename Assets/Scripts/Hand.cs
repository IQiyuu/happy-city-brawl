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
            piecesList.Add(i, null);
            createPiece(i);
        }
    }

    public void replace(Building toRep) {
        delete(toRep);
        refill();
    }

    private void delete(Building toDel) {
        piecesList[toDel.handId] = null;
        toDel.handId = -1;
    }

    private void    createPiece(int i) {
        piecesList[i] = Instantiate(getRandomBuilding());
        piecesList[i].handId = i;
        var camPos = Camera.main.transform.position;
        piecesList[i].transform.position = new Vector3(camPos.x - 9, camPos.y - 4 + (2 * i), -2);
    }

    public void refill() {
        for(int i = 0; i < handSize; i++)
            try {
                if (piecesList[i] == null){
                    createPiece(i);}
            } catch {}
    }
}
