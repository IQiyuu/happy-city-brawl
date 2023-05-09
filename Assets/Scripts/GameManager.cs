using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Scoreboard	sb;
	[SerializeField] private Grid   board;
	[SerializeField] public Hand	hand;
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
			hand.move(new Vector3(0, 1, 0));
		}
		else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0){
			Camera.main.transform.position = new Vector3(camPos.x, camPos.y - 1, -10);
			hand.move(new Vector3(0, -1, 0));
		}
		sb.refresh(calculScore());
	}

	public Vector2	calculScore() {
		int	pop = 0, hap = 0;
		List<Building> closed;
		for (int x = 0; x < board.width; x++) {
			for (int y = 0; y < board.height; y++) {
				var build = board.getTile(new Vector2(x, y)).getContent();
				if (build != null && !closed.Contains(build)) {
					pop += build.population;
					hap += build.happiness;
					closed.Add(closed);
				}
			}
		}
		return (new Vector2(pop, hap));
	}

	public void	addPopulation(int n) {
		population += n;
	}
	public void	addHappiness(int n) { 
		happiness += n;
		sb.refresh(happiness, population);
	}

	public void	remPopulation(int n) { 
		population -= n; 
	}

	public void	remHappiness(int n) { 
		happiness -= n;
	}

	public Grid	getBoard() { return board; }
}
