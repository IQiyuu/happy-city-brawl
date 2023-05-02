using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public int    width, height;
    [SerializeField] private Tile   tileSprite;
    private Dictionary<Vector2, Tile> grid;

    void Start() {
        grid = new Dictionary<Vector2, Tile>();
        generateGrid();
    }

    void    generateGrid() {
        for(float i = 0; i < width; i++) {
            for(float j = 0; j < height; j++) {
                var newTile = Instantiate(tileSprite, new Vector3(i / 2 + 0.25f, j / 2 + 0.25f, 0), Quaternion.identity);
                newTile.Init(((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0)));
                grid[new Vector2(i,j)] = newTile;
            }
        }
    }

    public Tile    getTile(Vector2 coords) {
        try {
            return grid[coords];
        } catch {
            return null;
        }
    }
}
