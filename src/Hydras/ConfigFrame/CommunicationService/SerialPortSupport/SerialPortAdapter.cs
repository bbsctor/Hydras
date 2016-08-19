using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace ConfigFrame.CommunicationService.SerialPortSupport
{
    public class SerialPortAdapter
    {
        private static bool isFirstTime = true;
        private int interval;

        public int Interval
        {
            get { return interval; }
            set 
            {
                //_serialPort.Close();
                interval = value;
                //_serialPort.Open();
            }
        }
        public int SendReadInterval
        {
            get { return interval; }
            set { this.interval = value; }
        }

        private int reSendTimes;

        public int ReSendTimes
        {
            get { return reSendTimes; }
            set { reSendTimes = value; }
        }

        private byte[] head;
        public byte[] Head
        {
            get { return head; }
            set { head = value; }
        }

        private byte[] tail;
        public byte[] Tail
        {
            get { return tail; }
            set { tail = value; }
        }

        public SerialPort _serialPort;

        public SerialPortAdapter()
        {
            _serialPort = new SerialPort();

            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.Even;
            _serialPort.StopBits = StopBits.One;

            _serialPort.ReadBufferSize = 4096;
            this.interval = 4000;
            reSendTimes = 6;
        }

        public void setFrameFormart(byte[] head, byte[] tail)
        {
            this.head = head;
            this.tail = tail;
        }

        public byte[] sendAndReceive(byte[] sendData)
        {
            if (isFirstTime == true)
            {
                interval = 4000;
                isFirstTime = false;
            }
            else
            {
                interval = 200;
            }
            int times = reSendTimes;
            lock (this)
            {
                byte[] temp = null;
                lock (_serialPort)
                {
                    while (times >= 0)
                    {
                        try
                        {
                            _serialPort.DiscardInBuffer();
                            _serialPort.Write(sendData, 0, sendData.Length);
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                        
                        Console.WriteLine("send: " + BitConverter.ToString(sendData));
                        

                        try
                        {
                            if (head == null || tail == null)
                            {
                                temp = readRaw();
                            }
                            else
                            {
                                temp = readFrame();
                            }
                            break;
                        }
                        catch (TimeoutException te)
                        {
                            if (times > 0)
                            {
                                times--;
                                continue;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }

                    Console.WriteLine("recv: " + BitConverter.ToString(temp));
                }

                return temp;
            }
            
        }

        public void writeOnly(byte[] data)
        {
            lock (this)
            {
                lock (_serialPort)
                {
                    try
                    {
                        _serialPort.Write(data, 0, data.Length);

                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    Console.WriteLine("send: " + BitConverter.ToString(data));
                    Console.WriteLine("in write only mode!");
                }
            }
        }

        private byte[] readRaw()
        {

            byte[] buff = new byte[1024];
            int count = 0;
            Thread.Sleep(interval);
            try
            {
                count = _serialPort.Read(buff, 0, buff.Length);
            }
            catch (TimeoutException e)
            {
                throw;
            }
            byte[] result = null;
            if (count > 0)
            {
                result = new byte[count];
                System.Array.Copy(buff, result, count);
            }
            return result;
        }

        private byte[] readFrame()
        {
            int iTemp = -1;
            List<byte> dataRecv = new List<byte>();
            Queue<byte> temp = new Queue<byte>();
            bool waitingTail = false;
            bool stop = false;

            while (stop == false)
            {
                try
                {
                    iTemp = _serialPort.ReadByte();
                    temp.Enqueue((byte)iTemp);
                    if (containArray(temp.ToArray(), head.ToArray()) == true)
                    {
                        dataRecv.AddRange(head);
                        temp.Clear();
                        waitingTail = true;
                    }
                    else if (waitingTail == true && containArray(temp.ToArray(), tail.ToArray()) == true)
                    {
                        dataRecv.AddRange(temp.ToArray());
                        stop = true;
                    }
                }
                catch (TimeoutException e)
                {
                    throw;
                }
            }
            return dataRecv.ToArray();
        }

        private bool containArray(byte[] a, byte[] b)
        {
            if (a != null && b != null && a.Length >= b.Length)
            {
                byte[] temp = new byte[b.Length];
                System.Array.Copy(a, a.Length - b.Length, temp, 0, temp.Length);
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != b[i])
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public void openSerial()
        {
            if (_serialPort.IsOpen == false)
            {
                lock (_serialPort)
                {
                    _serialPort.Open();
                }
            }
                
        }

        public void closeSerial()
        {
            if (_serialPort.IsOpen == true)
            {
                lock (_serialPort)
                {
                    _serialPort.Close();
                }
            }
        }

        public void SetPortBaudRate(int p)
        {
            if (p != _serialPort.BaudRate)
            {
                _serialPort.BaudRate = p;
            } 
        }
        public void SetPortName(string port)
        {
            if (port.Equals(_serialPort.PortName) == false)
            {
                _serialPort.PortName = port;
            }

        }

        public void SetReadTimeOut(int mseconds)
        {
            _serialPort.ReadTimeout = mseconds;
        }

        public void SetWriteTimeOut(int mseconds)
        {
            _serialPort.WriteTimeout = mseconds;
        }

        public void SetTimeOut(int mseconds)
        {
            _serialPort.ReadTimeout = mseconds;
            _serialPort.WriteTimeout = mseconds;
        }
    }
}
