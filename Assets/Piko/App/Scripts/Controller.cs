using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Piko.App
{
    public abstract class Controller<M, V> : MonoBehaviour where M : Model where V : View
    {
        [SerializeField]
        protected M model;
        [SerializeField]
        protected V view;

        protected virtual void Enabled()
        {

        }

        private void OnEnable()
        {
            Enabled();
        }

        protected virtual void Disabled()
        {

        }

        private void OnDisable()
        {
            Disabled();
        }
    }

    [System.Serializable]
    public class Model
    {

    }

    [System.Serializable]
    public class View
    {

    }


}

