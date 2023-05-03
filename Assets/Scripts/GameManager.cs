using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Grid   board;
	[SerializeField] private Scoreboard	sb;
	private int		happiness, population;

	void Start() { 
		Camera.main.transform.position = new Vector3(board.width / 1.5f - 2, board.height / 4 + 0.5f , -10);
		happiness = 0;
		population = 0;
		sb.refresh(0, 0);
		print("gm " + happiness);
	}

	void Update() {
		var camPos = Camera.main.transform.position;
		if(Input.GetAxisRaw("Mouse ScrollWheel") > 0){
			Camera.main.transform.position = new Vector3(camPos.x, camPos.y + 1, -10);
		}
		else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0){
			Camera.main.transform.position = new Vector3(camPos.x, camPos.y - 1, -10);
		}
	}

	public void	addPopulation(int n) {
		print("gm " + happiness);
		population += n;
		sb.refresh(population, happiness);
	}
	public void	addHappiness(int n) { 
		happiness += n;
		sb.refresh(population, happiness);
	}

	public void	remPopulation(int n) { 
		population -= n; 
		sb.refresh(population, happiness);
	}
	public void	remHappiness(int n) { 
		happiness -= n; 
		sb.refresh(population, happiness);
	}
}
