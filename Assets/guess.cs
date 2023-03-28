using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class guess : MonoBehaviour
{
   public TextMeshProUGUI textMeshProUGUI;
   public TMP_InputField inputfield;
    int random;
    int guess12;
    string guessnumber;
    bool set=false;
    // Start is called before the first frame update
    void Start()
    {
       set = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
   public  void guess1()
   {

        guessnumber = inputfield.text;
        guess12 = int.Parse(guessnumber);
        if (set==false)
        {
           random=Random.Range(0,100); 

        }
        Debug.Log(random);
        set=true;
        if (random>guess12)
        {
            textMeshProUGUI.text = "higher Number";
        }
        if (random<guess12)
        {
            textMeshProUGUI.text = "lessser number";

        }
        if (random==guess12)
        {
            textMeshProUGUI.text = "winner";
            set = false;  
        }
   }
}
