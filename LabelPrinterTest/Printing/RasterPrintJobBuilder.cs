using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace LabelPrinterTest.Printing {
    public class RasterPrintJobBuilder {
        private readonly Pipe _pipe;
        private readonly PaperSize _paperSize;
        private readonly ILogger _logger;

        public Stream Document => _pipe.Reader.AsStream(false);

        public RasterPrintJobBuilder(PaperSize paperSize, ILoggerFactory loggerFactory) {
            _pipe = new();
            _paperSize = paperSize;
            _logger = loggerFactory.CreateLogger<RasterPrintJobBuilder>();
        }

        public async ValueTask<long> WriteData(IEnumerable<SKImage> data) {
            using Stream stream = _pipe.Writer.AsStream(false);

            long bytesWritten = await WriteAsync(stream, Enumerable.Repeat((byte)0x00, 200).ToArray()).ConfigureAwait(false);

            bytesWritten += await WriteAsync(
                stream,
                [
                    0x1b, 0x69, 0x61, 0x01,
                    0x1b, 0x40
                ]
            ).ConfigureAwait(false);

            bool firstPage = true;
            byte[] rasterBuffer = Array.Empty<byte>();
            foreach(SKImage raster in data) {
                int maxBufferSize = (3 + ((raster.Width + 7) >> 3)) * raster.Height;
                if(maxBufferSize > rasterBuffer.Length) {
                    rasterBuffer = new byte[maxBufferSize];
                }

                if(!firstPage) {
                    bytesWritten += await WriteAsync(stream, 0x0c).ConfigureAwait(false);
                } else {
                    bytesWritten += await WriteAsync(
                        stream,
                        [0x1b, 0x69, 0x55, 0x77, 0x01]
                    ).ConfigureAwait(false);

                    bytesWritten += await WriteAsync(
                        stream,
                        _paperSize.Bytes
                    ).ConfigureAwait(false);

                    firstPage = false;
                }

                bytesWritten += await WriteAsync(
                    stream,
                    [
                        0x1b, 0x69, 0x7a, 0x8e, (byte)_paperSize.Type,
                            (byte)_paperSize.Width, (byte)_paperSize.Height,
                            (byte)(raster.Height & 0xff), (byte)((raster.Height >> 8) & 0xff),
                            (byte)((raster.Height >> 16) & 0xff), (byte)((raster.Height >> 24) & 0xff),
                            0x00, 0x00,
                        0x1b, 0x69, 0x64, 0x00, 0x00, //TODO margin for non-Diecut
                        0x4d, 0x00 //TODO compression?
                    ]
                ).ConfigureAwait(false);

                BufferBuilder<byte> buffer = new(rasterBuffer);
                using(SKPixmap pixels = raster.PeekPixels()) {
                    CopyTo(pixels, ref buffer);
                }

                bytesWritten += await WriteAsync(stream, buffer.AsReadOnlyMemory()).ConfigureAwait(false);
            }

            if(firstPage) {
                bytesWritten += await WriteAsync(stream, Enumerable.Repeat((byte)0x00, 200).ToArray()).ConfigureAwait(false);
            } else {
                bytesWritten += await WriteAsync(stream, 0x1a).ConfigureAwait(false);
            }

            await stream.FlushAsync().ConfigureAwait(false);

            return bytesWritten;
        }

        private ValueTask<int> WriteAsync(Stream stream, byte value)
            => WriteAsync(stream, [value]);

        private static ValueTask<int> WriteAsync(Stream stream, byte[] buffer)
            => WriteAsync(stream, (ReadOnlyMemory<byte>)buffer);

        private static async ValueTask<int> WriteAsync(Stream stream, ReadOnlyMemory<byte> buffer) {
            await stream.WriteAsync(buffer.ToArray(), 0, buffer.Length).ConfigureAwait(false);
            return buffer.Length;
        }

        private static void CopyTo(SKPixmap pixels, ref BufferBuilder<byte> data) {
            ReadOnlySpan<byte> pixelSpan = pixels.GetPixelSpan();
            for(int curRow = 0; curRow < pixels.Height; curRow++) {
                ReadOnlySpan<byte> line = pixelSpan.Slice(curRow * pixels.RowBytes, pixels.RowBytes);
                if(line.IndexOf((byte)0x0) < 0) {
                    data.Add(0x5a);
                } else {
                    data.Add(0x67);
                    data.Add(0x00);
                    data.Add((byte)((pixels.RowBytes + 7) >> 3));
                    for(int idx = line.Length - 1; idx >= 0;) {
                        byte curByte = 0;
                        for(int shift = 7; shift >= 0; shift--, idx--) {
                            curByte |= (byte)(((line[idx] ^ 0x1) & 0x1) << shift);
                        }
                        data.Add(curByte);
                    }
                }
            }
        }
    }
}
