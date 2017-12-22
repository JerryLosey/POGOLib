using System;
using System.Net;

namespace POGOLib.Official.Extensions
{
    internal class WebProxy : IWebProxy
    {
        public string Host { get; set; }
        public int Port { get; set; }
        private ICredentials _credentials;
        public ICredentials Credentials { 
            get {
                return _credentials;
            }
            set { 
                _credentials = value;
                return;
            }
        }

        public WebProxy()
        {
        }

        public WebProxy(string host, int port)
        {
            Host = host;
            Port = port;
        }
        public WebProxy(string host, int port, string username, string password)
        {
            Host = host;
            Port = port;
            
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password)) {
                Credentials = new NetworkCredential(username, password);
            }
        }

        public void SetCredentials(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password)) {
                Credentials = new NetworkCredential(username, password);
            }
        }


        public static bool TryParse(string combo, out WebProxy result)
        {
            result = new WebProxy();

            string[] parts = combo.Split(':');

            if (parts.Length != 2 && parts.Length != 4) {
                //throw new ArgumentException("proxyCombo must be in the format IP:PORT or IP:PORT:USERNAME:PASSWORD");

                return false;
            }

            int port = 0;

            if (!Int32.TryParse(parts[1], out port)) {
                //throw new ArgumentException(String.Format("Invalid port value \"{0}\"", parts[1]));

                return false;
            }

            result.Host = parts[0];
            result.Port = port;

            if (parts.Length == 4) {
                if (!String.IsNullOrEmpty(parts[2]) && !String.IsNullOrEmpty(parts[3])) {
                    result.Credentials = new NetworkCredential(parts[2], parts[3]);
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            var proxyEx = obj as WebProxy;

            if (proxyEx == null) {
                return base.Equals(obj);
            }

            return proxyEx.Host == Host && proxyEx.Port == Port;
        }

        public override int GetHashCode()
        {
            return (Host + ":" + Port).GetHashCode();
        }

        public override string ToString()
        {

            return !String.IsNullOrEmpty(Host) ?
                String.Format("{0}:{1}", Host, Port) :
                String.Empty;

        }

        public Uri GetProxy(Uri destination)
        {
            //NOTE: this returns the Uri of the proxy to be used [ex. http://host:port].
            var address = Host;
            if (Host.IndexOf("http", StringComparison.Ordinal) < 0) {
                address = "http://" + Host;
            }
            return new Uri(address + ":" + Port);
        }

        public bool IsBypassed(Uri host)
        {
            //NOTE: "is bypassed" is to bypass (skip) the proxy, so never should be true to our propouses.
            return false;
        }
    }
}