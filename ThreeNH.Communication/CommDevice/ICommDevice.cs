using System;
using System.IO;

namespace ThreeNH.Communication.CommDevice
{
    /// <summary>
    /// 接收到的数据类型
    /// </summary>
    public enum DataType
    {
        Bytes,  // 字节数据流
        Eof,    // EOF，通常指示对方已关闭写连接
        Error,  // 错误，此时设置ex
    }

    /// <summary>
    /// 数据接收消息
    /// </summary>
    /// <param name="sender">发送都</param>
    /// <param name="dataType">接收到的数据类型，Bytes表示字节数据
    /// Eof通常指示对方已关闭写连接
    /// Error，说明出错，会设置ex参数</param>
    /// <param name="ex">当bytesReceived为null是指示异常信息</param>
    public delegate void DataReceivedEventHandler(ICommDevice sender, DataType dataType, Exception ex);


    public interface ICommDevice : IDisposable
    {
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="device">要打开的设备的标识；如果为null打开默认设备</param>
        void Open(object device=null);
        /// <summary>
        /// 开闭设备
        /// </summary>
        void Close();

        /// <summary>
        /// 通讯设备名称
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// 通讯设备是否已打开
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// 接收缓冲区的数据
        /// </summary>
        int BytesToRead { get; }

        /// <summary>
        /// 获取发送缓冲区中的字节数
        /// </summary>
        int BytesToWrite { get; }

        /// <summary>
        /// 丢弃输入缓冲区
        /// </summary>
        void DiscardInBuffer();

        /// <summary>
        /// 丢弃输出缓冲区中的内容
        /// </summary>
        void DiscardOutBuffer();

        /// <summary>
        /// 获取或设置读取操作未完成时发生超时之前的毫秒数。
        ///  默认值为 InfiniteTimeout。
        /// </summary>
        int ReadTimeout { get; set; }

        /// <summary>
        /// 获取或设置写入操作未完成时发生超时之前的毫秒数。
        ///  默认值为 InfiniteTimeout。
        /// </summary>
        int WriteTimeout { get; set; }
        
        /// <summary>
        /// 从设备输入缓冲区读取一些字节并将那些字节写入字节数组中指定的偏移量处。
        /// </summary>
        /// <param name="buffer">将输入写入到其中的字节数组</param>
        /// <param name="offset">缓冲区数组中开始写入的偏移量</param>
        /// <param name="count">要读取的字节数</param>
        /// <returns>读取的字节数</returns>
        int Read(byte[] buffer, int offset, int count);

        /// <summary>
        /// 向设备中写入指定字节数
        /// </summary>
        /// <param name="buffer">包含要写入端口的数据的字节数组</param>
        /// <param name="offset">参数中从零开始的字节偏移量，从此处开始将字节复制到设备</param>
        /// <param name="count">要写入的字节数</param>
        void Write(byte[] buffer, int offset, int count);

            /// <summary>
        /// 接收数据消息
        /// </summary>
        event DataReceivedEventHandler DataReceived;
    }
}
