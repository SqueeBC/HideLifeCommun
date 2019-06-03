using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class MaskPassword : MonoBehaviour
{
    
    string[] maskArray = new string[500];
    uint maskIndex = 0;

    public InputField PasswordInput;
    public Text MaskOutput;
    public Text ZoneMdp;

    public void MaskPasswordFunction()
    {
        /*if (ZoneMdp.text.Length != maskIndex)
        {
            maskIndex = ZoneMdp.text.Length;
        }*/
        /*if (ZoneMdp.text == "")
        {
            maskIndex = 0;
        }*/
    
        if (Input.GetKey(KeyCode.Backspace))
        {
            /*if (maskIndex < 0)
            {
                maskIndex = 0;
                MaskOutput.text = maskArray[maskIndex];
                ZoneMdp.text = "";
            }
            else*/
            {
                maskIndex--;
                MaskOutput.text = maskArray[maskIndex];
            }
        }
        else
        {
            maskIndex++;
            MaskOutput.text = maskArray[maskIndex];
        }
    }

    void Update()
    {
        
        Debug.Log(PasswordInput.text);
    }

    void Start()
    {
        maskArray[0] = "";
        string mask = "";
        for (int count = 1; count <= 499; count++)
        {
            maskArray[count] = mask + "•";
            mask += "•";
        }
    }
    
}
