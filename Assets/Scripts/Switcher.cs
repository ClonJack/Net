using UnityEngine;

public class Switcher : MonoBehaviour
{
    #region  serializeField private members
    [SerializeField] private Entry entry;
    [SerializeField] private HandlerData handler;
    #endregion

    #region public methods
    public void ToggleData()
    {
        Debug.Log("ToggleData");
        handler.dataUser.Login = null;
        handler.dataUser.Password = null;
        handler.dataUser.Entry = entry;     
    }
    #endregion
}
