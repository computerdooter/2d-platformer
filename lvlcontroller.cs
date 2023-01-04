using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lvlcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    
   public void yofirst()
    {
        SceneManager.LoadScene("yofirst");
    }

   public void Lvl2()
    {
        SceneManager.LoadScene("Lvl2");
    }

   public void congrats()
    {
        SceneManager.LoadScene("congrats");
    }
    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }
}
