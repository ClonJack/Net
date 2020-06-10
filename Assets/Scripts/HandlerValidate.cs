
public static class HandlerValidate
{
    public static bool IsValidateData(User userServer, User userClient)
    {
      return (userServer.Login == userClient.Login && userServer.Password == userClient.Password);
    }
}
