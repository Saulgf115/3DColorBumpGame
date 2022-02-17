using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text cLevelText, nLevelText;
    Image fill;

    float startDistance, distance;

    GameObject player, finish,hand;

    TextMesh levelNumber;

    public int currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        cLevelText = GameObject.Find("CurrentLevelNumber").GetComponent<Text>();

        nLevelText = GameObject.Find("NextLevelNumber").GetComponent<Text>();

        fill = GameObject.Find("Fill").GetComponent<Image>();

        player = GameObject.Find("Player");
        finish = GameObject.Find("Finish");

        levelNumber = GameObject.Find("LevelNumber").GetComponent<TextMesh>();

        hand = GameObject.Find("HandImage");
    }

    
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level");

        levelNumber.text = "LEVEL " + currentLevel;

        nLevelText.text = currentLevel + 1 + "";
        cLevelText.text = currentLevel.ToString();

        startDistance = Vector3.Distance(player.transform.position,finish.transform.position);

        //SceneManager.LoadScene("Level" + currentLevel);

    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(player.transform.position, finish.transform.position);

        if(player.transform.position.z < finish.transform.position.z)
            fill.fillAmount = 1 - (distance / startDistance);
    }

    public void RemoveUI()
    {
        hand.SetActive(false);
    }
}
