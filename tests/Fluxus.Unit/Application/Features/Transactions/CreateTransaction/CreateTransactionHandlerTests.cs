using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Application.Domain.UnitOfWork;
using Fluxus.Application.Features.Transactions.CreateTransaction;
using Fluxus.Application.Features.Transactions.Events;
using Fluxus.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Fluxus.Unit.Application.TestData;
using MediatR;

namespace Fluxus.UnitTests.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionHandlerTests
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateTransactionHandler _handler;

        public CreateTransactionHandlerTests()
        {
            _repository = Substitute.For<ITransactionRepository>();
            _mapper = Substitute.For<IMapper>();
            _publisher = Substitute.For<IPublisher>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _handler = new CreateTransactionHandler(_repository, _mapper, _publisher, _unitOfWork);
        }

        [Fact]
        public async Task Should_Save_Transaction_When_Command_Is_Valid()
        {
            // Arrange
            var command = CreateTransactionHandlerTestData.GenerateValidCommand();
            var transaction = CreateTransactionHandlerTestData.GenerateExpectedTransaction(command);
            var resultDto = new CreateTransactionResult { Id = transaction.Id };

            _mapper.Map<Transaction>(command).Returns(transaction);
            _repository.AddAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>())
                .Returns(callInfo => Task.FromResult(callInfo.ArgAt<Transaction>(0)));
            _mapper.Map<CreateTransactionResult>(transaction).Returns(resultDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(transaction.Id);

            await _repository.Received(1).AddAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>());
            await _publisher.Received(1).Publish(Arg.Any<TransactionCreatedEvent>(), Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
