using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Grid   board;

    void Start() { Camera.main.transform.position = new Vector3(board.width / 1.5f, board.height / 4, -10); }

    void Update() {
        var camPos = Camera.main.transform.position;
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0){
            Camera.main.transform.position = new Vector3(camPos.x, camPos.y + 1, -10);
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0){
            Camera.main.transform.position = new Vector3(camPos.x, camPos.y - 1, -10);
        }
    }
}
