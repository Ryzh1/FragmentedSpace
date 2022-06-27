using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private Shooting shooting;
    public TMP_Text ammoCount;
    public int minShots;

    public GameObject gameOver;
    public GameObject UI;

    public GameObject leftStar;
    public GameObject rightStar;
    public GameObject middleStar;
    public GameObject mmButton;
    public GameObject nextLevelButton;
    public GameObject retryButton;
    private bool ammoEmpty;
    private bool? WinState = null;
    private GoalZoneManager goalManager;

    // Start is called before the first frame update
    void Start()
    {
        goalManager = GameObject.FindObjectOfType<GoalZoneManager>();
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        gameOver.SetActive(false);
        UI.SetActive(true);
        ammoEmpty = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        if (WinState != null)
        {
            return;
        }
        ammoCount.text = "Ammo: x" + shooting.ammoCount;
        if (shooting.ammoCount <= 0 && !ammoEmpty)
        {
           
            var rigidBodies = GameObject.FindObjectsOfType<Rigidbody2D>().Where(x => x.bodyType == RigidbodyType2D.Dynamic);
            var velocities = rigidBodies.Where(x => x.velocity != Vector2.zero).ToList();
            if (!velocities.Any() && !goalManager.AllValid)
            {
                StartCoroutine(RetryDelay());
            }
        }

      
    }

    IEnumerator RetryDelay()
    {
        yield return new WaitForSeconds(2f);
        if(WinState != null)
        {
            StopCoroutine(RetryDelay());
            yield return null;
        }
        else
        {
            gameOver.SetActive(true);
            shooting.enabled = false;
            retryButton.SetActive(true);
            mmButton.SetActive(true);
            ammoEmpty = true;
            WinState = false;
        }
     
    }



    public void Over()
    {
       
        if (WinState != null)
        {
            return;
        }
        WinState = true;
        shooting.enabled = false;
        gameOver.SetActive(true);
        UI.SetActive(false);

        if(shooting.shotsTaken <= minShots)
        {

            StartCoroutine(Stars(3));

        }
        else if(shooting.shotsTaken <= minShots + 1)
        {

            StartCoroutine(Stars(2));
        }
        else if (shooting.shotsTaken <= minShots + 2)
        {
            
            StartCoroutine(Stars(1));
        }
        else
        {
            StartCoroutine(Stars(0));
        }

    }

    IEnumerator Stars(int starAmount)
    {
        yield return new WaitForSeconds(0.5f);
        switch (starAmount)
        {
            case 0:
                break;
            case 1:
                leftStar.SetActive(true);
                break;
            case 2:
                leftStar.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                rightStar.SetActive(true);
                break;
            case 3:
                leftStar.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                rightStar.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                middleStar.SetActive(true);
                break;

        }
        yield return new WaitForSeconds(0.5f);
        
        mmButton.SetActive(true);
        nextLevelButton.SetActive(true);
    }


    public void LevelSelect()
    {
        var nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
