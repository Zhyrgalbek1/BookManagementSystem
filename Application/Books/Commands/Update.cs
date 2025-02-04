﻿using Application.Shared;
using Domain.Repositories;
using MediatR;

namespace Application.Books.Commands
{
    public record UpdateBookResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    public record UpdateBookCommand : IRequest<UpdateBookResponse?>
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }

    internal class UpdateBookHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateBookResponse?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByTitle(request.Title).ConfigureAwait(false);
            if (book is not null)
            {
                book.Title = request.Title;
                book.Description = request.Description;
                book.Price = request.Price;

                _bookRepository.Update(book);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);

                var response = new UpdateBookResponse
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
