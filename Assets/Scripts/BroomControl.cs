using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BroomControl : MonoBehaviour {
    public bool mode; //0 é o treino , 1 o chase


    public int pontos;
    public float speed;
    public float extraspeed;
    //canvas
    public GameObject texto;
    public GameObject textospeed;
    public GameObject endtext;
    public GameObject disttext;
    //arduino
    public GameObject arduino;
    //vrstart
    public GameObject headsensor;
    public GameObject head;
    //broom model
    public GameObject nimbus;

    public GameObject bola;
    public GameObject eye;

    //collision sensors
    public GameObject front, left, right,up,down;


    int lateral;
    bool endtimer;
    float endcount;
    bool boosting;
    float boosttimer;
    public int rotlat;
    public int upmove;

    // Use this for initialization
    void Start () {
        pontos = 0;
        endtimer = false;
        endcount = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (headsensor.GetComponent<StartSensor>().start)
        {
            keyboardControl();
            if(head.transform.localPosition.x + 0.15 < headsensor.transform.localPosition.x)
            {
                if (!left.GetComponent<MoveSensor>().active)
                {
                    
                    lateral = -8;
                }
            }
            else if (head.transform.localPosition.x - 0.15 > headsensor.transform.localPosition.x)
            {
                if (!right.GetComponent<MoveSensor>().active)
                {
                    lateral = 8;
                }
            }
            else
            {
               
                lateral = 0;
            }

            speed = 0.1f;
            float space = head.transform.localPosition.y - headsensor.transform.localPosition.y;
            speed = (space * 0.1f) / 0.01f;
            speed *= 6;

            if (speed > 30)
            {
                speed = 30;
            }
            if (speed < 0)
            {
                speed = 0;
            }

            if (boosting)
            {
                boosttimer -= 1 * Time.deltaTime;
                speed += extraspeed;
                if(boosttimer <= 0)
                {
                    boosting = false;
                }
            }

            if(front.GetComponent<MoveSensor>().active)
            {
                speed = 0;
            }


            if(nimbus.transform.localRotation.eulerAngles.y > 15 && nimbus.transform.localRotation.eulerAngles.y < 180)
            {
                rotlat = 30;
            }
            else if(nimbus.transform.localRotation.eulerAngles.y > 180 && nimbus.transform.localRotation.eulerAngles.y <345)
            {
                rotlat = -30;
            }
            else
            {
                rotlat = 0;
            }

            if (nimbus.transform.localRotation.eulerAngles.x > 5 && nimbus.transform.localRotation.eulerAngles.x < 180)
            {
                upmove = 30;
            }
            else if (nimbus.transform.localRotation.eulerAngles.x > 180 && nimbus.transform.localRotation.eulerAngles.x < 355)
            {
                upmove = -30;
            }
            else
            {
                upmove = 0;
            }

            if (up.GetComponent<MoveSensor>().active || down.GetComponent<MoveSensor>().active)
            {
                upmove = 0;
            }

            Move(new Vector3(lateral, speed, 0), new Vector3(upmove, 0, rotlat));

            if (!mode)
            {
                texto.GetComponent<Text>().text = "Points: " + pontos;
                disttext.GetComponent<Text>().text = "";
            }
            else
            {
                disttext.GetComponent<Text>().text = "Distance: " + Mathf.Floor(Vector3.Distance(this.transform.position,bola.transform.position));
                texto.GetComponent<Text>().text = "";
            }
           
            textospeed.GetComponent<Text>().text = "Speed: " + (int)speed;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                eye.transform.Translate(new Vector3(0, 1.4f, 0));
            }
        }

        if(endtimer)
        {
            endcount += 1 * Time.deltaTime;
            if(endcount >= 5)
            {
                SceneManager.LoadScene("menu");
            }
        }
    }

    public void Move(Vector3 movement, Vector3 rotation)
    {

        gameObject.transform.Translate(movement * Time.deltaTime);
        gameObject.transform.Rotate(rotation * Time.deltaTime);
       
    }

    void keyboardControl()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            nimbus.transform.Rotate(new Vector3(-20,0,0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            nimbus.transform.Rotate(new Vector3(20, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nimbus.transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            nimbus.transform.Rotate(new Vector3(0, -60, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            eye.transform.Translate(new Vector3(0.5f, 0, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            eye.transform.Translate(new Vector3(-0.5f, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            eye.transform.Translate(new Vector3(0, 0, 0.5f) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            eye.transform.Translate(new Vector3(0, 0, -0.5f) * Time.deltaTime);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Arco")
        {
            pontos++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "boost")
        {
            boosting = true;
            boosttimer = 3;
        }

        else if (other.gameObject.tag == "endtrigger")
        {
            endtimer = true;
            endtext.GetComponent<Image>().enabled = true;
        }


    }
}
