using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class GameManager : MonoBehaviour {


	// Have to create list to hold all the tiles
	List<Tile> tiles;
	GameObject tileFolder;

	// Matrix
	public Tile [,]  tile_matrix;
	List<Tile> tile_row;

	// Player
	GameObject playerObject;
	Character player;

    // Getting all guards
    GameObject[] guards;

    // Scent
    public GameObject scent_colliders;
    public int scent_number = 1;

	float clock = 0f;
	float turn_duration = 0.5f;

	string itemText = "You're Holding Nothing";
    string loseText = "";

    public bool paused = false;

	void Start () {
		// Setting up the manager
		this.gameObject.tag = "Game Controller";
        this.name = "manager";

        // Creating our grid and matrix
		tile_matrix = new Tile [16, 21];
		tiles = new List<Tile> ();
		tileFolder = new GameObject ();
		tileFolder.name = "Tiles";

		// Obtaining the player. 
		playerObject = GameObject.Find ("skeleton");
		player = playerObject.GetComponent<Character> ();

        // This gets all current active guards on the scene (prior to pressing play).
        guards = GameObject.FindGameObjectsWithTag("Dog");

        scent_colliders = new GameObject();
        scent_colliders.name = "Scent";

        makeGrid ();
        //Pause();                    //PAUSING
	}
	// TODO:
        // All of the guard's AI
        // Collision with objects.
        // Tile textures and behaviors. 

	void Update () {
        
        // The player controls this. It'll unlock after 1 unit of traversal has been complete.
        if (player.turnLock == false)
        {
            // Wait for player input, when it happens, initiate 1 'Turn' in the game world.
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)
               || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                // We only want this to happen once.
                paused = false;                                     //UNPAUSING
                player.Move();
                
                foreach (GameObject go in guards)
                {
                    go.GetComponent<Dog>().Move();
                }
            }
          
        }
	}

	void makeGrid(){
		for (int y = 0; y < 16; y++) {
			for (int x = 0; x <= 20; x++) {
				addTile (x, y);
			}
		}
	}

	void addTile(int x, int y){
		GameObject tileobject = new GameObject ();
		Tile tile = tileobject.AddComponent<Tile> ();

		tile.transform.parent = tileFolder.transform;
		tile.transform.position = new Vector3 (x, 0, y);

		tile.init (x, y, this);
		tile_matrix [y,x] = tile; 

		tile.name = "Tile " + tiles.Count;
		tiles.Add (tile);
	}
		
	void UpdateGUI(string item){
		itemText = "You're Holding " + item; 
	}

	void OnGUI()
	{
		GUI.Label (new Rect (100, 70, 100, 50), itemText);
        GUI.Label(new Rect(200, 70, 100, 50), loseText);
	}

    void gameOver()
    {
        //Time.timeScale = 0;
        loseText = "Game Over =(";
        StartCoroutine(reloadLevel());
    }
    IEnumerator reloadLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause(){
		Time.timeScale = 0;
	}

	public void unPause(){
		Time.timeScale = 1;
	}
}
