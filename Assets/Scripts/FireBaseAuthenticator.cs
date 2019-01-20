using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseAuthenticator : MonoBehaviour
{

    AuthType authtype;
    public UIManager manager;
    string regEmail, regPass, loginEmail, loginPass;
    int isRegistered; //0 = false , 1 = true
    int rememberMe; //0 = false, 1 = true
    [SerializeField] Toggle checkBox;
    public string userId;
    int Ref;
    public static FireBaseAuthenticator Instance;
    public static string CurrentUser;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Ref = PlayerPrefs.GetInt("U");
        isRegistered = PlayerPrefs.GetInt("REGISTERED"); //is registered go to sign in
        rememberMe = PlayerPrefs.GetInt("REMEMBERME"); // remember the user info
        loginEmail = PlayerPrefs.GetString("EMAIL"); //remembers the email
        loginPass = PlayerPrefs.GetString("PASS"); // remembers the pass

        if (isRegistered.Equals(1))
        {
            manager.RegisterMenu.enabled = false;
            manager.LoginMenu.enabled = true;
        }
        else if (isRegistered.Equals(0))
        {
            manager.RegisterMenu.enabled = true;
            manager.LoginMenu.enabled = false;
        }
        if (rememberMe.Equals(1))
        {
            manager.LoginEmail.text = loginEmail;
            manager.LoginPassword.text = loginPass;
            checkBox.isOn = true;
        }
        else if (rememberMe.Equals(0))
        {
            PlayerPrefs.DeleteKey("EMAIL");
            PlayerPrefs.DeleteKey("PASS");
            checkBox.isOn = false;
        }
    }

    public void RememberInfo()
    {

        if (checkBox.isOn)
        {
            PlayerPrefs.SetInt("REMEMBERME", 1);
            rememberMe = PlayerPrefs.GetInt("REMEMBERME"); //REMEMBERS THE EMAIL AND THE PASSWORD;
        }
        else if (!checkBox.isOn)
        {
            PlayerPrefs.SetInt("REMEMBERME", 0);
        }
    }

    public void Registration()
    {
        authtype = AuthType.Register;
        regEmail = manager.RegistrationEmail.text;
        regPass = manager.RegistrationPassword.text;
        var Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (Auth != null)
        {

            Auth.CreateUserWithEmailAndPasswordAsync(regEmail, regPass).ContinueWith(task =>
             {
                 if (task.IsCanceled)
                 {
                     Debug.Log("Creating User With Email and Password was canceled");
                     return;
                 }
                 if (task.IsFaulted)
                 {
                     Debug.LogError("Create User With Email And Password Async encountered an error: " + task.Exception);
                     StartCoroutine(CustomLog(false));
                     return;
                 }

                 //User Has Been Created
                 Firebase.Auth.FirebaseUser newUser = task.Result;
                 FireBase.CurrentUserId = newUser.UserId;
                 PlayerPrefs.SetString("user", FireBase.CurrentUserId);
                 Debug.Log(FireBase.CurrentUserId);
                 Drone.USERID =newUser.UserId;
                 
                 StartCoroutine(CustomLog(true));
                 Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                 PlayerPrefs.SetInt("REGISTERED", 1); //set registration to true
                 manager.RegisterMenu.enabled = false;
                 manager.LoginMenu.enabled = true;

             });
        }
    }

    public void LogIn()
    {
        authtype = AuthType.Login;
        loginEmail = manager.LoginEmail.text;
        loginPass = manager.LoginPassword.text;

        var Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (Auth != null)
        {
            Auth.SignInWithEmailAndPasswordAsync(loginEmail, loginPass).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignIn With Email And PasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignIn With Email And PasswordAsync encountered an error: " + task.Exception);
                    StartCoroutine(CustomLog(false));
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                StartCoroutine(CustomLog(true));
                Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                PlayerPrefs.SetString("EMAIL", loginEmail); //sets the email
                PlayerPrefs.SetString("PASS", loginPass); //sets the pass
                manager.LoginMenu.enabled = false;
                manager.RegisterMenu.enabled = false;
                manager.HexesMenu.enabled = true;
            });
        }
    }

    IEnumerator CustomLog(bool success)
    {
        if (authtype == AuthType.Register)
        {
            if (success)
            {
                manager.Log.GetComponent<Text>().color = manager.Success;
                manager.Log.text = "user created successfully";
            }
            else
            {
                manager.Log.GetComponent<Text>().color = manager.Fail;
                manager.Log.text = "User with email address " + regEmail + " already exists, or the email email format is wrong";
            }
            yield return new WaitForSeconds(2f);
            manager.Log.text = "";
        }

        if (authtype == AuthType.Login)
        {
            if (success)
            {
                manager.Log.GetComponent<Text>().color = manager.Success;
                manager.Log.text = "user signed in successfully";
            }
            else
            {
                manager.Log.GetComponent<Text>().color = manager.Fail;
                manager.Log.text = "wrong password or email address";
            }
            yield return new WaitForSeconds(2f);
            manager.Log.text = "";

        }
    }

    enum AuthType
    {
        Register,
        Login
    }
}
