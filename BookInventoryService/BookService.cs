using System.Collections.Concurrent;
using System.Threading.Tasks;
using Grpc.Core;
using BookInventory;

namespace BookInventoryService.Services
{
    public class BookServiceImpl : BookService.BookServiceBase
    {
        private static readonly ConcurrentDictionary<string, Book> _books = new();

        public override Task<AddBookResponse> AddBook(AddBookRequest request, ServerCallContext context)
        {
            var book = new Book
            {
                Id = request.Id,
                Title = request.Title,
                Author = request.Author,
                Year = request.Year
            };

            _books[book.Id] = book;
            return Task.FromResult(new AddBookResponse { Success = true });
        }

        public override Task<GetBookResponse> GetBook(BookId request, ServerCallContext context)
        {
            if (_books.TryGetValue(request.Id, out var book))
            {
                return Task.FromResult(new GetBookResponse { Book = book });
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }
        }

        public override Task<Empty> UpdateBook(Book request, ServerCallContext context)
        {
            if (!_books.ContainsKey(request.Id))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }

            _books[request.Id] = request;
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteBook(BookId request, ServerCallContext context)
        {
            if (!_books.TryRemove(request.Id, out _))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));
            }

            return Task.FromResult(new Empty());
        }

        public override async Task ListBooks(Empty request, IServerStreamWriter<Book> responseStream, ServerCallContext context)
        {
            foreach (var book in _books.Values)
            {
                await responseStream.WriteAsync(book);
            }
        }
    }
}
