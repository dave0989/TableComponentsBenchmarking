namespace TelerikRadGridViewTester
{
    using System;
    using Core;
    using Core.Data;
    using NUnit.Framework;

    [TestFixture]
    public class TelerikRadGridViewFixtureBase<T> : BenchmarkFixtureBase<T> where T : SecurityRecordWithoutINotifyPropertyChanged, new()
    {
        [Test]
        [STAThread]
        public void Benchmark()
        {
            var start = DateTime.Now;

            var telerikRadGridViewWindow = new TelerikRadGridViewWindow();
            telerikRadGridViewWindow.Show();

            var end = DateTime.Now;
            Console.WriteLine("Window loading time: {0}", end - start);

            this.Tester((itemsSource) => telerikRadGridViewWindow.GridWithData.ItemsSource = itemsSource, () => telerikRadGridViewWindow.GridWithData.ItemsSource = null);
        }

        [Test]
        [STAThread]
        public void BenchmarkWithPreload()
        {
            var telerikRadGridViewWindow = new TelerikRadGridViewWindow();
            telerikRadGridViewWindow.Show();
            telerikRadGridViewWindow.Close();

            var start = DateTime.Now;

            telerikRadGridViewWindow = new TelerikRadGridViewWindow();
            telerikRadGridViewWindow.Show();

            var end = DateTime.Now;
            Console.WriteLine("Window loading time: {0}", end - start);

            this.Tester((itemsSource) => telerikRadGridViewWindow.GridWithData.ItemsSource = itemsSource, () => telerikRadGridViewWindow.GridWithData.ItemsSource = null);
        }

    }
}