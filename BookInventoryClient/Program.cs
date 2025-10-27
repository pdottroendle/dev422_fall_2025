using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Core;
using BookInventory;

class Program
{
    static async Task Main(string[] args)
    {
      //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
        using var channel = GrpcChannel.ForAddress("https://localhost:7068");

        var client = new BookService.BookServiceClient(channel);

        Console.WriteLine("Book Inventory Client");
        while (true)
        {
            Console.WriteLine("\nChoose an operation:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Get Book");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. List Books");
            Console.WriteLine("6. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter ID: ");
                    var id = Console.ReadLine();
                    Console.Write("Enter Title: ");
                    var title = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    var author = Console.ReadLine();
                    Console.Write("Enter Publication Year: ");
                    if (!int.TryParse(Console.ReadLine(), out int year))
                    {
                        Console.WriteLine("Invalid year input.");
                        break;
                    }

                  //  var newBook = new Book { Id = id, Title = title, Author = author, PublicationYear = year };
                  //  await client.AddBookAsync(newBook);
                    var newBookRequest = new AddBookRequest { Id = id, Title = title, Author = author, Year = year };
		    await client.AddBookAsync(newBookRequest);
                    Console.WriteLine("Book added.");
                    break;

                case "2":
                    Console.Write("Enter Book ID: ");
                    var getId = Console.ReadLine();
                    try
                    {
                     //   var book = await client.GetBookAsync(new BookId { Id = getId });
                     //   Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}");
var response = await client.GetBookAsync(new BookId { Id = getId });
var book = response.Book;
Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}");
                    }
                    catch (RpcException ex)
                    {
                        Console.WriteLine($"Error: {ex.Status.Detail}");
                    }
                    break;

                case "3":
                    Console.Write("Enter ID: ");
                    var updateId = Console.ReadLine();
                    Console.Write("Enter New Title: ");
                    var newTitle = Console.ReadLine();
                    Console.Write("Enter New Author: ");
                    var newAuthor = Console.ReadLine();
                    Console.Write("Enter New Year: ");
                    if (!int.TryParse(Console.ReadLine(), out int newYear))
                    {
                        Console.WriteLine("Invalid year input.");
                        break;
                    }

                    var updatedBook = new Book { Id = updateId, Title = newTitle, Author = newAuthor, PublicationYear = newYear };
                    try
                    {
                        await client.UpdateBookAsync(updatedBook);
                        Console.WriteLine("Book updated.");
                    }
                    catch (RpcException ex)
                    {
                        Console.WriteLine($"Error: {ex.Status.Detail}");
                    }
                    break;

                case "4":
                    Console.Write("Enter Book ID to delete: ");
                    var deleteId = Console.ReadLine();
                    try
                    {
                        await client.DeleteBookAsync(new BookId { Id = deleteId });
                        Console.WriteLine("Book deleted.");
                    }
                    catch (RpcException ex)
                    {
                        Console.WriteLine($"Error: {ex.Status.Detail}");
                    }
                    break;

                case "5":
                    var books = client.ListBooks(new Empty());
                    while (await books.ResponseStream.MoveNext())
                    {
                        var book = books.ResponseStream.Current;
                        Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}");
                    }
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
