using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piko.App;
using TMPro;
using Firebase.Auth;
using Piko.App;

namespace Piko.App.Controllers
{
    [System.Serializable]
    public class RegistrationModel : Model
    {
        private string _email, _password, _confirmPassword;
        RegistrationController _controller;

        public void Register(string email,string password,string confirm, RegistrationController controller)
        {
            this._email = email;
            this._password = password;
            this._confirmPassword = confirm;
            this._controller = controller;
            TryToRegister();
        }

        void TryToRegister()
        {
            if (_password != _confirmPassword)
            {
                _controller.Fail(1);
                return;
            }

            var auth = FirebaseAuth.DefaultInstance;
            auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    _controller.Fail(2);
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("Error with signing up: " + task.Exception);
                    return;
                }

                var newUser = task.Result;
                Debug.Log("Successfully registered: " + newUser.Email);
                _controller.Success();
            });

        }
    }

    [System.Serializable]
    public class RegistrationView : View
    {
        [SerializeField]
        private TMP_InputField _email, _password, _confirmPassword;

        public string GetEmail() => _email.text;
        public string GetPassword() => _password.text;
        public string GetConfirmPassword() => _confirmPassword.text;
    }

    public class RegistrationController : Controller<RegistrationModel, RegistrationView>
    {
        [SerializeField]
        private App.Panels _nextPanel; 

        public void Register()
        {
            model.Register(view.GetEmail(), view.GetPassword(), view.GetConfirmPassword(), this);
        }

        public void Success()
        {
            Debug.Log("Show Home Page");
            App.Instance.OpenPanel(_nextPanel);
        }

        public void Fail(int statusCode)
        {
            switch(statusCode)
            {
                case 1: // passwords don't match
                    Debug.Log("Passwords don't match!");
                    break;
                case 2: //cancelled signup
                    Debug.Log("Signup cancelled. Try again.");
                    break;
            }
        }
    }
}


