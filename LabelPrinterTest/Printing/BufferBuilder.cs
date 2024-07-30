using System;

namespace LabelPrinterTest.Printing {
    public struct BufferBuilder<T> {
        private readonly T[] _buffer;

        public int CurrentLength => _currentLength;
        private int _currentLength;

        public int MaxLength => _buffer.Length;

        public BufferBuilder() : this(Array.Empty<T>()) {
        }

        public BufferBuilder(T[] buffer) {
            _buffer = buffer;
            _currentLength = 0;
        }

        public void Add(T val) {
            _buffer[_currentLength++] = val;
        }

        public ReadOnlyMemory<T> AsReadOnlyMemory()
            => new ReadOnlyMemory<T>(_buffer, 0, _currentLength);
    }
}
