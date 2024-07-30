using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace LabelPrinterTest.Printing {
    public class LabelPrinter {
        private readonly ILogger _logger;
        private readonly RasterPrinter _rasterPrinter;

        public LabelPrinter(ILoggerFactory loggerFactory) {
            _logger = loggerFactory.CreateLogger<LabelPrinter>();
            _rasterPrinter = new(PaperSizes.Rafook40x30, loggerFactory);
        }

        public async Task Print(PrintJobInfo jobInfo, IEnumerable<FileInfo> data) {
            _logger.LogDebug("printing labels ({jobInfo})", jobInfo);
            List<IDisposable> disposables = new();
            try {
                int cnt = 0;
                await _rasterPrinter.Print(
                    jobInfo,
                    data.Select(d => {
                        cnt++;
                        _logger.LogTrace("printing {data}", d);
                        return Print(d, disposables.Add);
                    })
                );
                _logger.LogDebug("printed {dataCnt} labels ({jobInfo})", cnt, jobInfo);
            } finally {
                foreach(IDisposable disposable in disposables) {
                    disposable.Dispose();
                }
            }
        }

        private SKImage Print(FileInfo data, Action<IDisposable> addDisposable) {
            SKImage img = SKImage.FromEncodedData(data.FullName);
            addDisposable(img);

            SKImage ret = img.ToRasterImage(true);
            addDisposable(ret);

            return ret;
        }
    }
}
