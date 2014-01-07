namespace TelerikLightRadGridViewTester
{
    using System;
    using Core;
    using Core.Data;
    using NUnit.Framework;

    [TestFixture]
    public class TelerikLightRadGridViewFixtureBase<T> : BenchmarkFixtureBase<T> where T : SecurityRecordWithoutINotifyPropertyChanged, new()
    {
        [Test]
        [STAThread]
        public void Benchmark()
        {
            var start = DateTime.Now;

            var telerikLightRadGridViewWindow = new TelerikLightRadGridViewWindow();
            telerikLightRadGridViewWindow.Show();

            var end = DateTime.Now;
            Console.WriteLine("Window loading time: {0}", end - start);

            this.Tester((itemsSource) => telerikLightRadGridViewWindow.GridWithData.ItemsSource = itemsSource, () => telerikLightRadGridViewWindow.GridWithData.ItemsSource = null);
        }

        [Test]
        [STAThread]
        public void BenchmarkWithPreload()
        {
            var telerikLightRadGridViewWindow = new TelerikLightRadGridViewWindow();
            telerikLightRadGridViewWindow.Show();
            telerikLightRadGridViewWindow.Close();

            var start = DateTime.Now;

            telerikLightRadGridViewWindow = new TelerikLightRadGridViewWindow();
            telerikLightRadGridViewWindow.Show();

            var end = DateTime.Now;
            Console.WriteLine("Window loading time: {0}", end - start);

            this.Tester((itemsSource) => telerikLightRadGridViewWindow.GridWithData.ItemsSource = itemsSource, () => telerikLightRadGridViewWindow.GridWithData.ItemsSource = null);
        }

    }
}