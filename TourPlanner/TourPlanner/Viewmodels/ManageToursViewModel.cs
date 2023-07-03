using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Views;

namespace TourPlanner.Viewmodels
{
    public class ManageToursViewModel : ViewModelBase
    {

        public ManageToursViewModel()
        {
            //CurrentTours = new List<string>
            //{
            //    "test1",
            //    "test2",
            //    "Test3"
            //};
            GetAllToursFromDatabase();
            AddTourCommand = new Commands.AddTourCommand(this);
        }

        public List<Tour> Tours { get; set; } = new();

        public void GetAllToursFromDatabase()
        {
            Tours = dbContext.Tours.ToList();
        }

        public void OpenCreateTourPopup()
        {
            CreateTourPopupWindow popup = new CreateTourPopupWindow(this);
            popup.ShowDialog();

            //string testOneValue = test;
            //CurrentTours.Add(testOneValue);
            
        }

        public void ExportTourToPdf(Tour tourToExport)
        {
            tourToExport.TourLogs = new List<TourLog>();

            tourToExport.TourLogs.Add(new TourLog
            {
                DateAndTime = new DateTime(2023, 6, 30, 14, 25, 0),
                Comment = "Tour will Start shortly",
                Difficulty = "easy",
                TotalTime = new TimeSpan(0, 0, 0),
                Rating = 5
            });
            tourToExport.TourLogs.Add(new TourLog
            {
                DateAndTime = new DateTime(2023, 6, 30, 15, 31, 0),
                Comment = "have been running for around an hour, still going strong",
                Difficulty = "easy",
                TotalTime = new TimeSpan(1, 0, 0),
                Rating = 5
            });
            tourToExport.TourLogs.Add(new TourLog
            {
                DateAndTime = new DateTime(2023, 6, 30, 16, 52, 0),
                Comment = "Getting tired, should slow down the pace",
                Difficulty = "challenging",
                TotalTime = new TimeSpan(2, 20, 16),
                Rating = 4
            });
            tourToExport.TourLogs.Add(new TourLog
            {
                DateAndTime = new DateTime(2023, 6, 30, 17, 34, 0),
                Comment = "Almost at the finish line, I feel like I'm dying",
                Difficulty = "hard",
                TotalTime = new TimeSpan(3, 23, 51),
                Rating = 3
            });
            tourToExport.TourLogs.Add(new TourLog
            {
                DateAndTime = new DateTime(2023, 6, 30, 17, 34, 0),
                Comment = "Almost at the finish line, I feel like I'm dying",
                Difficulty = "hard",
                TotalTime = new TimeSpan(3, 52, 34),
                Rating = 3
            });
            tourToExport.TourLogs.Add(new TourLog
            {
                DateAndTime = new DateTime(2023, 6, 30, 18, 20, 0),
                Comment = "Finally done, in retrospect this was awesome",
                Difficulty = "hard",
                TotalTime = new TimeSpan(4, 20, 25),
                Rating = 5
            });


            var image = GetImage(new Uri("https://www.mapquestapi.com/staticmap/v5/map?start=" + tourToExport.From.Replace(' ', '+') + "&end=" + tourToExport.To.Replace(' ', '+') + "&size=600,400@2x&key=RWjNFiNXi7QJ5jmjgYM7mjujwDcF3ebG"));
            CreatePDF(image, tourToExport);
            string test = "bin drin";
        }

        private void CreatePDF(byte[] image, Tour pdfTour)
        {
            var writer = new PdfWriter("Results/result.pdf");
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var headerTour = new Paragraph($"Tour from {pdfTour.From} to {pdfTour.To}")
                //.SetFont(PdfFontFactory.CreateFont(StandardFonts.COURIER))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(25)
                .SetUnderline()
                .SetBold()
                .SetMarginBottom(10);

            Image itextImage = new Image(iText.IO.Image.ImageDataFactory.Create(image))
                .SetMaxWidth(525)
                .SetMarginBottom(15)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);


            var headerTourInfo = new Paragraph($"{pdfTour.Name}:")
                //.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetFontSize(16)
                .SetBold();

            var tourInfo = new Table(2)
                .AddCell($"Description: {pdfTour.Description}\n")
                .AddCell($"From: {pdfTour.From}\n")
                .AddCell($"To: {pdfTour.To}\n")
                .AddCell($"Type: {pdfTour.TransportType}\n")
                .AddCell($"Distance: {(float)pdfTour.Distance}km\n")
                .AddCell($"Time: {pdfTour.Time}\n")
                //.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetFontSize(12)
                .SetMarginBottom(30)
                .SetBorder(Border.NO_BORDER);

            Table tourLogs = new Table(5)
                .AddHeaderCell("Date/Time")
                .AddHeaderCell("Comment")
                .AddHeaderCell("Difficulty")
                .AddHeaderCell("Total Time")
                .AddHeaderCell("Rating")
                .SetFontSize(10);

            if (pdfTour.TourLogs.Any())
            {
                foreach (var tourlog in pdfTour.TourLogs)
                {
                    tourLogs.AddCell(tourlog.DateAndTime.ToString());
                    tourLogs.AddCell(tourlog.Comment);
                    tourLogs.AddCell(tourlog.Difficulty.ToString());
                    tourLogs.AddCell(tourlog.TotalTime.ToString(@"hh\:mm\:ss"));
                    tourLogs.AddCell($"{tourlog.Rating}/5");
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    tourLogs.AddCell("-");
            }

            document.Add(headerTour);
            document.Add(itextImage);
            document.Add(headerTourInfo);
            document.Add(tourInfo);
            document.Add(new Paragraph("Tour Logs:").SetBold().SetFontSize(16));
            document.Add(tourLogs);
            document.Close();
        }

        public byte[] GetImage(Uri uri)
        {
            using (WebClient webClient = new WebClient())
                return webClient.DownloadData(uri);
        }

        public void AddTourToDatabase(string Name, string From, string To, string TranportType)
        {

            var results = GetTimeAndDistance(new Uri("https://www.mapquestapi.com/directions/v2/route?key=RWjNFiNXi7QJ5jmjgYM7mjujwDcF3ebG&from=" + From.Replace(' ', '+') + "&to=" + To.Replace(' ', '+') + "&unit=k"));
            TimeSpan tourTime = results.time;
            double tourDistance = results.distance;

            var newTour = new Tour
            {
                Name = Name,
                From = From,
                To = To,
                TransportType = TranportType,
                Time = tourTime,
                Distance = tourDistance
            };

            dbContext.Tours.Add(newTour);
            dbContext.SaveChanges();

            GetAllToursFromDatabase();
        }

        public (float distance, TimeSpan time) GetTimeAndDistance(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();

                // Parse the JSON response
                JObject responseObj = JObject.Parse(jsonResponse);

                // Extract distance and time values
                float distance = (float)responseObj["route"]["distance"];
                int time = (int)responseObj["route"]["time"];

                return (distance, TimeSpan.FromSeconds(time));
            }
        }

        private List<string> _currentTours;

        public List<string> CurrentTours
        {
            get 
            { 
                return _currentTours;
            }
            set 
            {
                _currentTours = value;
                OnPropertyChanged(nameof(CurrentTours));
            }
        }

        public ICommand AddTourCommand { get; }

        public ICommand DeleteTourCommand { get; }

        public ICommand ModifyTourCommand { get; }
    }
}
