using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBP.Data.DTO.Booking;
public sealed class BookingResult
{
    public string ConfirmationMessage { get; }

    public bool Success { get; }

    public BookingResult(
        string confirmationMessage,
        bool success)
    {
        ConfirmationMessage = confirmationMessage;
        Success = success;
    }
}
