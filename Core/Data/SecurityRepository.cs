namespace Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using Telerik.Windows.Data;

    public class SecurityRepository<T> where T : SecurityRecordWithoutINotifyPropertyChanged, new()
    {
        private readonly char[] alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();

        private readonly int numberOfRecords;

        public SecurityRepository(int numberOfRecords)
        {
            this.numberOfRecords = numberOfRecords;
            this.PrepareRecords();
        }

        public DataTable DataTable { get; private set; }

        public List<T> List { get; private set; }

        public T[] Array { get; private set; }

        public ObservableCollection<T> ObservableCollection { get; private set; }

        public RadObservableCollection<T> RadObservableCollection { get; private set; }

        public QueryableCollectionView QueryableCollectionView { get; private set; }

        public IEnumerable[] DataSources()
        {
            return new IEnumerable[] { this.List, this.Array, this.ObservableCollection, this.RadObservableCollection, this.DataTable.AsDataView(), this.QueryableCollectionView };
        }

        private void PrepareRecords()
        {
            this.List = new List<T>();
            this.ObservableCollection = new ObservableCollection<T>();
            this.RadObservableCollection = new RadObservableCollection<T>();

            this.DataTable = new DataTable();
            this.DataTable.Columns.Add(new DataColumn("Ticker", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("Close", typeof(double)));
            this.DataTable.Columns.Add(new DataColumn("Open", typeof(double)));
            this.DataTable.Columns.Add(new DataColumn("High", typeof(double)));
            this.DataTable.Columns.Add(new DataColumn("Low", typeof(double)));

            var i = 0;
            var rng = new Random();
            foreach (var ticker in this.GenerateNextTicker())
            {
                var newRecord = new T()
                {
                    Ticker = ticker,
                    Name = string.Format("{0} {1}", i, ticker),
                    Close = rng.NextDouble(),
                    Open = rng.NextDouble(),
                    Low = rng.NextDouble(),
                    High = rng.NextDouble()
                };

                this.List.Add(newRecord);
                this.ObservableCollection.Add(newRecord);
                this.RadObservableCollection.Add(newRecord);

                var newRow = this.DataTable.NewRow();
                newRow["Ticker"] = newRecord.Ticker;
                newRow["Name"] = newRecord.Name;
                newRow["Close"] = newRecord.Close;
                newRow["Open"] = newRecord.Open;
                newRow["Low"] = newRecord.Low;
                newRow["High"] = newRecord.High;

                this.DataTable.Rows.Add(newRow);

                if (i++ >= this.numberOfRecords)
                {
                    break;
                }
            }

            this.Array = this.List.ToArray();
            this.QueryableCollectionView = new QueryableCollectionView(this.List);
        }

        private IEnumerable<string> GenerateNextTicker()
        {
            return from l1 in this.alphabet from l2 in this.alphabet from l3 in this.alphabet from l4 in this.alphabet from l5 in this.alphabet select l1.ToString(CultureInfo.InvariantCulture) + l2 + l3 + l4 + l5;
        }
    }
}