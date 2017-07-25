// Decompiled with JetBrains decompiler
// Type: Syslog.Helper
// Assembly: WMI Process Host, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 30F31E6A-63E0-45BD-BCAC-DDFCEE06997E
// Assembly location: Z:\Malware\1661599428\1661599428.exe

using System.Net;
using System.Net.Sockets;

namespace Syslog
{
    public class Helper : UdpClient
    {
        public bool IsActive
        {
            get
            {
                return this.Active;
            }
        }

        public Helper()
        {
        }

        public Helper(IPEndPoint ipe)
          : base(ipe)
        {
        }

        ~Helper()
        {
            if (!this.Active)
                return;
            this.Close();
        }
    }
}
