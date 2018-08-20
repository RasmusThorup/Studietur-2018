using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    public bool dontDestroyOnLoad;

	// Use this for initialization
	void Start () {
        if(dontDestroyOnLoad)
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
