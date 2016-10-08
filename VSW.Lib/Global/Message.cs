using System;
using System.Collections.Generic;


namespace VSW.Lib.Global
{
    public class Message
    {
        public enum MessageTypeEnum
        {
            Message = 0,
            Error = 1,
            Notice = 2
        }

        private List<string> list = new List<string>();

        public Message()
        {
           
        }

        public Message(MessageTypeEnum MessageType)
        {
            this.MessageType = MessageType;
        }

        public void Clear()
        {
            ListMessage.Clear();
            MessageType = MessageTypeEnum.Message;
        }

        public List<string> ListMessage
        {
            get { return list; }
        }

        public string MessageTypeName
        {
            get 
            {
                return MessageType.ToString().ToLower();
            }
        }

        private MessageTypeEnum _MessageType = MessageTypeEnum.Message;
        public MessageTypeEnum MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }
    }
}