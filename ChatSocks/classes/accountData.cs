using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ChatSocks.classes
{
    class accountData
    {
        public string username;
        public messageSingle[] messages = new messageSingle[5000];
        public int messageCount = -1 ;
    
        public accountData(string name,string introMessage)
        {
            this.username = name;
            addMessage(introMessage,"client");
        }

        public void addMessage(string message, string from)
        {
            messageCount++;
            messages[messageCount] =  new  messageSingle(message,from);
            messages[messageCount].from = from;
        }

        public string getLastMessage()
        {
            return messages[messageCount].message;
        }

        public string GetName()
        {
            return this.username;
        }
    }
    
    class messageSingle
    {
        public string from;
        public string message;

        public messageSingle(string message, string from)
        {
            this.from = from;
            this.message = message;
        }
    }

}
