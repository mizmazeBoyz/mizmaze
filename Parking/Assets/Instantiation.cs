using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instantiation : MonoBehaviour {

	private Wall wall = new Wall();
	private List<GameObject> objects = new List<GameObject>();
	private bool[,] gameMatrix;
	private Movable selected;
	private int levelSizeX;
	private int levelSizeY;

	// Use this for initialization
	void Start () {
		InstantiationObject[,] instantiationMatrix = new LevelGenerator ().generateLevelInstantiationMatrix (wall);
		levelSizeX = instantiationMatrix.GetLength (0);
		levelSizeY = instantiationMatrix.GetLength (1);
		print (levelSizeX);

		// generate scene
		gameMatrix = new bool[levelSizeX, levelSizeY];
		for (int y=0; y<levelSizeY; y++) {
			for (int x=0; x<levelSizeX; x++) {
				InstantiationObject instObject = instantiationMatrix [x, y];
				if (instObject != null) {
					if (instObject == wall) {
						gameMatrix [x, y] = true;
						GameObject somewall = (GameObject)Instantiate (Resources.Load ("wall"));
						somewall.transform.position = new Vector3 (x, 0, y);
						objects.Add (somewall);
					} else {
						MovableObject movable = (MovableObject)instObject;
						for (int s = 0; s < movable.size; s++) {
							if (movable.xDirection) {
								gameMatrix [x - s, y] = true;
							} else {
								gameMatrix [x, y - s] = true;
							}
						}
						GameObject someobject;
						switch (movable.size) {
						case 2:	
							someobject = (GameObject)Instantiate (Resources.Load ("cube2x"));
							break;
						case 3: 
							someobject = (GameObject)Instantiate (Resources.Load ("cube3x"));
							break;
						default:
							throw new UnassignedReferenceException ();
						}
						someobject.transform.position = new Vector3 (x, 0, y);

						if (!movable.xDirection) {
							someobject.transform.Rotate (0, -90, 0);
							print (someobject.transform);
							someobject.transform.GetChild (0).Translate (0, 0, 1);
							//someobject.GetComponent("container").transform.Translate(0,0,1);
						}
						Movable mov = someobject.GetComponentInChildren<Movable> ();
						mov.xDirection = movable.xDirection;
						mov.positionX = x;
						mov.positionY = y;
						mov.size = movable.size;
					}
				}
			}
		}
	}

	public bool[,] getGameMatrix(){
		return gameMatrix;
	}

	public int[] selectMovable(int size, bool xDirection, int positionX, int positionY){
		int i = 0;
		while(gameMatrix[(positionX+(xDirection?(i+1):0)),(positionY+(xDirection?0:(i+1)))] == false){
			i++;
		}
		int j = 0;
		while(gameMatrix[(positionX-(xDirection?size+j:0)),positionY-(xDirection?0:(size+j))] == false){
			j++;
		}
		updateMatrixOnSelect(size, xDirection, positionX, positionY);
		int position = xDirection ? positionX : positionY;
		return new int[]{position+i,position-j};
	}

	private void updateMatrixOnSelect(int size, bool xDirection, int positionX, int positionY){
		updateMatrix (false, size, xDirection, positionX, positionY);
	}

	public void updateMatrixOnRelease(int size, bool xDirection, int positionX, int positionY){
		updateMatrix (true, size, xDirection, positionX, positionY);
	}

	private void updateMatrix(bool isSelected, int size, bool xDirection, int positionX, int positionY){
		
		for (int i = 0; i<size; i++) {
			if (xDirection) {
				gameMatrix [positionX - i, positionY] = isSelected;
			} else {
				gameMatrix [positionX , positionY-i] = isSelected;
			}
		}
	}
	/*
	public void printMatrix(){
		string bla = "";
		for(int y=0;y<8;y++){
			for(int x=0;x<8;x++){
				bla+=gameMatrix[x,y];
				bla+=",";
			}
			bla+="/";
		}
		print (bla);

	}
	*/
	// Update is called once per frame
	void Update () {
	
	}
}
