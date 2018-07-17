using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPathFollow : MonoBehaviour {
    public Transform[] path;
    public GameObject test,vassoura;
    public int atual = 0;
    public int speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position != path[atual].position)
        {
            Vector3 move = new Vector3 (path[atual].position.x - this.transform.position.x,
                                         path[atual].position.y - this.transform.position.y,
                                        path[atual].position.z - this.transform.position.z);
            float veclength = Mathf.Sqrt(move.x * move.x + move.y * move.y + move.z * move.z);
            move.x = move.x / veclength;
            move.y = move.y / veclength;
            move.z = move.z / veclength;

            this.transform.Translate(move * speed * Time.deltaTime);
        }

        float dist = Vector3.Distance(this.transform.position, vassoura.transform.position);
        if(dist < 5)
        {
            speed = 40;
        }
        else if (dist < 15)
        {
            speed = 30;
        }
        else if(dist < 30)
        {
            speed = 20;
        }
        else
        {
            speed = 10;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boost")
        {
            atual = (atual + 1) % path.Length;
        }


    }
}
