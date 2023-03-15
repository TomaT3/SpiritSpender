namespace SpiritSpenderServer.NC_Communication;

using System;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Options;

public interface ISerialCommunication
{
    event Action<string>? MessageReceived;
    public Task StartAsync();
    void Write(string text);
}

public class SerialCommunication : ISerialCommunication
{
    private SerialPort _serialPort;

    public SerialCommunication(IOptions<SpiritSpenderServer.Config.SerialCommunicationConfig> serialCommunicationsOptions)
    {
        var options = serialCommunicationsOptions.Value;
        var port = @$"{options.Port}";
        var baudrate = options.BaudRate;
        _serialPort = new SerialPort(port, baudrate);
        _serialPort.ReadTimeout = options.ReadTimeout;
        _serialPort.WriteTimeout = options.WriteTimeout;
        
    }

    public event Action<string>? MessageReceived;

    public Task StartAsync()
    {
        _serialPort.Open();
        Task.Run(() => StartListenToSerialPort());
        return Task.CompletedTask;
    }

    private void StartListenToSerialPort()
    {
        while (true)
        {
            try
            {
                string message = _serialPort.ReadLine();
                MessageReceived?.Invoke(message);
                Console.WriteLine($"Received: {message}");
            }
            catch (TimeoutException timeoutException)
            {
                Console.WriteLine(timeoutException);
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
