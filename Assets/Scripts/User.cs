using System;

[Serializable]
public class User
{
    public string Login;
    public string Password;
    public bool IsActivePanel;
}

[Serializable]
public class DataUser:User
{
    public Entry Entry;
}
