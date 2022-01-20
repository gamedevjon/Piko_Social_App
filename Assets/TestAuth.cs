using Firebase.Auth;
using UnityEngine;

public class TestAuth : MonoBehaviour
{
    [SerializeField]
    private TextAsset _googleAuthObj;
    // Start is called before the first frame update
    void Start()
    {
        var auth = FirebaseAuth.DefaultInstance;

        var googleAuth = JsonUtility.FromJson<GoogleAuth>(_googleAuthObj.text);

      


        Debug.Log("AUTH URL:" + googleAuth.web.auth_uri);

        Application.OpenURL(googleAuth.web.auth_uri+"?client_id="+googleAuth.web.client_id+"&redirect_uri=urn:ietf:wg:oauth:2.0:oob&response_type=code&scope=email/");


        /*auth.CreateUserWithEmailAndPasswordAsync("test@gmail.com", "Gakljdsljadl!1").ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Task was cancelled");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("Task errored: " + task.Exception);
                return;
            }

            var newUser = task.Result;
            Debug.Log("Successfully created user: " + newUser.Email + " Display Name: " + newUser.DisplayName);


        });*/

        /*auth.SignInWithEmailAndPasswordAsync("test@gmail.com", "Gakljdsljadl!1").ContinueWith(task =>
        {
            var User = task.Result;

            Debug.Log("Successfully signed in: " + User.Email);
        });*/


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[System.Serializable]
public class Web
{
    public string client_id;
    public string project_id;
    public string auth_uri;
    public string token_uri;
    public string auth_provider_x509_cert_url;
    public string client_secret;
}

[System.Serializable]
public class GoogleAuth
{
    public Web web;
}

