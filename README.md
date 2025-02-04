# AkashChat  

**AkashChat** — package for communicating with AI models using Akash 

## 📦 Install  

Install from **NuGet**:  
```sh
 dotnet add package Verone.AkashChat
```

### 🎛 Configuration
You can configurate package with `AkashChatOptions`:

```csharp
var options = new AkashChatOptions
{
    Url = // Base URL for Chat API
    Model = // AI Model (list of availables models: https://chatapi.akash.network/documentation)
    Token = // Token for Authorization (you need to take it from akash.network)
};

builder.Services.AddAkashChat(options);
```

## 🚀 Usage  

Example of usage:  
inject `IAkashChatService` into your class

```csharp
using Verone.AkashChat;

var response = await akashChatService.SendMessage("Hi, how are you?");
Console.WriteLine(response);
``` 

## 📝 License  

[MIT](LICENSE).
