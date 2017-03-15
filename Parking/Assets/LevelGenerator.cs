using UnityEngine;
using System.Collections;

public class LevelGenerator {

	public InstantiationObject[,] generateLevelInstantiationMatrix(Wall wall){

		InstantiationObject[,] instantiationMatrix = new InstantiationObject[8, 9];
		for (int i=0; i<8; i++) {
			instantiationMatrix [0, i] = wall;
			instantiationMatrix [7, i] = wall;
			instantiationMatrix [i, 0] = wall;
			instantiationMatrix [i, 7] = wall;
		}
		instantiationMatrix [2, 2] = new MovableObject (2, true);
		instantiationMatrix [4, 3] = new MovableObject (2, false);
		instantiationMatrix [3, 4] = new MovableObject (3, true);

		return instantiationMatrix;
	}

}
