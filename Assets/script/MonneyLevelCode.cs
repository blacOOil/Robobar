using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonneyLevelCode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MoneyText;
    public float MonneyAmount;
  
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = MonneyAmount.ToString();
    }
    public void moneyAdd()
    {
        MonneyAmount += 1;
    }
}
