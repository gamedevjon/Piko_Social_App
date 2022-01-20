using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piko.App
{
    public class App : MonoSingleton<App>
    {
        public enum Panels
        {
            Registration, 
            Login,
            Home
        }

        public override void Init()
        {
            base.Init();
        }
     
        
        public void OpenPanel(Panels panel)
        {
            switch(panel)
            {
                case Panels.Registration:
                    Debug.Log("Opening Registration");
                    break;
                case Panels.Home:
                    Debug.Log("Opening Home");
                    break;
            }
        }


    }
}

