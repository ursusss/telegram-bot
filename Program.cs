using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

var client = new TelegramBotClient(GetTelegramToken());
client.StartReceiving(Update, Error);

String GetTelegramToken()
{
    try {
        // Production build scenario:
        System.IO.StreamReader file = new System.IO.StreamReader("/run/secrets/token");
        return file.ReadLine();
    } catch (Exception e) {
        // Development build scenario, Win32 compatible
        System.IO.StreamReader file = new System.IO.StreamReader("token.txt");
        return file.ReadLine();
    }
}

async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
{
    var message = update.Message;
    

    
    if (message.Text != null)
    {
        Console.WriteLine($"{message.Chat.FirstName} | {message.Text}");

        switch (message.Text.ToLower())
        {
            case "/start":
                ReplyKeyboardMarkup key = new(new[]
{
    new KeyboardButton[] { "Скинь грустного Пепе", "Скинь весёлого Пепе", "Не скидывай Пепе" },
})
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Скинуть Пепе?", replyMarkup: key);
                break;
            case "скинь грустного пепе":
                await botClient.SendTextMessageAsync(message.Chat.Id, "Держи грустного Пепе:");
                await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri("https://getwallpapers.com/wallpaper/full/0/e/b/1283690-best-pepe-wallpaper-1920x1440.jpg"));
                break;
            case "скинь весёлого пепе":
                await botClient.SendTextMessageAsync(message.Chat.Id, "Держи весёлого Пепе:");
                await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri("https://papik.pro/grafic/uploads/posts/2023-04/1682758752_papik-pro-p-pepe-smaili-tvich-png-2.jpg"));
                break;
            case "не скидывай пепе":
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ладно, не буду");
                break;
        }
       
    }
    if (message.Photo != null)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, "Картинка супер");
        return;
    }
 }

Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
{
    Console.WriteLine($"{exception.ToString()}");
    throw new NotImplementedException();
}

while (true) {
    System.Threading.Thread.Sleep(750);
}
