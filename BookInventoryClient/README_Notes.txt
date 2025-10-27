C:\Node\GitPort\dev422_fall_2025
dotnet new grpc -o BookInventoryService 

C:\Node\GitPort\dev422_fall_2025
dotnet new console -o BookInventoryClient

in what directory C:\Node\GitPort\dev422_fall_2025\BookInventoryClient or C:\Node\GitPort\dev422_fall_2025
seem I need to delete the subdirectory created?????????? yes
------------------------------------------------------------------

did I  add in the good place  in .csproj 
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

<ItemGroup>
  <Protobuf Include="../BookInventoryService/Protos/book_service.proto" GrpcServices="Client" />
</ItemGroup>
</Project>

C:\Node\GitPort\dev422_fall_2025\BookInventoryClient
dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools

C:\Node\GitPort\dev422_fall_2025\BookInventoryClient
dotnet new sln -n BookInventorySolution

dotnet sln BookInventorySolution.sln add BookInventoryService/BookInventoryService.csproj

dotnet sln BookInventorySolution.sln add BookInventoryClient/BookInventoryClient.csproj

----------------------------------------
C:\Node\GitPort\dev422_fall_2025
λ dotnet new grpc -o BookInventoryService
The template "ASP.NET Core gRPC Service" was created successfully.

Processing post-creation actions...
Restoring C:\Node\GitPort\dev422_fall_2025\BookInventoryService\BookInventoryService.csproj:
Restore succeeded.

C:\Node\GitPort\dev422_fall_2025
λ dotnet sln BookInventorySolution.sln add BookInventoryService/BookInventoryService.csproj
Project `BookInventoryService\BookInventoryService.csproj` added to the solution.

- Created the gRPC server project (BookInventoryService)
- Created the gRPC client project (BookInventoryClient)
- Created a solution file (BookInventorySolution.sln)
- Added the server project to the solution

- adding Client Project:
dotnet sln BookInventorySolution.sln add BookInventoryClient/BookInventoryClient.csproj


C:\Node\GitPort\dev422_fall_2025\
├── BookInventorySolution.sln
├── BookInventoryService\
│   └── BookInventoryService.csproj
└── BookInventoryClient\
    └── BookInventoryClient.csproj

--------------------------------------
HTTP instead of HTTPS

var channel = GrpcChannel.ForAddress("http://localhost:5112", new GrpcChannelOptions
{
    HttpHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    }
});
--------------------------------------