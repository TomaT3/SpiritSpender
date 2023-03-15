namespace SpiritSpenderServer.NC_Communication;

using System;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Options;

public interface ISerialCommunication
{
    IObservable<string> MessageReceived { get; }
    public Task StartAsync();
    void Write(string text);
}

public class SerialCommunication : ISerialCommunication
{
    private SerialPort _serialPort;
    private BehaviorSubject<string> _messageReceived;

    public SerialCommunication(IOptions<SpiritSpenderServer.Config.SerialCommunicationConfig> serialCommunicationsOptions)
    {
        _messageReceived = new BehaviorSubject<string>(string.Empty);
        var options = serialCommunicationsOptions.Value;
        var port = @$"{options.Port}";
        var baudrate = options.BaudRate;
        _serialPort = new SerialPort(port, baudrate);
        _serialPort.ReadTimeout = options.ReadTimeout;
        _serialPort.WriteTimeout = options.WriteTimeout;
        
    }

    public IObservable<string> MessageReceived => _messageReceived.AsObservable();

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
                _messageReceived.OnNext(message);
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
