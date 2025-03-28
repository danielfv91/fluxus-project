using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Application.Features.Transactions.ListTransactions;
using Fluxus.Common.Security.Interfaces;
using Fluxus.Domain.Entities;
using Fluxus.UnitTests.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsHandlerTests
    {
        private readonly ITransactionRepository _repository = Substitute.For<ITransactionRepository>();
        private readonly IAuthenticatedUser _authenticatedUser = Substitute.For<IAuthenticatedUser>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();

        private readonly ListTransactionsHandler _handler;

        public ListTransactionsHandlerTests()
        {
            _handler = new ListTransactionsHandler(_repository, _authenticatedUser, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedResult_WhenTransactionsExist()
        {
            // Arrange
            var userId = ListTransactionsHandlerTestData.UserId;
            var transactions = ListTransactionsHandlerTestData.GenerateFakeTransactions();
            var query = ListTransactionsHandlerTestData.GetQuery();

            _authenticatedUser.Id.Returns(userId);
            _repository.GetByPeriodAsync(userId, null, null, Arg.Any<CancellationToken>())
                       .Returns(transactions);

            var expected = transactions.Select(t => new ListTransactionsResult
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = (int)t.Type,
                Description = t.Description,
                Date = t.Date
            }).ToList();

            _mapper.Map<List<ListTransactionsResult>>(transactions)
                   .Returns(expected);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
