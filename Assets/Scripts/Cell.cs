using UnityEngine;

public class Cell : MonoBehaviour
{
    public int status; //0: none, 1:sphere, 2:cube
    public GameManager gameManger;
    public GameObject sphere;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        sphere.SetActive(false);
        cube.SetActive(false);
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("AI") == 1)
        {
            if (status != 0)
            {
                gameManger.botCell();
                onClick();
            }
        }
    }
    
    void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("GameFinish") == 0 && PlayerPrefs.GetInt("AITurn") == 0)
        {
            onClick();
        }
    }
    public void onClick()
    {
        if (!cube.activeSelf && !sphere.activeSelf)
        {
            if (gameManger.isCubeTurn == true)
            {
                status = 2;
                cube.SetActive(true);
                sphere.SetActive(false);
                gameManger.isCubeTurn = false;
            }
            else
            {
                status = 1;
                sphere.SetActive(true);
                cube.SetActive(false);
                gameManger.isCubeTurn = true;
            }
        }
        gameManger.CheckWinner();
    }
}
