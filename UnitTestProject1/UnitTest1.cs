using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioController;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        List<ClientAverageCost> list = new List<ClientAverageCost>();
        public List<ClientAverageCost> GetList()
        {
            ClientAverageCost c = new ClientAverageCost()
            {
                Date = "06/30/2015",
                Side = "BUY",
                Cost = 1.20M,
                Volume = 5000,
                BuyPrice = 6023.00M,
                SellPrice = 5947.00M,
                NetVolume = 5000,
                SumOfNetPrice=6023.00M,
                AverageCost = 1.2046M
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "07/01/2015",
                Side = "SELL",
                Cost = 1.28M,
                Volume = 5000,
                BuyPrice = 6423.04M,
                SellPrice = 6344.96M,
                NetVolume = 0,
                SumOfNetPrice=0,
                AverageCost = 0
            };
            list.Add(c);
            return list;
        }

        [TestMethod]
        public void TestTwoBuyBuy()
        {
            ClientAverageCost c = new ClientAverageCost()
            {
                Date = "07/02/2015",
                Side = "BUY",
                Cost = 1.32M,
                Volume = 19000,
                BuyPrice = 25080.0000M,
                SellPrice = 25080.0000M,
                NetVolume = 19000,
                SumOfNetPrice = 25080.0000M,
                AverageCost = 1.32M
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "07/15/2015",
                Side = "BUY",
                Cost = 1.42M,
                Volume = 11000,
                BuyPrice = 0,
                SellPrice = 0,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0
            };
            list.Add(c);

            StockXDBController s = new StockXDBController();
            Assert.IsFalse(list.Count < 2 && list.Count > 2);
            list = GetList();
            s.CalculateAvgCost(list);
            //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            //System.Diagnostics.Debug.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            // Trace.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            Assert.IsTrue(Math.Round(list[0].AverageCost.Value, 4) == 1.32M);
            Assert.IsTrue(Math.Round(list[1].AverageCost.Value, 4) == 1.3567M);
            Assert.IsTrue(list[0].NetVolume == 19000);
            Assert.IsTrue(list[1].NetVolume == 30000);
        }

        [TestMethod]
        public void TestTwoSellBuy()
        {
            ClientAverageCost c = new ClientAverageCost()
            {
                Date = "06/30/2015",
                Side = "SELL",
                Cost = 1.20M,
                Volume = 5000,
                BuyPrice = 6023.00M,
                SellPrice = 5947.00M,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "07/01/2015",
                Side = "BUY",
                Cost = 1.32M,
                Volume = 19000,
                BuyPrice = 0,
                SellPrice = 0,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0
            };
            list.Add(c);

            StockXDBController s = new StockXDBController();
            Assert.IsFalse(list.Count < 2 && list.Count > 2);
            list = GetList();
            s.CalculateAvgCost(list);
            //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            //System.Diagnostics.Debug.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            // Trace.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            Assert.IsTrue(Math.Round(list[0].AverageCost.Value, 4) == 0);
            Assert.IsTrue(Math.Round(list[1].AverageCost.Value, 4) == 1.32M);
            Assert.IsTrue(list[0].NetVolume == 0);
            Assert.IsTrue(list[1].NetVolume == 19000);
        }

        [TestMethod]
        public void TestTwoBuySell()
        {
            ClientAverageCost c = new ClientAverageCost()
            {
                Date = "06/30/2015",
                Side = "BUY",
                Cost = 1.20M,
                Volume = 5000,
                BuyPrice = 6023.00M,
                SellPrice = 5947.00M,
                NetVolume = 5000,
                SumOfNetPrice = 6023.00M,
                AverageCost = 1.2046M
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "07/01/2015",
                Side = "SELL",
                Cost = 1.28M,
                Volume = 5000,
                BuyPrice = 6423.04M,
                SellPrice = 6344.96M,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0
            };
            list.Add(c);

            StockXDBController s = new StockXDBController();
            Assert.IsFalse(list.Count < 2 && list.Count > 2);
            list = GetList();
            s.CalculateAvgCost(list);
            //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            //System.Diagnostics.Debug.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            // Trace.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            Assert.IsTrue(Math.Round(list[0].AverageCost.Value, 4) == 1.2046M);
            Assert.IsTrue(list[1].AverageCost.Value == 0);
            Assert.IsTrue(list[0].NetVolume == 5000);
            Assert.IsTrue(list[1].NetVolume == 0);
        }

        [TestMethod]
        public void TestFirstSell()
        {
            ClientAverageCost c = new ClientAverageCost()
            {
                Date = "06/30/2015",
                Side = "SELL",
                Cost = 1.20M,
                Volume = 5000,
                BuyPrice = 6023.00M,
                SellPrice = 5947.00M,
                NetVolume = 5000,
                SumOfNetPrice = 6023.00M,
                AverageCost = 1.2046M
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "",
                Side = "",
                Cost = 0,
                Volume = 0,
                BuyPrice = 0,
                //BuyPrice = 0,
                SellPrice = 0,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0
            };
            list.Add(c);

            StockXDBController s = new StockXDBController();
            Assert.IsFalse(list.Count < 2 && list.Count > 2);
            list = GetList();
            s.CalculateAvgCost(list);
            //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            //System.Diagnostics.Debug.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            // Trace.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            Assert.IsTrue(Math.Round(list[0].AverageCost.Value, 4) == 0);
            Assert.IsTrue(list[1].AverageCost.Value == 0);
            Assert.IsTrue(list[0].NetVolume == 0);
            Assert.IsTrue(list[1].NetVolume == 0);
        }
        
        [TestMethod]
        public void TestFirstBuy()
        {
            ClientAverageCost c = new ClientAverageCost()
            {
                Date = "06/30/2015",
                Side = "BUY",
                Cost = 1.20M,
                Volume = 5000,
                BuyPrice = 0,
                SellPrice = 0,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "",
                Side = "",
                Cost = 0,
                Volume = 0,
                BuyPrice = 0,
                //BuyPrice = 0,
                SellPrice = 0,
                NetVolume = 0,
                SumOfNetPrice = 0,
                AverageCost = 0 
            };
            list.Add(c);

            StockXDBController s = new StockXDBController();
            Assert.IsFalse(list.Count < 2 && list.Count > 2);
            list = GetList();
            s.CalculateAvgCost(list);
            //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            //System.Diagnostics.Debug.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
           // Trace.WriteLine(Math.Round(list[0].AverageCost.Value, 4));
            Assert.IsTrue( Math.Round(list[0].AverageCost.Value,4)== 1.20M );
            Assert.IsTrue(list[1].AverageCost.Value == 0);
            Assert.IsTrue(list[0].NetVolume == 5000);
            Assert.IsTrue(list[1].NetVolume == 0);

        }
    }
}
