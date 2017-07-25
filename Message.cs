// Decompiled with JetBrains decompiler
// Type: Syslog.Message
// Assembly: WMI Process Host, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 30F31E6A-63E0-45BD-BCAC-DDFCEE06997E
// Assembly location: Z:\Malware\1661599428\1661599428.exe

namespace Syslog
{
    public class Message
    {
        private int _facility;
        private int _level;
        private string _text;

        public int Facility
        {
            get
            {
                return this._facility;
            }
            set
            {
                this._facility = value;
            }
        }

        public int Level
        {
            get
            {
                return this._level;
            }
            set
            {
                this._level = value;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public Message()
        {
        }

        public Message(string text)
        {
            this._text = text;
        }
    }
}
