namespace Angel.Model
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 开发者信息
        /// </summary>
        public string msgDev { get; set; }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static MessageModel<T> Success(string msg) {
            return Message(0, msg, default);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Success(string msg, T data)
        {
            return Message(0, msg, data);
        }

        public static MessageModel<List<T>> Success(string msg, List<T> data)
        {
            return MessageList(0, msg, data);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(string msg)
        {
            return Message(0, msg, default);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Fail(string msg, T data)
        {
            return Message(0, msg, data);
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="code">失败/成功</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static MessageModel<T> Message(int code, string msg, T data )
        {
            return new MessageModel<T>() { msg = msg, data = data, code = code };
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MessageModel<List<T>> MessageList(int code, string msg, List<T> data)
        {
            return new MessageModel<List<T>>() { msg = msg, data = data, code = code };
        }

    }

    public class MessageModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public object data { get; set; }


    }
}
