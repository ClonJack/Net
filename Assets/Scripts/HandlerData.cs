using UnityEngine;
using TMPro;
public class HandlerData : MonoBehaviour
{
    #region public members
    public DataUser dataUser;
    #endregion

    #region serializeField private members
    [SerializeField] private GameObject signed;
    #endregion

    #region private metods 
    private void Update() => signed.SetActive(dataUser.IsActivePanel);
    #endregion

    #region public metods
    public void InputLogin(TMP_InputField input) => dataUser.Login = input.text;
    public void InputPassword(TMP_InputField input) => dataUser.Password = input.text;
    #endregion
}
