using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

<<<<<<< HEAD
=======
//this is a test
//balls i like

>>>>>>> origin/master
public class GameManager : MonoBehaviour {

	// Have to create list to hold all the tiles
	List<Tile> tiles;
	GameObject tileFolder;

	Tile [,]  tile_matrix;
	List<Tile> tile_row;

	string itemText = "You're Holding Nothing";


	void Start () {
		this.gameObject.tag = "Game Controller";
        //		GameObject steakObj = new GameObject ();
        //		Steak steak = steakObj.AddComponent<Steak>();
        //		steak.init (5, -4);
        this.name = "manager";

		tile_matrix = new Tile [16, 20];
		tiles = new List<Tile> ();
		tileFolder = new GameObject ();
		tileFolder.name = "Tiles";

		makeGrid ();

	}
	
	void Update () {
	}

	void makeGrid(){
		for (int y = 0; y < 16; y++) {
			for (int x = 1; x < 20; x++) {
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
	}
}
