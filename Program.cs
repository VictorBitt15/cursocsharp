class program 
    {
        
        static void Main(){
            
            //"Testando".WriteLine(ConsoleColor.Red);
            /*Run((x,y)=> x*y);
            var test = (string name)=>Console.WriteLine($"Olá {name}");
            test("Jonh wick"); 
            Func<decimal> test2 = () => 4.2m;
            Console.WriteLine(test2());
            Func<string, bool> checkName = name => name == "Victor";
            Console.WriteLine(checkName("Luiza"));*/
           var store = new DataStore<int>();
            store.Value=42;
            Console.WriteLine(store.Value);
        }
        
        /*static void Run(Func<int,int,int> calc){
            Console.WriteLine(calc(20,30));
        }
        static int Sum(int a, int b){
            return a+b;
        }*/
        
    }

    class DataStore<T>{
        public T Value{get;set;}
    }
    static class Extensions{
        public static void WriteLine(this string text, ConsoleColor color){
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
class FileLogger : ILogger
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
interface ILogger{
    void Log(string message){
        Console.WriteLine($"LOGGER:{message}");
    }
}
class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        
    }
}
class BankAccont{
    private string name;
    private readonly ILogger logger;

    public decimal Balance { 
        get; private set;
    }  

    public BankAccont(string name, decimal balance, ILogger logger){
        if(string.IsNullOrWhiteSpace(name)){
            throw new ArgumentException("Nome inválido", nameof(name));
        }
        if(balance < 0){
            throw new Exception("Saldo não pode ser negativo.");
        }
        this.name = name;
        this.Balance = balance;
        this.logger = logger;
    }
    public void deposito(decimal valor){
        if(valor <=0){
            logger.Log($"Não é possível depositar{valor} na conta de {name}");
            throw new Exception("Deposito inválido! Tente novamente");
        }
        Balance+=valor;
    }
}