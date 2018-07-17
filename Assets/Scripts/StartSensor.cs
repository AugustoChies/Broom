using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSensor : MonoBehaviour
{
    public bool start;
    public float timer;
    public GameObject arduin;
    // Use this for initialization
    void Start()
    {
        start = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (timer >= 3)
            {
                start = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.transform.Translate(0, -0.1f, 0);
                arduin.GetComponent<Arduino_script>().start = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "sensor")
        {
            timer += 1 * Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sensor")
        {
            timer = 0;
        }

    }
}
