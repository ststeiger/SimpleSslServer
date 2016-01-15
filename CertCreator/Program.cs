using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CertCreator
{
    class Program
    {

        public static string[] SplitCommandLine(string commandLine)
        {
            bool inQuotes = false;

            System.Text.StringBuilder sb = new StringBuilder();

            List<string> ls = new List<string>();
            string prm;

            for (int i = 0; i < commandLine.Length; ++i )
            {
                char c = commandLine[i];

                char bla = 'a';
                if(i!=0)
                    bla = commandLine[i-1];


                if (c == '\"' && bla != '\\')
                {
                    inQuotes = !inQuotes;
                    continue;
                }
                    

                if (!inQuotes && c == ' ')
                {
                    prm = sb.ToString();
                    if (!string.IsNullOrEmpty(prm) && prm.Trim() != string.Empty)
                            ls.Add(prm);

                    sb.Clear();
                    prm = null;
                    continue;
                }

                sb.Append(c);
            }

            prm = sb.ToString();
            if (!string.IsNullOrEmpty(prm) && prm.Trim() != string.Empty)
                ls.Add(prm);

            sb.Clear();
            prm = null;

            return ls.ToArray();
        }



        static void Main(string[] args)
        {
            string[] myargs1 = SplitCommandLine("-r -pe -n \"CN=My Root Authority\" -ss CA -sr CurrentUser -a sha1 -sky signature -cy authority -sv CA.pvk CA.cer");
            string[] myargs2 = SplitCommandLine("-pe -n \"CN=fqdn.of.server\" -a sha1 -sky Exchange -eku 1.3.6.1.5.5.7.3.1 -ic CA.cer -iv CA.pvk -sp \"Microsoft RSA SChannel Cryptographic Provider\" -sy 12 -sv server.pvk server.cer");


            // http://stackoverflow.com/questions/496658/using-makecert-for-development-ssl 

            // https://github.com/mono/mono/blob/master/mcs/tools/security/makecert.cs
            // https://raw.githubusercontent.com/mono/mono/master/mcs/tools/security/makecert.cs
            // http://www.bouncycastle.org/wiki/display/JA1/X.509+Public+Key+Certificate+and+Certification+Request+Generation
            Mono.Tools.MakeCert.MyCert(myargs1);
            Mono.Tools.MakeCert.MyCert(myargs2);
        }
    }
}
