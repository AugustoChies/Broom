using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorUpdate : MonoBehaviour {
    public GameObject eye;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = eye.transform.position;
        this.transform.rotation = eye.transform.rotation;
	}
}
