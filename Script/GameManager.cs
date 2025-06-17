using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Ustawienia kuli")]
    public GameObject Gracz;
    public Transform PozycjaPocz¹tkowa;

    [Header("Ustawienia Czas")]

    public TextMeshProUGUI TimeText;
    public float czas;

    private void Awake()
    {
        Instantiate(Gracz, PozycjaPocz¹tkowa.position, Quaternion.identity);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        czas -= Time.deltaTime;

        TimeText.text = "Czas:" + Mathf.Clamp(Mathf.CeilToInt(czas), 0, int.MaxValue).ToString();
        if(czas <= 0)
        {
            RestartPoziomu();
        }
    }
    public void RestartPoziomu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
