using UnityEngine;
using System.Collections;
using System;

public class Movable : MonoBehaviour {
	public bool xDirection;
	public int positionX;
	public int positionY;
	public int size;

	private int max;
	private int min;

	private Instantiation game;

	private Vector3 screenPoint;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		game = GameObject.Find("game").GetComponent<Instantiation>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.parent.position);
		int[] space = game.selectMovable(size,xDirection,positionX,positionY);

		max = space [0];
		min = space [1];
		//offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
	}
	
	void OnMouseDrag()
	{
		Vector3 curScreenPoint;
		curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) ;
		// + offset;

		//transform.parent.position = xDirection? new Vector3 (curPosition.x , transform.parent.position.y, transform.parent.position.z): new Vector3 (transform.parent.position.x, transform.parent.position.y,curPosition.z);
		Vector3 pos = xDirection? new Vector3 (Math.Abs(curPosition.x-Math.Round (curPosition.x))>.25?curPosition.x:(float)Math.Round (curPosition.x) , transform.parent.position.y, transform.parent.position.z): new Vector3 (transform.parent.position.x, transform.parent.position.y,Math.Abs(curPosition.z-Math.Round (curPosition.z))>.25?curPosition.z:(float)Math.Round (curPosition.z));
		if (xDirection&&pos.x > max||!xDirection&&pos.z>max) {
			pos = xDirection?new Vector3(max,pos.y,pos.z):new Vector3(pos.x,pos.y,max);
		}
		if(xDirection&&pos.x<min||!xDirection&&pos.z<min){
			pos = xDirection?new Vector3(min,pos.y,pos.z):new Vector3(pos.x,pos.y,min);
		}
		transform.parent.position = pos;
	}

	void OnMouseUp() {
		positionX = (int)Math.Round (transform.parent.position.x);
		positionY = (int)Math.Round (transform.parent.position.z);
		transform.parent.position = new Vector3 ((float)positionX, (float)Math.Round (transform.parent.position.y), (float) positionY);
		game.updateMatrixOnRelease(size,xDirection,positionX,positionY);
	}
}
