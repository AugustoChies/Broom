using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallCatcher : MonoBehaviour
{
    public bool active;
    public GameObject imgend;
    float timer;


    // Use this for initialization
    void Start()
    {
        timer = 0;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += 1 * Time.deltaTime;
            if (timer > 5)
            {
                SceneManager.LoadScene("menu");
            }
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Destroy(other.gameObject);
            active = true;
            imgend.GetComponent<Image>().enabled = true;
        }
    }
}
