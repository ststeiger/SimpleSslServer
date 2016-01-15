
using System.Net.Sockets;
using System.Net.Security;


// http://www.dib0.nl/code/343-using-ssl-over-tcp-as-client-and-server-with-c
// https://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx



// http://stackoverflow.com/questions/252365/creating-a-tcp-client-connection-with-ssl
// https://www.simple-talk.com/dotnet/.net-framework/tlsssl-and-.net-framework-4.0/


namespace MySimpleSslServer
{


    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
        }


        public static void Server()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1300);
            listener.Start();

            // Wait for a client to connect on TCP port 1300
            TcpClient clientSocket = listener.AcceptTcpClient();
            System.Security.Cryptography.X509Certificates.X509Certificate certificate = 
                new System.Security.Cryptography.X509Certificates.X509Certificate("..\\path\\tp\\Certificate.pfx", "ThisPasswordIsTheSameForInstallingTheCertificate");

            // Create a stream to decrypt the data
            using (SslStream sslStream = new SslStream(clientSocket.GetStream()))
            {
                sslStream.AuthenticateAsServer(certificate);
                // ... Send and read data over the stream

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("_message");
                sslStream.Write(buffer, 0, buffer.Length); 

            }
        }


    }
}
