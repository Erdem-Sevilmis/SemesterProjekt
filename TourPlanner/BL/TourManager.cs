using DAL;
using DAL.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Net;
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
using DAL.Models;
//using TourPlanner.Views;
using System.Collections;
using static log4net.Appender.RollingFileAppender;
using Document = iText.Layout.Document;
using Image = iText.Layout.Element.Image;
using Table = iText.Layout.Element.Table;

namespace BL
{
    public class TourManager
    {
        

        public DataManager _dataManager;
        public TourManager(DataManager dataManager)
        {
            _dataManager = dataManager;
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
                    tourLogs.AddCell(tourlog.TotalTime.ToString());
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

        public void EditTourLogToDatabase(int oldTourId, string Comment, string DateAndTime, string Difficulty, string TotalTime, string Rating)
        {
            DateTimeOffset dateTimeOffset;
            if (DateTimeOffset.TryParse(DateAndTime, out dateTimeOffset))
            {
                // Parsing successful
                //Console.WriteLine(dateTimeOffset);
            }
            else
            {
                // Parsing failed
                dateTimeOffset = DateTimeOffset.UtcNow;
            }
            TimeSpan timeSpan;
            if (TimeSpan.TryParse(TotalTime, out timeSpan))
            {
                // Parsing successful
                //Console.WriteLine(timeSpan);
            }
            else
            {
                // Parsing failed
                timeSpan = TimeSpan.Zero;
            }
            TourLog tourLogToEdit = _dataManager.GetTourLogById(oldTourId);
            tourLogToEdit.Comment = Comment;
            tourLogToEdit.DateAndTime = dateTimeOffset;
            tourLogToEdit.Difficulty = Difficulty;
            tourLogToEdit.TotalTime = timeSpan;
            tourLogToEdit.Rating = int.Parse(Rating);
            _dataManager.DbSaveChanges();


            _dataManager.GetAllToursFromDatabase();
        }


        


        public void AddTourToDatabaseLogic(string Name, string From, string To, string TranportType, TimeSpan tourTime, double tourDistance, int imageId)
        {
            var newTour = new Tour
            {
                Name = Name,
                From = From,
                To = To,
                TransportType = TranportType,
                Time = tourTime,
                Distance = tourDistance,
                ImageId = imageId
            };

            _dataManager.AddNewTourToDb(newTour);

            _dataManager.GetAllToursFromDatabase();
        }


        public void AddTourLogToDatabase(string Comment, string DateAndTime, string Difficulty, string TotalTime, string Rating, int currentSelectedTourId, List<TourLog> listOfCurrentTourLogs)
        {

            //DateTime Example: "2023-07-08";

            DateTimeOffset dateTime;
            if (DateTimeOffset.TryParse(DateAndTime, out dateTime))
            {

            }
            else
            {
                dateTime = DateTimeOffset.UtcNow;
            }
            TimeSpan timeSpan;
            if (TimeSpan.TryParse(TotalTime, out timeSpan))
            {
                // Parsing successful
                //Console.WriteLine(timeSpan);
            }
            else
            {
                // Parsing failed
                timeSpan = TimeSpan.Zero;
            }
            var newTourLog = new TourLog
            {
                Comment = Comment,
                DateAndTime = dateTime,
                Difficulty = Difficulty,
                TotalTime = timeSpan,
                Rating = int.Parse(Rating)
            };

            Tour tourToEdit = _dataManager.GetTourById(currentSelectedTourId);
            tourToEdit.TourLogs = new List<TourLog>();
            tourToEdit.TourLogs = listOfCurrentTourLogs;
            tourToEdit.TourLogs.Add(newTourLog);
            _dataManager.DbSaveChanges();

            _dataManager.GetAllToursFromDatabase();
        }

        public void EditTourToDatabaseLogic(int oldTourId, string Name, string From, string To, string TranportType, double tourDistance, TimeSpan tourTime, int imageId)
        {
            Tour tourToEdit = _dataManager.GetTourById(oldTourId);
            tourToEdit.Name = Name;
            tourToEdit.From = From;
            tourToEdit.To = To;
            tourToEdit.TransportType = TranportType;
            tourToEdit.Distance = tourDistance;
            tourToEdit.Time = tourTime;
            tourToEdit.ImageId = imageId;
            _dataManager.DbSaveChanges();

            _dataManager.GetAllToursFromDatabase();
        }


            public int GetImageId()
        {
            string currentFolderPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Images";

            if (!Directory.Exists(currentFolderPath))
            {
                throw new DirectoryNotFoundException($"Folder '{currentFolderPath}' does not exist.");
            }

            string[] fileNames = Directory.GetFiles(currentFolderPath);

            int variable = 1;
            bool isDifferent = false;
            while (!isDifferent)
            {
                isDifferent = true;
                foreach (string fileName in fileNames)
                {
                    if (int.TryParse(Path.GetFileNameWithoutExtension(fileName), out int parsedValue))
                    {
                        if (variable == parsedValue)
                        {
                            variable++;
                            isDifferent = false;
                            break;
                        }
                    }
                }
            }

            return variable;
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


    }
}