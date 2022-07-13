using System.Text.Json;
using System.Text.Json.Serialization;
using Banks;
class program 
{      
    static async Task Main()
    {
        var task = Task.Run(()=> 
        {
            
            Thread.Sleep(5000);
            Console.WriteLine("Acordou");
            return 42;
        });
        var result = await task;
        Console.WriteLine("Pronto");
        Console.WriteLine(result);
        Console.ReadLine();

    }
}     


namespace Banks
{
    public class FileLogger : ILogger
    {
        private readonly string file;

        public FileLogger (string file)
        {
            this.file = file;
        }
        public void Log(string message)
        {
            File.AppendAllText(file,$"{message}{Environment.NewLine}");
        }
    }
    public interface ILogger{
        void Log(string message){
            Console.WriteLine($"LOGGER:{message}");
        }
    }
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            
        }
    }
    public class BankAccont{
        private readonly ILogger logger;

        public string Name{get; private set;}
        public decimal Balance { 
            get; private set;
        }  
        public string branch;

        [JsonConstructor]
        public BankAccont(string name, decimal balance): this(name,balance, new ConsoleLogger())
        {

        }
        public BankAccont(string name, decimal balance,ILogger logger){
            if(string.IsNullOrWhiteSpace(name)){
                throw new ArgumentException("Nome inválido", nameof(name));
            }
            if(balance < 0){
                throw new Exception("Saldo não pode ser negativo.");
            }
            Name = name;
            this.Balance = balance;
            this.logger = logger;
        }
        public void deposito(decimal valor){
            if(valor <=0){
                logger.Log($"Não é possível depositar{valor} na conta de {Name}");
                throw new Exception("Deposito inválido! Tente novamente");
            }
            Balance+=valor;
        }
    }   
}