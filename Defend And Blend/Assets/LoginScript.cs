using UnityEngine;
using System.Collections;
using Parse;

public class LoginScript : MonoBehaviour
{
    public string fullName;
    public string session;
    public string mailaddress;
    public string password;

    public bool backButton;
    public bool loginButton;
    public bool registerButton;

    public bool firstScreen;
    public bool loginScreen;
    public bool registerScreen;

    public bool loggedIn;

    public void Start()
    {
        resetAll();
        
    }
    public void resetAll()
    {
        loggedIn = false;
        fullName = "Voor en Achternaam";
        session = "001";
        mailaddress = "E-mailadres.nl";
        password = "Password";
        backButton = false;
        firstScreen = true;
        loginScreen = false;
        registerScreen = false;
    }
    void OnGUI()
    {
        
        if (firstScreen)
        {
            loginButton = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25F, 200, 20), "Login");
            registerButton = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 20), "Registreren");
            
            if (loginButton)
            {
                firstScreen = false;
                loginScreen = true;
                
            }
            else if (registerButton)
            {
                firstScreen = false;
                registerScreen = true;
            }
        }
        if (loginScreen)
        {
            fullName = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25F, 200, 20), fullName, 25);
            session = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 20), session, 25);
            password = GUI.PasswordField(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25F, 200, 20), password, "*"[0], 25);
            backButton = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50F, 100, 20), "Back");
            loginButton = GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 50F, 100, 20), "Login");

            if (backButton)
            {
                Start();
            }
            if (loginButton)
            {
                ParseUser.LogInAsync(fullName, password).ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        Debug.Log("Houston, there seems to be a problem with the " + fullName + " and the Password");
                    }
                    else
                    {
                        firstScreen = false;
                        loginScreen = false;
                        registerScreen = false;

                        loggedIn = true;

                        if (loggedIn)
                        {
                            Debug.Log("Login Successfully!");
                        }
                    }
                });
            }
        }
        if (registerScreen)
        {
            fullName = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25F, 200, 20), fullName, 25);
            mailaddress = GUI.TextField(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 20), mailaddress, 50);
            password = GUI.PasswordField(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 20), password, "*"[0], 25);
            backButton = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50F, 100, 20), "Back");
            registerButton = GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 50F, 100, 20), "Registreren");

            if (backButton)
            {
                Start();
            }

            if (registerButton)
            {
                var user = new ParseUser()
                {
                    Username = fullName,
                    Password = password,
                    Email = mailaddress
                };
                user.SignUpAsync();
                
                    Debug.Log("User: " + fullName + " Succesfully registered.");
               
            }
        }
    }
}
