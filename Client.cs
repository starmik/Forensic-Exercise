// Decompiled with JetBrains decompiler
// Type: Syslog.Client
// Assembly: WMI Process Host, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 30F31E6A-63E0-45BD-BCAC-DDFCEE06997E
// Assembly location: Z:\Malware\1661599428\1661599428.exe

using System;
using System.Net;
using System.Text;

namespace Syslog
{
    public class Client
    {
        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint ipLocalEndPoint;
        private Helper helper;
        private string _port;
        private string _hostIp;

        public bool IsActive
        {
            get
            {
                return this.helper.IsActive;
            }
        }

        public string Port
        {
            get
            {
                return this._port;
            }
            set
            {
                if (this._port != null || this.IsActive)
                    return;
                this._port = value;
            }
        }

        public string HostIp
        {
            get
            {
                return this._hostIp;
            }
            set
            {
                if (this._hostIp != null || this.IsActive)
                    return;
                this._hostIp = value;
            }
        }

        public Client()
        {
            this.ipHostInfo = Dns.Resolve(Dns.GetHostName());
            this.ipAddress = this.ipHostInfo.AddressList[0];
            this.ipLocalEndPoint = new IPEndPoint(this.ipAddress, 0);
            this.helper = new Helper(this.ipLocalEndPoint);
        }

        public void Close()
        {
            if (!this.helper.IsActive)
                return;
            this.helper.Close();
        }

        public void Send(Message message)
        {
            if (!this.helper.IsActive)
                this.helper.Connect(this._hostIp, Convert.ToInt32(this._port));
            if (!this.helper.IsActive)
                throw new Exception("Syslog client Socket is not connected. Please set the host IP");
            byte[] bytes = Encoding.ASCII.GetBytes(string.Format("<{0}>{1}", (object)(message.Facility * 8 + message.Level), (object)message.Text));
            this.helper.Send(bytes, bytes.Length);
        }
    }
}
