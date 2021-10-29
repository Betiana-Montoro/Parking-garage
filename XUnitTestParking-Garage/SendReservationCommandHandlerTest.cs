using Moq;
using Parking_garage.Application.ReservationQueue;
using Parking_garage.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MediatR;
using Parking_garage.Model;
using System.Threading;

namespace XUnitTestParking_Garage
{
    public class SendReservationCommandHandlerTest
    {
        private SendReservationCommandHandler _sut;
        private Mock<IAddReservationQueue> _addReservationQueue;

        [Fact]
        public async Task GivenReservationObject_WhenSendReservation_ThenItShouldBeSentToQueue()
        {
            //Arrange
            _addReservationQueue = new Mock<IAddReservationQueue>();//mock to our interface

            var request = new SendReservationCommand(It.IsAny<Reservation>());
            var cancellationToken = new CancellationToken();

            _sut = new SendReservationCommandHandler(_addReservationQueue.Object);

            //Act
             await _sut.Handle(request, cancellationToken);

            //Assert
            _addReservationQueue.Verify(x => x.SendReservation(request.Reservation), Times.Once); //Should be sent to queue
        }
    }
}
