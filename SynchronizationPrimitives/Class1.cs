namespace SynchronizationPrimitives
{
    public class Example
    {
        //lock: оператор lock используется для обеспечения блокировки доступа к критической секции кода,
        //когда один поток входит в блокировку, другие потоки ожидают, пока блокировка не будет освобождена.
        private readonly object _lockObject = new object();

        public void SomeMethod()
        {
            lock (_lockObject)
            {
                // Критическая секция кода
            }
        }


        /// <summary>
        /// Monitor: класс Monitor предоставляет механизм блокировки, аналогичный lock, 
        /// но с большей гибкостью. Вы можете использовать Monitor.Enter, Monitor.Exit, 
        /// Monitor.Wait, Monitor.Pulse и Monitor.PulseAll для управления блокировками и сигналами между потоками.
        /// </summary>
        private readonly object _monitorObject = new object();

        public void SomeMethod()
        {
            Monitor.Enter(_monitorObject);
            try
            {
                // Критическая секция кода
            }
            finally
            {
                Monitor.Exit(_monitorObject);
            }
        }

        /// <summary>
        /// Mutex: Mutex (сокращение от "взаимное исключение") является примитивом синхронизации, 
        /// который обеспечивает взаимоисключение доступа к ресурсу между несколькими потоками или процессами.
        /// </summary>

        private static Mutex _mutex = new Mutex();

        public void SomeMethod()
        {
            _mutex.WaitOne();
            try
            {
                // Критическая секция кода
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        //Semaphore: Semaphore используется для ограничения числа потоков,
        //которые могут получить доступ к ограниченному ресурсу одновременно.
        //Семафор имеет счетчик, который увеличивается и уменьшается в зависимости от доступности ресурса.
        private static Semaphore _semaphore = new(initialCount: 2, maximumCount: 2);

        public void SomeMethod()
        {
            _semaphore.WaitOne();
            try
            {
                // Критическая секция кода
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// ReaderWriterLockSlim: Этот механизм блокировки позволяет множеству потоков безопасно 
        /// читать данные в разделяемой области памяти, но ограничивает доступ при записи только одному потоку.
        /// </summary>

        private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        public void ReadData()
        {
            _rwLock.EnterReadLock();
            try
            {
                // Чтение данных
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Barrier: Barrier позволяет нескольким потокам выполняться параллельно 
        /// и синхронизировать их выполнение на определенных точках, чтобы один поток не опережал другие.
        /// </summary>
        private static Barrier _barrier = new Barrier(participantCount: 3);

        public void SomeMethod()
        {
            // Код, выполняемый параллельно

            _barrier.SignalAndWait(); // Точка синхронизации

            // Код, выполняемый после синхронизации
        }



        //ManualResetEvent и AutoResetEvent: Эти примитивы синхронизации 
        //позволяют одному или нескольким потокам ожидать сигнала от другого потока для продолжения выполнения.

        private static ManualResetEvent _manualResetEvent = new ManualResetEvent(initialState: false);
        private static AutoResetEvent _autoResetEvent = new AutoResetEvent(initialState: false);

        public void WaitForSignal()
        {
            _manualResetEvent.WaitOne(); // или _autoResetEvent.WaitOne();
        }

        public void SendSignal()
        {
            _manualResetEvent.Set(); // или _autoResetEvent.Set();
        }
    }
}