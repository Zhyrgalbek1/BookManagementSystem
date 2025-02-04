﻿using Domain.Repositories;
using MediatR;

namespace Application.Books.Requests
{
    public record GetBookResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
    public record GetBookByTitleRequest : IRequest<GetBooksResponse> { public required string Title { get; init; } }
    internal class GetBookByTitle : IRequestHandler<GetBookByTitleRequest, GetBooksResponse?>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByTitle(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<GetBooksResponse?> Handle(GetBookByTitleRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByTitle(request.Title);
            if (book is not null)
            {
                var response = new GetBooksResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                };
                return response;
            }
            return default;
        }
    }
}
