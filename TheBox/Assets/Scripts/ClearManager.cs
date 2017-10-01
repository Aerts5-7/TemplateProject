using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour {

	public GameObject ButtonBackTitle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushButtonBackTitle(){
		SceneManager.LoadScene ("TitleScene");
	}

}
