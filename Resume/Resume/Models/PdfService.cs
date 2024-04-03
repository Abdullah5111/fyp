//using DinkToPdf;
//using DinkToPdf.Contracts;
//using System.Text;

//namespace Resume.Resume
//{
//    public class PdfService
//    {
//        private readonly IConverter _converter;

//        public PdfService(IConverter converter)
//        {
//            _converter = converter;
//        }
//        public byte[] GeneratePdf(string htmlContent)
//        {
//            var doc = new HtmlToPdfDocument()
//            {
//                Objects = {
//                new ObjectSettings() {
//                    HtmlContent = htmlContent,
//                }
//            }
//            };

//            return _converter.Convert(doc);
//        }
//        public string ConvertModelToHtml(ResumeModel model)
//        {
//            var htmlContent = new StringBuilder();

//            // Build the HTML content based on the model
//            htmlContent.AppendLine("<!DOCTYPE html>");
//            htmlContent.AppendLine("<html lang=\"en\">");
//            htmlContent.AppendLine("<head>");
//            // Add head content...
//            htmlContent.AppendLine("</head>");
//            htmlContent.AppendLine("<body>");

//            // Add other body content based on the model
//            htmlContent.AppendLine($"<h1>{model.FirstName} {model.SecondName}</h1>");
//            htmlContent.AppendLine($"<p>Email: {model.Email}</p>");
//            htmlContent.AppendLine($"<p>Phone Number: {model.PhoneNumber}</p>");
//            htmlContent.AppendLine($"<p>Phone Number: {model.City}</p><br><br>");

//            htmlContent.AppendLine($"<h1>Education</h1>");



//            htmlContent.AppendLine("</body>");
//            htmlContent.AppendLine("</html>");

//            return htmlContent.ToString();
//        }

//    }
//}
