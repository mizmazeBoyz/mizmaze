using UnityEngine;
using System.Collections;

public class MovableObject : InstantiationObject{

	public bool xDirection;
	public int size;


	public MovableObject(int size, bool xDirection){
		this.size = size;
		this.xDirection = xDirection;
	}
	
}
