
using System.Net.Sockets;
using System.Net.Security;


// http://www.dib0.nl/code/343-using-ssl-over-tcp-as-client-and-server-with-c


namespace MySimpleSslClient
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main(string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
        }


        static void Test()
        {
            // Connect as client to port 1300
            string server = "127.0.0.1";
            TcpClient client = new TcpClient(server, 1300);

            // Create a secure stream
            using (SslStream sslStream = new SslStream(client.GetStream(), false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate), null))
            {
                sslStream.AuthenticateAsClient(server);

                // ... Send and read data over the stream
                byte[] buffer = new byte[1024];
                int n = sslStream.Read(buffer, 0, 1024);


                string _message = System.Text.Encoding.UTF8.GetString(buffer, 0, n);
                System.Console.WriteLine("Client said: " + _message); 

            }

            // Disconnect and close the client
            client.Close();
        }


        // The following method is invoked by the RemoteCertificateValidationDelegate.
        // This allows you to check the certificate and accept or reject it
        // return true will accept the certificate
        public static bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            
            // Accept all certificates
            return true;


            switch (sslPolicyErrors)
            {
                case SslPolicyErrors.RemoteCertificateNameMismatch:
                    System.Console.WriteLine("Client's name mismatch. End communication ...\n");
                    return false;
                case SslPolicyErrors.RemoteCertificateNotAvailable:
                    System.Console.WriteLine("Client's certificate not available. End communication ...\n");
                    return false;
                case SslPolicyErrors.RemoteCertificateChainErrors:
                    System.Console.WriteLine("Client's certificate validation failed. End communication ...\n");
                    return false;
            } 


        }


    }
}
