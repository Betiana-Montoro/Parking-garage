using FluentValidation;
using MediatR;
using Moq;
using Shouldly;
using Parking_garage.Application.Commands;
using Parking_garage.Application.Queries;
using Parking_garage.Application.ReservationQueue;
using Parking_garage.Model;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestParking_Garage
{
    public class FinishReservationCommandHandlerTest
    {
        private FinishReservationCommandHandler _sut;
        private Mock<IFinishReservationQueue> _finishReservationQueue;
        private Mock<IMediator> _mediator;

        [Fact]
        public async Task GivenReservationId_WhenFinishReservation_ThenItShouldBeRetrievedAndSentToQueue()
        {
            //Arrange
            _finishReservationQueue = new Mock<IFinishReservationQueue>();//mock to our interface
            _mediator = new Mock<IMediator>();

            var reservation = new Reservation(); // new object
            var request = new FinishReservationCommand(It.IsAny<Guid>());
            var cancellationToken = new CancellationToken();

            _sut = new FinishReservationCommandHandler(_finishReservationQueue.Object, _mediator.Object);

            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);//to get reservation
            
            //Act
            reservation = await _sut.Handle(request, cancellationToken);
           
            //Assert
            
            _finishReservationQueue.Verify(x => x.SendFinishReservationMessage(reservation), Times.Once); //Should be sent to queue
            _mediator.Verify(x => x.Send(It.IsAny<GetReservationByIDQuery>(),It.IsAny<CancellationToken>()) ,Times.Once); //Should be retrieved
        }
        [Fact]
        public async Task GivenReservationTimeLessThan3Hours_WhenFinishReservation_ThenCostShouldBe3Dolars()
        {
            //Arrange
            _finishReservationQueue = new Mock<IFinishReservationQueue>();
            _mediator = new Mock<IMediator>();

            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(1) };
            var request = new FinishReservationCommand(It.IsAny<Guid>());
            var cancellationToken = new CancellationToken();

            _sut = new FinishReservationCommandHandler(_finishReservationQueue.Object, _mediator.Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);

            //Act
            reservation = await _sut.Handle(request, cancellationToken);

            //Assert

            reservation.Cost.ShouldBe(3);
        }

        [Fact]
        public async Task GivenReservationTimeEqualTo5Hours_WhenFinishReservation_ThenCostShouldBe9Dolars()
        {
            //Arrange
            _finishReservationQueue = new Mock<IFinishReservationQueue>();
            _mediator = new Mock<IMediator>();

            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-5) };
            var request = new FinishReservationCommand(It.IsAny<Guid>());
            var cancellationToken = new CancellationToken();

            _sut = new FinishReservationCommandHandler(_finishReservationQueue.Object, _mediator.Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);

            //Act
            reservation = await _sut.Handle(request, cancellationToken);

            //Assert

            reservation.Cost.ShouldBe(9);
        }
        [Fact]
        public async Task GivenReservationTimeEqualTo9Hours_WhenFinishReservation_ThenCostShouldBe57Dolars()
        {
            //Arrange
            _finishReservationQueue = new Mock<IFinishReservationQueue>();
            _mediator = new Mock<IMediator>();

            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-9) };
            var request = new FinishReservationCommand(It.IsAny<Guid>());
            var cancellationToken = new CancellationToken();

            _sut = new FinishReservationCommandHandler(_finishReservationQueue.Object, _mediator.Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);

            //Act
            reservation = await _sut.Handle(request, cancellationToken);

            //Assert

            reservation.Cost.ShouldBe(57);
        }
        [Fact]
        public async Task GivenReservationTimeEqualTo2Days_WhenFinishReservation_ThenCostShouldBe114Dolars()
        {
            //Arrange
            _finishReservationQueue = new Mock<IFinishReservationQueue>();
            _mediator = new Mock<IMediator>();

            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-47) };
            var request = new FinishReservationCommand(It.IsAny<Guid>());
            var cancellationToken = new CancellationToken();

            _sut = new FinishReservationCommandHandler(_finishReservationQueue.Object, _mediator.Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);

            //Act
            reservation = await _sut.Handle(request, cancellationToken);

            //Assert

            reservation.Cost.ShouldBe(114);
        }
        [Fact]
        public async Task GivenReservationTimeEqualTo5Days_WhenFinishReservation_ThenCostShouldBe285Dolars()
        {
            //Arrange
            _finishReservationQueue = new Mock<IFinishReservationQueue>();
            _mediator = new Mock<IMediator>();

            var reservation = new Reservation { CheckIn = DateTime.Now.AddHours(-120) };
            var request = new FinishReservationCommand(It.IsAny<Guid>());
            var cancellationToken = new CancellationToken();

            _sut = new FinishReservationCommandHandler(_finishReservationQueue.Object, _mediator.Object);
            _mediator.Setup(x => x.Send(It.IsAny<GetReservationByIDQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(reservation);

            //Act
            reservation = await _sut.Handle(request, cancellationToken);

            //Assert

            reservation.Cost.ShouldBe(342);
        }
    }
}
