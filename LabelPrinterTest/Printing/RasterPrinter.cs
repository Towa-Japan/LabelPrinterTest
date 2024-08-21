using System;
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

            TimeSpan waitTime = TimeSpan.FromMilliseconds(1);
            TimeSpan maxWaitTime = TimeSpan.FromSeconds(10);
            while(!await IsPrinterReady(printClient, jobInfo)) {
                _logger.LogDebug("Printer not ready, waiting {waitTime}", waitTime);
                await Task.Delay(waitTime);
                waitTime = waitTime * 2 < maxWaitTime ? waitTime * 2 : maxWaitTime;
            }

            RasterPrintJobBuilder printDocument = new(_paperSize, _loggerFactory);

            ValueTask<long> writeTask = printDocument.WriteData(data);

            using Stream document = printDocument.Document;

            PrintJobRequest printReq = InitializeRequest<PrintJobRequest>(jobInfo);
            printReq.Document = document;
            printReq.NewJobAttributes = new() {
                JobName = jobInfo.JobName,
                Copies = jobInfo.Copies,
                PrintScaling = PrintScaling.AutoFit
            };
            printReq.DocumentAttributes = new() {
                DocumentFormat = "application/octet-stream",
            };

            _logger.LogTrace("sending print request: {request}", printReq);

            PrintJobResponse printResp = await printClient.PrintJobAsync(printReq);

            _logger.LogDebug("print document bytes: {bytesSent}, response: {printResp}", await writeTask, printResp.StatusCode);
        }

        private async Task<bool> IsPrinterReady(ISharpIppClient printClient, PrintJobInfo jobInfo) {
            GetPrinterAttributesRequest req = InitializeRequest<GetPrinterAttributesRequest>(jobInfo);
            req.RequestedAttributes = [ "printer-state", "printer-is-accepting-jobs", "queued-job-count" ];

            GetPrinterAttributesResponse resp = await printClient.GetPrinterAttributesAsync(req);

            _logger.LogTrace("Printer Status (accepting jobs = {acceptingJobs}): {response}", resp.PrinterIsAcceptingJobs, resp);

            return resp.PrinterIsAcceptingJobs ?? false;
        }

        private static T InitializeRequest<T>(PrintJobInfo jobInfo) where T : IIppRequest, new() {
            return new() {
                PrinterUri = jobInfo.PrinterUri,
                Version = IppVersion.V1_1,
                RequestId = jobInfo.JobId,
                RequestingUserName = jobInfo.User
            };
        }
    }
}
