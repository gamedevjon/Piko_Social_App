using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;


namespace Piko.App.Controllers
{
    [System.Serializable]
    public class LoginModel : Model
    {
        private LoginController _controller;
        private string _email, _password;

        public void Login(string email, string password, LoginController controller)
        {
            this._email = email;
            this._password = password;
            this._controller = controller;
            Process();
        }

        void Process()
        {
            var auth = FirebaseAuth.DefaultInstance;
            auth.SignInWithEmailAndPasswordAsync(_email, _password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Login Cancelled");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("Error Logging In: " + task.Exception);
                    return;
                }

                var user = task.Result;

                Debug.Log("Successfully Signed In: " + user.Email);

                _controller.LoginComplete();

            });
        }
    }

    [System.Serializable]
    public class LoginView: View
    {
        [SerializeField]
        private TMP_InputField _email;
        [SerializeField]
        private TMP_InputField _password;

        public string GetEmail() => _email.text;
        public string GetPassword() => _password.text;
    }

    public class LoginController : Controller<LoginModel, LoginView>
    {
        

        protected override void Enabled()
        {
            base.Enabled();
        }


        public void ProcessLogin()
        {
            model.Login(view.GetEmail(), view.GetPassword(), this);
        }

        public void LoginComplete()
        {
            Debug.Log("Display Home Page");
            this.gameObject.SetActive(false);
        }


        protected override void Disabled()
        {
            base.Disabled();
        }


    }
}

