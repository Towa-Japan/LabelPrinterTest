using System;

namespace LabelPrinterTest.Printing {
    public class PrintJobInfo {
        public required int JobId { get; init; }
        public string JobName { get; init; } = "";

        public required Uri PrinterUri { get; init; }


        public string User { get; init; } = Environment.UserName;

        public int Copies { get; init; } = 1;

        public override string ToString() {
            return $"[{GetType().Name}: {JobName}({JobId})]";
        }
    }
}
