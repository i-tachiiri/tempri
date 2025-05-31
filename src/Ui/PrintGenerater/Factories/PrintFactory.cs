namespace PrintGenerater.Factories;
public class PrintFactory(IServiceProvider serviceProvider,IPageService pageService)
{ 
    public async Task<IPrintEntity> CreateInstanceAsync(int printId)
    {
        return printId switch
        {
            100007 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100007>()),
            100008 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100008>()),
            100009 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100009>()),
            100010 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100010>()),
            100011 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100011>()),
            100012 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100012>()),
            100013 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100013>()),
            100014 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100014>()),
            100015 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100015>()),
            100016 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100016>()),
            100017 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100017>()),
            100018 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100018>()),
            100019 => await InitializePrintAsync(serviceProvider.GetRequiredService<Print100019>()),
            _ => throw new ArgumentException("Invalid print ID")
        };
    }

    private async Task<IPrintEntity> InitializePrintAsync(IPrintEntity print)
    {
        return await print.SetPrintAsync();
    }
}
