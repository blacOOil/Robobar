using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkmakingMinigame : MonoBehaviour
{
    public float Alimit, Dlitmit,PassCenterAmount,speed = 10f;
    public GameObject drink,shakingGroup;
    public Transform CentPivot;
    public bool IsShakingfin, Isgamefin;
   public int ShakingCoustiing = 0;
    public bool isapressing = false;
   public bool isdpressing = false;
    // Start is called before the first frame update
    void Start()
    {
        PassCenterAmount = 0;
        IsShakingfin = false;
        Isgamefin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Isgamefin)
        {
            if (!IsShakingfin)
            {
                shakingGroup.SetActive(true);
                Shaking();
            }
            else
            {
              //  shakingGroup.SetActive(false);
                Isgamefin = true;
            }
        }
        else
        {
            
        }

        
    }
    public void ShakingCouting()
    {
        
        if(ShakingCoustiing > 5)
        {
            
            IsShakingfin = true;
        
        }
        else
        {

            if (!isapressing)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    ShakingCoustiing++;
                    isapressing = true;
                    isdpressing = false;
                }
            }
            if (!isdpressing)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    ShakingCoustiing++;
                    isapressing = false;
                    isdpressing = true;
                }
            }
           
        }
       
    }

    public void Shaking()
    {
        float moveInput = Input.GetAxis("Horizontal1");
        drink.transform.Translate(Vector3.right * moveInput * speed * Time.deltaTime);
        if (moveInput == 0)
        {
            drink.transform.position = Vector3.Lerp(drink.transform.position, CentPivot.position, speed * Time.deltaTime);
        }
        ShakingCouting();
    }
}
