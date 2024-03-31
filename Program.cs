//////****Versão utilizando IsCancellationRequested****//////

//Cancellation Token é criado com tempo de cancelamento de 10 segundos
//Se o método Cancel não for chamado manualmente, a operação é cancelada automaticamente após 10 segundos
/*CancellationTokenSource cancellationTokenSource = new(10000);
var token = cancellationTokenSource.Token;
var task = ExecutarOperacaoAssincrona(token); // A operação assincrona é iniciada de forma assincrona
Console.ReadLine(); // Aguarda o usuário pressionar enter para cancelar a operação 
if(!token.IsCancellationRequested) // Se a operação já não foi cancelada por tempo, prossegue com o cancelamento manual
{
    cancellationTokenSource.Cancel();
    Console.ReadLine();
}

async Task ExecutarOperacaoAssincrona(CancellationToken cancellationToken)
{
    for(int i=0; i<100; i++)
    {
        Console.WriteLine(i);
        await Task.Delay(1000);
        if(cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Operação cancelada");
            return;
        }
    }
}*/


//////****Versão utilizando ThrowIfCancellationRequested****//////

CancellationTokenSource cancellationTokenSource = new(10000);
var token = cancellationTokenSource.Token;
var task = ExecutarOperacaoAssincrona(token); // A operação assincrona é iniciada de forma assincrona
Console.ReadLine(); // Aguarda o usuário pressionar enter para cancelar a operação 
if(!token.IsCancellationRequested) // Se a operação já não foi cancelada por tempo, prossegue com o cancelamento manual
    cancellationTokenSource.Cancel();

try{
    await task;
}
catch(OperationCanceledException)
{
    Console.WriteLine("Operação cancelada");
}

async Task ExecutarOperacaoAssincrona(CancellationToken cancellationToken)
{
    for(int i=0; i<100; i++)
    {
        Console.WriteLine(i);
        await Task.Delay(1000);
        cancellationToken.ThrowIfCancellationRequested();
    }
}