using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerB : MonoBehaviour
{
    public Text perdeuText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Perdeu()
    {
        perdeuText.gameObject.SetActive(true);
        Invoke("Resetar", 2f);


    }
    public void Resetar()
    {
        SceneManager.LoadScene(0);
    }
}
