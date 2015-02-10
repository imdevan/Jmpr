//-----------------------------------------
//   SpawnObjects.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] danger;
    public float fireRate;
    public float spawnWidth;

    private float seconds;
	
	// Update is called once per frame
	void Update () 
    {
        // instantiates our danger objects
        //Spawn(fireRate);
	}

    void Spawn(float rate)
    {
        // timer, adds up the delta time for seconds
        seconds += Time.deltaTime;
        // if seconds great than our rate as defined in the inspector
        if (seconds > rate)
        {
			// random horizonal position
			float randomX = Random.Range(-spawnWidth, spawnWidth);
			float randomZ = Random.Range(-spawnWidth, spawnWidth);
            Vector3 position = new Vector3
                (
                randomX,
                transform.position.y,
				randomZ
                );

			// random horizontal rotation
			float randomY = Random.Range(0,360);
			Quaternion rotation = new Quaternion(
				0.0f,
				randomY,
				0.0f,
				0.0f);

            // randomly select an object in the danger array
            int randomObj = Random.Range(0, danger.Length);
			Instantiate(danger[randomObj], position, rotation);

            // zero out the seconds variable
            seconds = 0;
        }
    }
}
