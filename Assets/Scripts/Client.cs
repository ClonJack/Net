using System.Net.Sockets;
using UnityEngine;
using System.Threading;
using System;

public class Client : MonoBehaviour
{
    #region private members
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion

    #region  serializeField private members
    [SerializeField] private HandlerData handler;
    #endregion
    // Use this for initialization 	
    #region private metods
    private void Start() => ConnectToTcpServer();
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient("localhost", 8052);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        handler.dataUser = ByteHandler.ByteArrayToObject<DataUser>(bytes);

                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    #endregion
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    #region public metods
    public void SendMessageServer()
    {

        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();

            if (stream.CanWrite)
            {
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = ByteHandler.ObjectToByteArray(handler.dataUser); /* Encoding.ASCII.GetBytes(message);*/
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");

            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    #endregion
}
