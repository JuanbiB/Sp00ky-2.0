using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && (other.gameObject.GetComponent<Character>().hasBone == true))
        {
            
            if(SceneManager.GetActiveScene().name=="Level 1")
            {
                SceneManager.LoadScene(4);
            }
            if (SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene(5);
            }
            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                SceneManager.LoadScene(6);
            }
            //TODO
            //Add scene transitions for rest of levels

            //Debug.Log("You win!");

        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
