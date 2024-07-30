using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SharpIpp;
using SharpIpp.Models;
using SharpIpp.Protocol.Models;
using SkiaSharp;

namespace LabelPrinterTest.Printing {
    public class RasterPrinter {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly PaperSize _paperSize;

        public RasterPrinter(PaperSize paperSize, ILoggerFactory loggerFactory) {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<RasterPrinter>();
            _paperSize = paperSize;
        }

        public async Task Print(PrintJobInfo jobInfo, IEnumerable<SKImage> data) {
            using HttpClientHandler httpHandler = new() {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };

            using HttpClient httpClient = new(new HttpClientLoggingHandler(httpHandler, _loggerFactory));
            using SharpIppClient printClient = new(httpClient);

            RasterPrintJobBuilder printDocument = new(_paperSize, _loggerFactory);

            ValueTask<long> writeTask = printDocument.WriteData(data);

            using Stream document = printDocument.Document;

            PrintJobRequest printReq = new() {
                PrinterUri = jobInfo.PrinterUri,
                Document = document,
                RequestingUserName = jobInfo.User,
                RequestId = jobInfo.JobId,
                NewJobAttributes = new() {
                    JobName = jobInfo.JobName,
                    Copies = jobInfo.Copies,
                    PrintScaling = PrintScaling.AutoFit
                },
                DocumentAttributes = new() {
                    DocumentFormat = "application/octet-stream",
                }
            };

            _logger.LogTrace("sending print request: {request}", printReq);

            PrintJobResponse printResp = await printClient.PrintJobAsync(printReq);

            _logger.LogDebug("print document bytes: {bytesSent}, response: {printResp}", await writeTask, printResp.StatusCode);
        }
    }
}
