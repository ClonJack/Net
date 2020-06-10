using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;

public class Server : MonoBehaviour
{
    #region private members 	
    /// <summary> 	
    /// TCPListener to listen for incomming TCP connection 	
    /// requests. 	
    /// </summary> 	
    private TcpListener tcpListener;
    /// <summary> 
    /// Background thread for TcpServer workload. 	
    /// </summary> 	
    private Thread tcpListenerThread;
    /// <summary> 	
    /// Create handle to connected tcp client. 	
    /// </summary> 	
    private TcpClient connectedTcpClient;
    #endregion

    #region  serializeField private members
    [SerializeField] private User user;
    #endregion
    // Use this for initialization
    #region private metods
    private void Start()
    {
        // Start TcpServer background thread 		
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }
    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
            tcpListener.Start();
            Debug.Log("Server is listening");
            byte[] bytes = new byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {

                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var getData = ByteHandler.ByteArrayToObject<DataUser>(bytes);

                            if (getData.Entry == Entry.SignUp)
                                user = getData;

                            else if (getData.Entry == Entry.SignIn && HandlerValidate.IsValidateData(user, getData))
                            {

                                user.IsActivePanel = true;
                                SendMessageClient();
                            }

                        }
                    }
                }

            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
    private void SendMessageClient()
    {

        if (connectedTcpClient == null)
            return;

        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = connectedTcpClient.GetStream();
            if (stream.CanWrite)
            {
                //  string serverMessage = null;
                // Convert string message to byte array.                 
                byte[] serverMessageAsByteArray = ByteHandler.ObjectToByteArray(user); /*Encoding.ASCII.GetBytes(serverMessage);*/
                // Write byte array to socketConnection stream.               
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                Debug.Log("Server sent his message - should be received by client");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    #endregion
}
