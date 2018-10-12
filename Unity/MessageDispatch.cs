using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BigRogue {

    /// <summary>
    /// 处理消息的委托
    /// </summary>
    /// <param name="sender">发送消息的对象</param>
    /// <param name="content">消息的内容</param>
    public delegate void MessageHandler(object sender, Message content);



    /// <summary>
    /// 消息对象的基类
    /// </summary>
    public class Message {

        /// <summary>
        /// 空的消息
        /// </summary>
        public static readonly Message Empty;

        /// <summary>
        /// 消息ID
        /// </summary>
        public MessageCode messageID { get; set; }

        public Message(MessageCode messageID) {
            this.messageID = messageID;
        }
    }

    
    /// <summary>
    /// 消息ID枚举
    /// </summary>
    public enum MessageCode {
        // Battle Scene UI Click
        MoveButtonClick,

    }

    /// <summary>
    /// 消息管理器
    /// </summary>
    public class MessageDispatch {

        private static Dictionary<MessageCode, List<MessageHandler>> s_messageHandlers 
            = new Dictionary<MessageCode, List<MessageHandler>>();

        public static void AddListener(MessageCode msgCode, MessageHandler handler) {
            Debug.Log("监听消息" + msgCode.ToString()+handler.ToString());
            if (!s_messageHandlers.ContainsKey(msgCode)) {
                s_messageHandlers.Add(msgCode, new List<MessageHandler>());
            }
            s_messageHandlers[msgCode].Add(handler);
        }

        public static void RemoveListener(MessageCode msgCode, MessageHandler handler) {
            if (s_messageHandlers.ContainsKey(msgCode)) {
                if (s_messageHandlers[msgCode].Contains(handler)) {
                    s_messageHandlers[msgCode].Remove(handler);
                }
            }
        }

        public static void SendMessage(object sender, Message msg) {
            Debug.Log("Trigger 消息" + sender.ToString()+msg.ToString());
            if (s_messageHandlers.ContainsKey(msg.messageID)) {
                foreach (var handler in s_messageHandlers[msg.messageID]) {
                    handler(sender, msg);
                }
            }
        }

    }
}
