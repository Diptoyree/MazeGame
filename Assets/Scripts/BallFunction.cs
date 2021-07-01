using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BallFunction : MonoBehaviour
{
    public string NextLevel;
    public float Speed;
    public float Rot;
    int CoinNumber = 0;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI WinScore;
    public TextMeshProUGUI OverScore;
    public Text LifeLeft;
    public Text TimeLeft;
    public int Life;
    public float TTime;
    bool CanPlayed = true;

    public GameObject Win;
    public GameObject GameOver;
    public GameObject GamePlay;

    void Awake()
    {
        CoinNumber = PlayerPrefs.GetInt("Score", 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test(5));
        Win.SetActive(false);
        GameOver.SetActive(false);
        Score.text = "Score: " + CoinNumber.ToString();
        LifeLeft.text = "Life: " + Life.ToString();
    }

    //delay Function
    IEnumerator Test(float sec)
    {
        yield return new WaitForSeconds(sec);
        Debug.Log("Delaye Function");
    }
    // Update is called once per frame
    void Update()
    {
        if (CanPlayed)
        {
            if (TTime <= 0)
            {
                Debug.Log("Game Over");
                CanPlayed = false;
                GameOver.SetActive(true);
                GamePlay.SetActive(false);
            }
            else
            {
                TTime -= Time.deltaTime;
                TimeLeft.text = "Time Left: " + Mathf.Round(TTime);
            }
            PlayerMovement();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //coin collect
        if (other.gameObject.tag == "Coin")
        {
            FindObjectOfType<AudioManager>().Play("Coin");
            CoinNumber += 1;
            Destroy(other.gameObject);
            //Debug.Log(CoinNumber);
            Score.text = "Score: " + CoinNumber.ToString();
            WinScore.text = "Score: " + CoinNumber.ToString();
            OverScore.text = "Score: " + CoinNumber.ToString();
        }

        //game win
        if (other.gameObject.tag == "Win")
        {
            CanPlayed = false;
            Win.SetActive(true);

            GamePlay.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Wall")
        {
            FindObjectOfType<AudioManager>().Play("Life");
            Life -= 1;
            if (Life <= 0)
            {
                CanPlayed = false;
                GameOver.SetActive(true);
                GamePlay.SetActive(false);
                Debug.Log("Game Over");
            }
            LifeLeft.text = "Life: " + Life.ToString();
            Debug.Log("Wall Touched");
        }

        if (Col.gameObject.tag == "Moving")
        {
            //makeing child of an Object
            this.transform.parent = Col.transform;
            Debug.Log(Col.transform.position);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Moving")
        {
            this.transform.parent = null;
            Debug.Log(collision.transform.position);
        }
    }
    void PlayerMovement()
    {
        float X = Input.GetAxis("Horizontal") * Time.deltaTime * Rot;
        float Z = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
        transform.Translate(0, 0, Z);
        transform.Rotate(0, X, 0);
    }

    public void GoNext()
    {
        PlayerPrefs.SetInt("Score", CoinNumber);
        Debug.Log(NextLevel);
        SceneManager.LoadScene(NextLevel);
    }

    public void Reload()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

