using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool isCubeTurn = false;
    public TextMeshProUGUI label;
    public Cell[] cells;
    public GameObject restart;
    public GameObject backToMenu;
    public GameObject waitforAI;
    public AudioClip draw;
    public AudioClip win;
    public AudioClip lose;
    public float AIWait;
    public bool isDraw;
    // Start is called before the first frame update
    void Start()
    {
        restart.SetActive(false);
        backToMenu.SetActive(false);
        waitforAI.SetActive(false);
        label.text = "Clica para empezar";
        label.color = Color.white;
        AIWait = 0.0f;
        PlayerPrefs.SetInt("GameFinish", 0);
        PlayerPrefs.SetInt("AITurn", 0);
        isDraw = true;
    }

    // Suponiendo que las cells se disponen de la siguiente forma:
    // 0 | 1 | 2
    // 3 | 4 | 5
    // 6 | 7 | 8

    public void botCell()
    {
        int aiCell = Random.Range(0, cells.Length);
        PlayerPrefs.SetInt("cellAI", aiCell);
    }
    public void CheckWinner()
    {
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (isCubeTurn)
                {
                    if (PlayerPrefs.GetFloat("AI") == 1)
                    {
                        if (i == PlayerPrefs.GetInt("cellAI") && cells[i].status == 0)
                        {
                            label.color = Color.blue;
                            label.text = "Turno de la IA.";
                            AIWait += Time.deltaTime;
                            PlayerPrefs.SetInt("AITurn", 1);
                            waitforAI.SetActive(true);
                            if (AIWait >= 3.0f)
                            {
                                waitforAI.SetActive(false);
                                cells[i].status = 2;
                                cells[i].cube.SetActive(true);
                                isCubeTurn = false;
                                AIWait = 0.0f;
                                PlayerPrefs.SetInt("AITurn", 0);
                            }
                        }
                    }
                    else
                    {
                        label.color = Color.red;
                        label.text = "Turno de Crosses.";
                    }
                }
                else
                {
                    if (PlayerPrefs.GetFloat("AI") == 1)
                    {
                        label.color = Color.green;
                        label.text = "Turno del jugador local.";
                    }
                    else
                    {
                        label.color = Color.yellow;
                        label.text = "Turno de Spheres.";
                    }
                }
                isDraw = true;
            }
            // Revisa las filas
            for (int i = 0; i < 9; i += 3)
            {
                if (cells[i].status != 0 && cells[i].status == cells[i + 1].status && cells[i + 1].status == cells[i + 2].status)
                {
                    DeclareWinner(cells[i].status);
                }
                if (cells[i].status == 0 || cells[i + 1].status == 0 || cells[i + 2].status == 0) isDraw = false;
            }

            // Revisa las columnas
            for (int i = 0; i < 3; i++)
            {
                if (cells[i].status != 0 && cells[i].status == cells[i + 3].status && cells[i + 3].status == cells[i + 6].status)
                {
                    DeclareWinner(cells[i].status);
                }
                if (cells[i].status == 0 || cells[i + 3].status == 0 || cells[i + 6].status == 0) isDraw = false;
            }

            // Revisa las diagonales
            if (cells[0].status != 0 && cells[0].status == cells[4].status && cells[4].status == cells[8].status)
            {
                DeclareWinner(cells[0].status);
            }

            if (cells[2].status != 0 && cells[2].status == cells[4].status && cells[4].status == cells[6].status)
            {
                DeclareWinner(cells[2].status);
            }

            // Si todas las celdas están llenas y no hay ganador, entonces es un empate.
            if (isDraw)
            {
                waitforAI.SetActive(false);
                label.color = Color.white;
                label.text = "¡Es un empate!";
                restart.SetActive(true);
                backToMenu.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(draw);
                PlayerPrefs.SetInt("GameFinish", 1);
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMainMenu(){
        SceneManager.LoadScene(0);
    }
    void DeclareWinner(int status)
    {
        if (status == 1)
        {
            SetUpGameFinish(true);
        }  
        else
        {
            SetUpGameFinish(false);
        }
        isDraw = false;
        waitforAI.SetActive(false);
        PlayerPrefs.SetInt("GameFinish", 1);
    }
    public void SetUpGameFinish(bool winner){
        restart.SetActive(true);
        backToMenu.SetActive(true);
        label.color = Color.white;
        if (winner){
            label.text = "¡Spheres han ganado!";
            GetComponent<AudioSource>().PlayOneShot(win);
        }
        else{
            label.text = "¡Crosses han ganado!";
            GetComponent<AudioSource>().PlayOneShot(lose);
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinner();
    }
}
