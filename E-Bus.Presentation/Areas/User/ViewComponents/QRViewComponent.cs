using IronQr;
using IronSoftware.Drawing;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs.ReservationDTO;

namespace E_Bus.Presentation.Areas.User.ViewComponents
{
    [ViewComponent]
    public class QRViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ReservationResponse userTripWrapperModel)
        {
            string ticketDetails = $"{userTripWrapperModel.TheUserDTO.FullName}/{userTripWrapperModel.TheUserDTO.NationalID}/{userTripWrapperModel.TheUserDTO.PhoneNumber}" +
                $"/{userTripWrapperModel.TheUserDTO.Email}/{userTripWrapperModel.TheTripResponse.Id}/Departure : {userTripWrapperModel.TheTripResponse.Starting}" +
                $"/Arrival : {userTripWrapperModel.TheTripResponse.Ending}";
            QrCode myQr = QrWriter.Write(ticketDetails);
            // Save QR Code as a Bitmap
            string qrImage = myQr.ToHtmlTag();
            // Save QR Code Bitmap as File
            //qrImage.SaveAs("qrCode.png");

            return View(model: qrImage);
        }
    }
}
