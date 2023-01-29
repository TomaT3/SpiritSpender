namespace SpiritSpenderServer.NC_Communication;

using System;
using System.IO.Ports;

public class SerialCommunication
{
    private SerialPort _serialPort;

    public SerialCommunication()
    {
        //var port = @"/dev/ttyS0";
        var port = @"/dev/ttyAMA0";
        var baudrate = 115200;
        _serialPort = new SerialPort(port, baudrate);
        _serialPort.ReadTimeout = System.IO.Ports.SerialPort.InfiniteTimeout;
        _serialPort.WriteTimeout = 1500;

    }

    public void OpenPort()
    {
        _serialPort.Open();
    }

    public void StartListenToSerialPort()
    {
        while (true)
        {
            try
            {
                string message = _serialPort.ReadLine();
                Console.WriteLine($"Received: {message}");
            }
            catch (TimeoutException timeoutException)
            {
                //Console.WriteLine(timeoutException);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public void Write(string text)
    {
        try
        {
            _serialPort.Write(text);
            Console.WriteLine($"Write:  {text}");
        }
        catch (TimeoutException) { }
    }
}
