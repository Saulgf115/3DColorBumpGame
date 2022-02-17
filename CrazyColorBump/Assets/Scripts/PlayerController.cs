using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Atrributes")]
    Rigidbody rb;
    Vector3 lastMousePosition;
    public float sensivity = 0.16f;
    public float clampDelta = 42.0f;

    public float bounds = 5;

    [HideInInspector]
    public bool canMove = false,gameOver = false,finish;

    public GameObject breakablePlayer;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-bounds,bounds),transform.position.y,transform.position.z);

        if(canMove)
            transform.position += FindObjectOfType<CameraMovement>().cameraVelocity;

        //if(!canMove && !gameOver)
        //{
        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        canMove = true;
        //    }
        //}

        if(!canMove && gameOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                Time.timeScale = 1.0f;

                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }

        }else if (!canMove && !finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<GameManager>().RemoveUI();
                canMove = true;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if(canMove && !finish)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 vector = lastMousePosition - Input.mousePosition;

                lastMousePosition = Input.mousePosition;

                vector = new Vector3(vector.x, 0.0f, vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
                rb.AddForce(/*Vector3.forward * 2 +*/ (-moveForce * sensivity - rb.velocity / 5.0f), ForceMode.VelocityChange);
                //rb.AddForce(-moveForce * sensivity - rb.velocity / 5, ForceMode.VelocityChange);
            }
        }

       

        rb.velocity.Normalize();
    }

    void GameOver()
    {
        GameObject shatterSphere = Instantiate(breakablePlayer,transform.position,Quaternion.identity);

        foreach(Transform o in shatterSphere.transform)
        {
            //o.GetComponent<Rigidbody>().AddForce(Vector3.forward * rb.velocity.magnitude,ForceMode.Impulse);
            o.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
        }

        //shatterSphere.transform.position = Vector3.zero;

        canMove = false;
        gameOver = true;

        GetComponent<MeshRenderer>().enabled = false;

        GetComponent<Collider>().enabled = false;

        //Time.timeScale = .25f;

        //Time.timeScale = 0.4f;
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }


    IEnumerator NextLevel()
    {
        finish = true;

        canMove = false;

        //PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level",1));

        yield return new WaitForSeconds(1);


        //SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
        SceneManager.LoadScene("Level2");

    }

    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Enemy")
        {
            if(!gameOver)
                GameOver();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            StartCoroutine(NextLevel());
        }
    }

}
