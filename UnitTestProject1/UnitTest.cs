using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioController;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest
    {
        List<ClientAverageCost> list = new List<ClientAverageCost>();

        [TestMethod]
        public void LeaderBoardTest()
        {
            var s = new StockXDBController();
            Client client = new Client {AccountCode="TEST12345", InitialCapital=1000000};
            List<Client> clients = new List<Client>();
            clients.Add(client);
            List<Leaderboard> list = LeaderBoardManager.GetLeaders(clients);
            Assert.IsTrue(list[0].Capital == 1000000);
            Assert.IsTrue(list[0].PnL == -0.02427);
        }

        [TestMethod]
        public void PortfolioTest()
        {
            var s = new StockXDBController();
            Client client = new Client { AccountCode = "TEST12345", Name = "test1" };
            List<MatchedOrder> matchedOrders = s.GetMatchedOrdersByAccountCode(client.AccountCode, DateTime.Now);
            s.UpdatePortfolio(matchedOrders);

            List<Portfolio> testPortfolio = s.GetPortfolioRecordsByAccountcode(client.AccountCode);
            Assert.IsTrue(testPortfolio.Count == 1);
            Assert.IsTrue(testPortfolio[0].StockCode == "TEST");
            Assert.IsTrue(testPortfolio[0].Shares == 19000);
        }

        [TestMethod]
        public void StockXTextFilesCheck()
        {
            string poscost = System.Configuration.ConfigurationManager.AppSettings["txtFilesPath"].ToString() + "poscost.txt";
            string cash = System.Configuration.ConfigurationManager.AppSettings["txtFilesPath"].ToString() + "cash.txt";
            string soa = System.Configuration.ConfigurationManager.AppSettings["txtFilesPath"].ToString() + "soa.txt";
            string clients = System.Configuration.ConfigurationManager.AppSettings["txtFilesPath"].ToString() + "clients.txt";
            string pledge = System.Configuration.ConfigurationManager.AppSettings["txtFilesPath"].ToString() + "pledge.txt";

            string[] poscostTxt = File.ReadAllLines(poscost);
            string[] cashTxt = File.ReadAllLines(cash);
            string[] soaTxt = File.ReadAllLines(soa);
            string[] clientsTxt = File.ReadAllLines(clients);
            string[] pledgeTxt = File.ReadAllLines(pledge);

            bool poscostCheck = true;
            bool cashCheck = true;
            bool soaCheck = true;
            bool clientsCheck = true;
            bool pledgeCheck = true;

            for (int i = 0; i < poscostTxt.Length; i++)
            {
                if (i >= poscostTxt.Length - 2)
                {
                    poscostCheck = (poscostTxt[i] == "");
                }
                else
                {
                    string[] poscostLine = poscostTxt[i].Split(';');
                    foreach (string item in poscostLine)
                    {
                        poscostCheck = (item != "");
                    }
                }
            }

            for (int i = 0; i < cashTxt.Length; i++)
            {
                if (i >= cashTxt.Length - 2)
                {
                    cashCheck = (cashTxt[i] == "");
                }
                else
                {
                    string[] cashLine = cashTxt[i].Split(';');
                    foreach (string item in cashLine)
                    {
                        cashCheck = (item != "");
                    }
                }
            }

            for (int i = 0; i < soaTxt.Length; i++)
            {
                if (i >= soaTxt.Length - 2)
                {
                    soaCheck = (soaTxt[i] == "");
                }
                else
                {
                    string[] soaLine = soaTxt[i].Split(';');
                    foreach (string item in soaLine)
                    {
                        soaCheck = (item != "");
                    }
                }
            }

            for (int i = 0; i < clientsTxt.Length; i++)
            {
                if (i >= clientsTxt.Length - 2)
                {
                    clientsCheck = (clientsTxt[i] == "");
                }
                else
                {
                    string[] clientsLine = clientsTxt[i].Split(';');
                    foreach (string item in clientsLine)
                    {
                        clientsCheck = (item != "");
                    }
                }
            }

            for (int i = 0; i < pledgeTxt.Length; i++)
            {
                if (i >= pledgeTxt.Length - 2)
                {
                    pledgeCheck = (pledgeTxt[i] == "");
                }
                else
                {
                    string[] pledgeLine = pledgeTxt[i].Split(';');
                    foreach (string item in pledgeLine)
                    {
                        pledgeCheck = (item != "");
                    }
                }
            }

            Assert.IsTrue(poscostCheck);
            Assert.IsTrue(clientsCheck);
            Assert.IsTrue(soaCheck);
            Assert.IsTrue(pledgeCheck);
            Assert.IsTrue(cashCheck);
        }

        [TestMethod]
        public void PortfolioZeroOutTest()
        {
            var s = new StockXDBController();
            Client client = new Client { AccountCode = "TEST12345", Name = "test1" };

            MatchedOrder sellAllStocks = new MatchedOrder
            {
                UserId = "test1",
                Side = "S",
                Quantity = 19000,
                Price = 20.0M,
                OrderDatetime = DateTime.Now.AddDays(-1),
                MatchDate = DateTime.Now.AddDays(-1).ToShortDateString(),
                BoardLot = "TESTZERO",
                AccountCode = client.AccountCode,
                StockCode = "TEST"
            };

            List<MatchedOrder> addToTable = new List<MatchedOrder>();
            addToTable.Add(sellAllStocks);

            s.SaveNewListToDB(addToTable);

            List<MatchedOrder> matchedOrders = s.GetMatchedOrdersByAccountCode(client.AccountCode, DateTime.Now);

            s.UpdatePortfolio(matchedOrders);

            List<Portfolio> testPortfolio = s.GetPortfolioRecordsByAccountcode(client.AccountCode);
            Assert.IsTrue(testPortfolio.Count == 0);
            //Assert.IsTrue(testPortfolio[0].StockCode == "TEST");
            //Assert.IsTrue(testPortfolio[0].Shares == 19000);

            s.RemoveFromMatchedOrdersTable(sellAllStocks);
        }

        

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
        public void CalculateCashTest()
        {
            var s = new StockXDBController();
            var date = new DateTime(2015, 07, 15, 23, 59, 59);
            var todayList = s.GetAllRecordsByDate(date);
            //foreach (var today in todayList)
            //{
            //    Console.WriteLine(today.AccountCode);
            //}
            var cash = s.CalculateCash("TEST12346", "7/15/2015", todayList);
         
            Assert.IsTrue(Math.Round(cash.Amount,2) == 991650.00M);

            //cash = s.CalculateCash("TEST12345", "7/15/2015", todayList);
            //Assert.IsTrue(Math.Round(cash.Amount, 2) == 1511930.00M);
            //991650

        }
        [TestMethod]
        public void PoscostFileTest()
        {
            var s = new StockXDBController();
            var date = new DateTime(2016,3,14);
            s.GeneratePosCost(date);
            
            string poscostFilePath = System.Configuration.ConfigurationManager.AppSettings["txtFilesPath"].ToString();

            Assert.IsTrue(File.Exists(poscostFilePath + "poscost.txt"));

            var lines = File.ReadAllLines(poscostFilePath + "poscost.txt");
            //Assert.IsTrue(lines.Length == 2);

        }

        [TestMethod]
        public void TestDBTwoSellBuy()
        {

        }

        [TestMethod]
        public void TestDBTwoBuySell()
        {
            var s = new StockXDBController();
            DateTime baseDate = new DateTime(2015, 7, 15,23,0,0);
            List<MatchedOrder> list = s.GetPreviousMatchedOrdersByAccountCodeAndStockCode("TEST12345", "TEST", baseDate);
           // list.Reverse();
            
            List<ClientAverageCost> clientList = s.TransformMatchedOrderToClientAverageCostList(list);
            s.CalculateAvgCost(list,clientList);
            Assert.IsTrue(Math.Round(clientList[0].AverageCost.Value, 4) == 1.20M);
            Assert.IsTrue(clientList[1].AverageCost.Value == 0);
            Assert.IsTrue(clientList[0].NetVolume == 5000);
            Assert.IsTrue(clientList[1].NetVolume == 0);
        }

        [TestMethod]
        public void TestDBTwoBuySell2()
        {
            var s = new StockXDBController();
            DateTime baseDate = new DateTime(2015, 7, 15, 23, 0, 0);
            List<MatchedOrder> list = s.GetPreviousMatchedOrdersByAccountCodeAndStockCode("TEST12346", "TEST", baseDate);
            //list.Reverse();

            List<ClientAverageCost> clientList = s.TransformMatchedOrderToClientAverageCostList(list);
            s.CalculateAvgCost(list,clientList);
            Assert.IsTrue(Math.Round(clientList[0].AverageCost.Value, 4) == 1.45M);
            Assert.IsTrue(clientList[1].AverageCost.Value == 1.45m);
            Assert.IsTrue(clientList[0].NetVolume == 19000);
            Assert.IsTrue(clientList[1].NetVolume == 14000);
        }

        [TestMethod]
        public void TestDBFirstSell()
        {
            var s = new StockXDBController();
            DateTime baseDate = new DateTime(2015, 7, 1);
            List<MatchedOrder> list = s.GetMatchedOrdersByAccountCodeAndStockCodeByDate("TEST12345", "TEST", baseDate);
            //list.Reverse();
            s.HandleLastRecord("TEST12345", "TEST", baseDate,list);
            
            List<ClientAverageCost> clientList = s.TransformMatchedOrderToClientAverageCostList(list);
            s.CalculateAvgCost(list,clientList);
            Assert.IsTrue(Math.Round(clientList[0].AverageCost.Value, 4) == 1.2M);
            Assert.IsTrue(clientList[1].AverageCost.Value == 0);
            Assert.IsTrue(clientList[0].NetVolume == 5000);
            Assert.IsTrue(clientList[1].NetVolume == 0);
        }

        [TestMethod]
        public void TestDBFirstBuy()
        {
            var s = new StockXDBController();
            DateTime baseDate = new DateTime(2015, 6, 30);
            List<MatchedOrder> list = s.GetPreviousMatchedOrdersByAccountCodeAndStockCode("TEST12345", "TEST", baseDate);
            //list.Reverse();

            List<ClientAverageCost> clientList = s.TransformMatchedOrderToClientAverageCostList(list);
            s.CalculateAvgCost(list,clientList);
            Assert.IsTrue(Math.Round(clientList[0].AverageCost.Value, 4) == 1.20M);
            Assert.IsTrue(clientList[1].AverageCost.Value == 0);
            Assert.IsTrue(clientList[0].NetVolume == 5000);
            Assert.IsTrue(clientList[1].NetVolume == 0);
        }


        [TestMethod]
        public void TestDBFirstNotEqualsLastRecordAndBuySellCurrentDay()
        {
            //todo: temp code for DateTime.Now
            var s = new StockXDBController();
            var lastRecord = s.GetLastRecord("TEST12347", "TEST", DateTime.Now);



            MatchedOrder current = new MatchedOrder()
            {
                AccountCode = "TEST12347",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 5,
                OrderDatetime = new DateTime(2025, 6, 1, 6, 0, 0),
                Side = "BUY",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/01/2025",
                Price = 8.0M,
                Quantity = 2000
            };
            List<MatchedOrder> currentList = new List<MatchedOrder>();
            currentList.Add(current);
            current = new MatchedOrder()
            {
                AccountCode = "TEST12347",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 6,
                OrderDatetime = new DateTime(2025, 6, 1, 10, 0, 0),
                Side = "SELL",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/01/2025",
                Price = 25.0M,
                Quantity = 3000
            };
            currentList.Add(current);

            if (lastRecord != null)
            {
                currentList.Reverse();
                currentList.Add(lastRecord);
                currentList.Reverse();
                //perform a first buy operation
                //create an empty record
            }
            //currentList = currentList.OrderBy(o => o.OrderDatetime).ToList();

            //s.CheckFirstAndLastRecord(currentList);
            List<ClientAverageCost> cList = s.TransformMatchedOrderToClientAverageCostList(currentList);
            s.CalculateAvgCost(currentList,cList);
            Assert.IsTrue(cList[1].SumOfNetPrice == 25500M);
            Assert.IsTrue(cList[1].AverageCost == 5.1M);
            Assert.IsTrue(cList[2].SumOfNetPrice == 10200M);
            Assert.IsTrue(cList[2].AverageCost == 5.1M);

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
                BuyPrice = 6000.00M,
                SellPrice = 6000.00M,
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
                BuyPrice = 6000.00M,
                SellPrice = 6000.00M,
                NetVolume = 5000,
                SumOfNetPrice = 6000.00M,
                AverageCost = 1.20M
            };

            list.Add(c);

            c = new ClientAverageCost()
            {
                Date = "07/01/2015",
                Side = "SELL",
                Cost = 1.28M,
                Volume = 5000,
                BuyPrice = 6400.00M,
                SellPrice = 6400.0M,
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
            Assert.IsTrue(Math.Round(list[0].AverageCost.Value, 4) == 1.20M);
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
                BuyPrice = 6000.00M,
                SellPrice = 6000.00M,
                NetVolume = 5000,
                SumOfNetPrice = 6000.00M,
                AverageCost = 1.20M
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


        [TestMethod]
        public void TestFirstNotEqualsLastRecordAndSellCurrentDay()
        {
            MatchedOrder first = new MatchedOrder()
            {
                AccountCode = "TEST12345",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 1,
                OrderDatetime = new DateTime(2015, 6, 1),
                Side = "BUY",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/01/2015",
                Price = 2.5M,
                Quantity = 1000,
                NetPrice = 2500M,
                SumOfNetPrice = 2500M,
                NetVolume = 1000,
                AvgCost = 2.5M
            };
            MatchedOrder last = new MatchedOrder()
            {
                AccountCode = "TEST12345",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 1,
                OrderDatetime = new DateTime(2015, 6, 2),
                Side = "BUY",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/02/2015",
                Price = 3.0M,
                Quantity = 1000,
                NetPrice = 3000M,
                SumOfNetPrice = 5500M,
                NetVolume = 2000,
                AvgCost = 2.75M
            };
            List<MatchedOrder> firstAndLast = new List<MatchedOrder>();
            firstAndLast.Add(first);
            firstAndLast.Add(last);

            MatchedOrder current = new MatchedOrder()
            {
                AccountCode = "TEST12345",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 1,
                OrderDatetime = new DateTime(2015, 6, 5),
                Side = "SELL",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/05/2015",
                Price = 5.0M,
                Quantity = 1000
            };
            List<MatchedOrder> currentQuery = new List<MatchedOrder>();
            currentQuery.Add(current);

            StockXDBController sControl = new StockXDBController();

            if (firstAndLast[0] != firstAndLast[1])
            {
                currentQuery.Add(firstAndLast[1]);
                currentQuery.Reverse();
            }

            var list = sControl.TransformMatchedOrderToClientAverageCostList(currentQuery);
            sControl.CalculateAvgCost(list);
            Assert.IsTrue(list[1].AverageCost == 2.75M);
            Assert.IsTrue(list[1].SumOfNetPrice == 2750M);
            Assert.IsTrue(list[1].NetVolume == 1000);

        }

        [TestMethod]
        public void TestFirstEqualsLastRecord()
        {
            MatchedOrder first = new MatchedOrder()
            {
                AccountCode = "TEST12345",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 1,
                OrderDatetime = new DateTime(2015,6,1),
                Side = "BUY",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/01/2015",
                Price = 2.5M,
                Quantity = 1000
            };
            List<MatchedOrder> firstAndLast = new List<MatchedOrder>();
            firstAndLast.Add(first);
            firstAndLast.Add(first);

            List<MatchedOrder> currentQuery = new List<MatchedOrder>();
            currentQuery.Add(first);

            StockXDBController sControl = new StockXDBController();

            if (firstAndLast[0] != firstAndLast[1])
            {
                currentQuery.Add(firstAndLast[1]);
            }

            var list = sControl.TransformMatchedOrderToClientAverageCostList(currentQuery);
            sControl.CalculateAvgCost(list);
            Assert.IsTrue(list[0].AverageCost == 2.5M);
            Assert.IsTrue(list[0].SumOfNetPrice == 2500M);
            Assert.IsTrue(list[0].NetVolume == 1000);

        }

        public string ConfigurationManager { get; set; }
    }
}
