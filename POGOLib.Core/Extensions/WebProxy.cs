using System;
using System.Net;

namespace POGOLib.Official.Extensions
{
    internal class WebProxy : IWebProxy
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICredentials Credentials { get; set; }

        public WebProxy()
        {
        }

        public WebProxy(string address, int port)
        {
            Address = address;
            Port = port;
        }

        public WebProxy AsWebProxy()
        {
            if (String.IsNullOrEmpty(Address) || Port == 0)
            {
                return null;
            }

            WebProxy proxy = new WebProxy(Address, Port);

            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                proxy.Credentials = new NetworkCredential(Username, Password);
            }

            return proxy;
        }

        public static bool TryParse(string combo, out WebProxy result)
        {
            result = new WebProxy();

            string[] parts = combo.Split(':');

            if (parts.Length != 2 && parts.Length != 4)
            {
                //throw new ArgumentException("proxyCombo must be in the format IP:PORT or IP:PORT:USERNAME:PASSWORD");

                return false;
            }

            int port = 0;

            if (!Int32.TryParse(parts[1], out port))
            {
                //throw new ArgumentException(String.Format("Invalid port value \"{0}\"", parts[1]));

                return false;
            }

            result.Address = parts[0];
            result.Port = port;

            if (parts.Length == 4)
            {
                result.Username = parts[2];
                result.Password = parts[3];
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            WebProxy proxyEx = obj as WebProxy;

            if (proxyEx == null)
            {
                return base.Equals(obj);
            }

            return proxyEx.Address == this.Address && proxyEx.Port == this.Port;
        }

        public override int GetHashCode()
        {
            return this.Address.GetHashCode() * 3 + this.Port.GetHashCode() * 17;
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                return String.Format("{0}:{1}:{2}:{3}", Address, Port, Username, Password);
            }

            if (!String.IsNullOrEmpty(Address))
            {
                return String.Format("{0}:{1}", Address, Port);
            }

            return String.Empty;
        }

        public Uri GetProxy(Uri destination)
        {
            //TODO: revise
            return new Uri(Address);
        }

        public bool IsBypassed(Uri host)
        {
            //TODO: revise
            return true;
        }
    }
}