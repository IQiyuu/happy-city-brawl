using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Scoreboard	sb;
	[SerializeField] private Grid   board;
	[SerializeField] private Hand	hand;
	private int		happiness, population;

	void Start() { 
		Camera.main.transform.position = new Vector3(board.width / 1.5f - 2, board.height / 4 + 0.5f , -10);
		happiness = 0;
		population = 0;
		sb.refresh(happiness, population);
		hand.generate();
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
		population += n;
		sb.refresh(happiness, population);
	}
	public void	addHappiness(int n) { 
		happiness += n;
		sb.refresh(happiness, population);
	}

	public void	remPopulation(int n) { 
		population -= n; 
		sb.refresh(happiness, population);
	}

	public void	remHappiness(int n) { 
		happiness -= n;
		sb.refresh(happiness, population);
	}
}
