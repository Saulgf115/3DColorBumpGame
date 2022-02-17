using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgImage : MonoBehaviour
{

    Color randomColor;

    // Start is called before the first frame update
    void Start()
    {
        RandomColor();
    }

    void RandomColor()
    {
        randomColor = new Color(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f));

        GetComponent<SpriteRenderer>().color = randomColor;
    }

   
}
