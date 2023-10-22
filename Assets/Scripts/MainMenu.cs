using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Toggle aiToggle;
    // Start is called before the first frame update
    void Start()
    {
        int flag = PlayerPrefs.GetInt("AI");
        if (flag == 0){
            aiToggle.isOn = false;
        }
        else{
            aiToggle.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void aiToggleChange(bool flag){
        if (aiToggle.isOn){
            PlayerPrefs.SetFloat("AI", 1);
        }
        else {
            PlayerPrefs.SetFloat("AI", 0);
        }
    }
    public void StartGame(){
        SceneManager.LoadScene(1);
    }
}
