using System;
using System.Linq;

namespace DataGridTester
{
    using Core;
    using Core.Data;
    using NUnit.Framework;

    [TestFixture]
    public class DataGridFixtureBase<T> : BenchmarkFixtureBase<T> where T : SecurityRecordWithoutINotifyPropertyChanged, new()
    {
        [Test]
        [STAThread]
        public void Benchmark()
        {
            var start = DateTime.Now;

            var dataGridWindow = new DataGridWindow();
            dataGridWindow.Show();

            var end = DateTime.Now;
            Console.WriteLine("Window loading time: {0}", end - start);

            this.Tester((itemsSource) => dataGridWindow.GridWithData.ItemsSource = itemsSource, () => dataGridWindow.GridWithData.ItemsSource = null);
        }

        [Test]
        [STAThread]
        public void BenchmarkWithPreload()
        {
            var dataGridWindow = new DataGridWindow();
            dataGridWindow.Show();
            dataGridWindow.Close();

            var start = DateTime.Now;

            dataGridWindow = new DataGridWindow();
            dataGridWindow.Show();

            var end = DateTime.Now;
            Console.WriteLine("Window loading time: {0}", end - start);

            this.Tester((itemsSource) => dataGridWindow.GridWithData.ItemsSource = itemsSource, () => dataGridWindow.GridWithData.ItemsSource = null);
        }

    }
}