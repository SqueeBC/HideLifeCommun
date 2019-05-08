using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {   
        private GameObject CurrentKey;
        private Dictionary<string,string> Marquage = new Dictionary<string, string>();
        private Color32 normal = new Color32(255, 255, 255,255);
        private Color32 selected = new Color32(239, 116, 36,255);
        
        public static InputManager inputManager;
        public KeyCode ForwardKey { get; set; }
        public KeyCode BackwardKey { get; set; }
        public KeyCode LeftKey { get; set; }
        public KeyCode RightKey { get; set; }
        public KeyCode JumpKey { get; set; }
        public KeyCode RunKey { get; set; }

        public KeyCode PauseKey { get; set; }
        
        public KeyCode ReloadKey { get; set; }

        public Text forwardtext, backwardtext, lefttext, righttext, jumptext, runtext, Pause1text, reloadtext;
        

        void Awake() //Awake est appelée avant Start
        {
           
            //utilisation des playerspref pour stocker les changements
            JumpKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("JumpKey", "Space"));
            ForwardKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("ForwardKey", "Z"));
            BackwardKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("BackwardKey", "S"));
            LeftKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("LeftKey", "Q"));
            RightKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("RightKey", "D"));
            PauseKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("PauseKey", "A"));           
            RunKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("RunKey", "E"));
            ReloadKey = (KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("ReloadKey", "R"));

        }

        private void Start()
        {
            
            forwardtext.text = PlayerPrefs.GetString("ForwardKey", "Z");
            backwardtext.text = PlayerPrefs.GetString("BackwardKey", "S");
            lefttext.text = PlayerPrefs.GetString("LeftKey", "Q");
            righttext.text = PlayerPrefs.GetString("RightKey", "D");
            jumptext.text = PlayerPrefs.GetString("JumpKey", "Space");
            runtext.text = PlayerPrefs.GetString("RunKey", "E");
            reloadtext.text = PlayerPrefs.GetString("ReloadKey", "R");
            Marquage.Add(forwardtext.text,"ForwardKey");
            Marquage.Add( backwardtext.text, "BackwardKey");
            Marquage.Add(lefttext.text,"LeftKey");
            Marquage.Add(righttext.text,"RightKey");
            Marquage.Add( jumptext.text,"JumpKey");
            Marquage.Add(  runtext.text,"RunKey");
            Marquage.Add(reloadtext.text,"ReloadKey");

         
        }
        

        private void OnGUI()
        {


            if (CurrentKey != null)
            {
                Event e = Event.current; //cet event est égal a la touche activée 
                if (e.isKey)
                {
                    if (!Marquage.ContainsKey(    e.keyCode.ToString()) && e.keyCode!=KeyCode.None)
                    {
                      
                           
                        Marquage.Remove((CurrentKey.transform.GetChild(0).GetComponent<Text>().text));
                        Marquage.Add(e.keyCode.ToString(), CurrentKey.name);

                        PlayerPrefs.SetString(CurrentKey.name, e.keyCode.ToString());
                        Debug.Log(CurrentKey);
                        CurrentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                        CurrentKey.GetComponent<Image>().color = normal;
                        CurrentKey = null;

                    }
                }
            }
        }

        public void ChangeKey(GameObject clicked)
        {   Debug.Log(clicked.ToString());
            if(CurrentKey!=null)  // si on déjà selectionné une autre touche
                CurrentKey.GetComponent<Image>().color = normal;
        
                CurrentKey = clicked;
            
            CurrentKey.GetComponent<Image>().color = selected;
         
        }
    }
}