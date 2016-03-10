﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioController;
using System.Data.SqlClient;

namespace StockXChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new StockXDBController();
            var lastList = s.GetFirstAndLastRecord("TEST12347", "TEST");

            

            MatchedOrder current = new MatchedOrder()
            {
                AccountCode = "TEST12347",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 5,
                OrderDatetime = new DateTime(2025, 6, 1,6,0,0),
                Side = "BUY",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/01/2025",
                Price = 8.0M,
                Quantity = 2000
            };
            var currentList = new List<MatchedOrder>();
            currentList.Add(current);
            current = new MatchedOrder()
            {
                AccountCode = "TEST12347",
                UserId = "TESTACCOUNT",
                MatchedOrderID = 6,
                OrderDatetime = new DateTime(2025, 6, 1,10,0,0),
                Side = "SELL",
                StockCode = "TEST",
                BoardLot = "MAIN",
                MatchDate = "06/01/2025",
                Price = 25.0M,
                Quantity = 3000
            };
            currentList.Add(current);
            
            if (lastList.Count != 0)
            {
                currentList.Add(lastList[0]);
                //perform a first buy operation
                //create an empty record
            }
            currentList = currentList.OrderBy(o => o.OrderDatetime).ToList();
            
            //s.CheckFirstAndLastRecord(currentList);
            List<ClientAverageCost> cList = s.TransformMatchedOrder(currentList);
            s.CalculateAvgCost(cList);

            foreach (var c in cList)
            {
                Console.WriteLine("Date: {0}", c.Date);
                Console.WriteLine("Side: ", c.Side);
                Console.WriteLine("AvgCost: {0}", c.AverageCost);
                Console.WriteLine("SumPrice: {0}", c.SumOfNetPrice);
                Console.WriteLine("Netvolume: {0}", c.NetVolume);
                Console.WriteLine("BuyPrice: {0}", c.BuyPrice);
                Console.WriteLine("--");
            }
            Console.ReadKey();
        }
        static void Main6(string[] args)
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
            currentQuery.Add(first);

            StockXDBController sControl = new StockXDBController();

            if (firstAndLast[0] != firstAndLast[1])
            {
                currentQuery.Add(firstAndLast[1]);
                currentQuery.Reverse();
            }

            var list = sControl.TransformMatchedOrder(currentQuery);
            sControl.CalculateAvgCost(list);
            Console.WriteLine(list[1].AverageCost);
            Console.WriteLine(list[1].SumOfNetPrice);
            Console.WriteLine(list[1].NetVolume);
            Console.ReadKey();
        }
        static void Main5(string[] args)
        {
            var s = new StockXDBController();
            DateTime baseDate = new DateTime(2015, 7, 15,23,0,0);
            List<MatchedOrder> list = s.GetMatchedOrders("TEST12345", "TEST",baseDate);
            List<MatchedOrder> firstAndLast = s.GetFirstAndLastRecord("TEST12345", "TEST");
            list.Reverse();

            List<ClientAverageCost> clientList = s.TransformMatchedOrder(list);

            Console.WriteLine("MatchedOrder List");
            Console.WriteLine("-----------------------");
            foreach (MatchedOrder item in list)
            {
                Console.WriteLine("Account Code: {0}",item.AccountCode);
                Console.WriteLine("User ID: {0}", item.UserId);
                Console.WriteLine("Side: {0}", item.Side);
                Console.WriteLine("Order Datetime: {0}", item.OrderDatetime);
                Console.WriteLine("Stock Code: {0}", item.StockCode);
                Console.WriteLine("Price: {0}", item.Price);
                Console.WriteLine("Quantity: {0}", item.Quantity);
                Console.WriteLine("Match Date: {0}", item.MatchDate);
                Console.WriteLine("--");
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("ClientAverageCost List");
            Console.WriteLine("-----------------------");
            foreach (ClientAverageCost client in clientList)
            {
                Console.WriteLine("Date: {0}", client.Date);
                Console.WriteLine("Cost: {0}", client.Cost);
                Console.WriteLine("Volume: {0}", client.Volume);
                Console.WriteLine("Side: {0}", client.Side);
                Console.WriteLine("--");
            }

            s.CalculateAvgCost(clientList);
            Console.WriteLine("-----------------------");
            Console.WriteLine("ClientAverageCost AverageCost Calculation");
            Console.WriteLine("-----------------------");
            foreach (ClientAverageCost client in clientList)
            {
                Console.WriteLine("Sum of Net Price: {0}", client.SumOfNetPrice);
                Console.WriteLine("Net Volume: {0}", client.NetVolume);
                Console.WriteLine("Average Cost: {0}", client.AverageCost);
                Console.WriteLine("--");
            }

            Console.ReadKey();
        }
        //static void Main1(string[] args)
        //{
        //   List<ClientAverageCost> list = new List<ClientAverageCost>();


        //   ClientAverageCost c = new ClientAverageCost()
        //   {
        //       Date = "06/30/2015",
        //       Side = "SELL",
        //       Cost = 1.20M,
        //       Volume = 5000,
        //       BuyPrice = 6023.00M,
        //       SellPrice = 5947.00M,
        //       NetVolume = 0,
        //       SumOfNetPrice = 0,
        //       AverageCost = 0
        //   };

        //   list.Add(c);

        //   c = new ClientAverageCost()
        //   {
        //       Date = "07/01/2015",
        //       Side = "BUY",
        //       Cost = 1.32M,
        //       Volume = 19000,
        //       BuyPrice = 0,
        //       SellPrice = 0,
        //       NetVolume = 0,
        //       SumOfNetPrice = 0,
        //       AverageCost = 0
        //   };
        //   list.Add(c);

        //    StockXDBController s = new StockXDBController();
        //    s.CalculateAvgCost(list);
        //    foreach (ClientAverageCost cac in list)
        //    {
        //        Console.WriteLine("Net Price:{5},Volume:{4},Cost:{3}, Sum Net Price:{0},Net Vol:{1}, Avg Cost: {2}", cac.SumOfNetPrice, cac.NetVolume, cac.AverageCost, cac.Cost, cac.Volume,cac.Cost*cac.Volume);
        //    }
        //    //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4));

        //    //Console.WriteLine(Math.Round(list[0].AverageCost.Value, 4) == 1.2046M);
        //    //Console.WriteLine("Net V:{0}",list[0].NetVolume);
        //    Console.ReadKey();
        //}
        //static void Main2(string[] args)
        //{
        //    var sdb = new StockXDBController();
        //    var orders = sdb.GetMatchedOrders();

        //    //sdb.Test();

        //    string connectionString = "Data Source=10.11.18.21;User ID=sa;"
        //                                       + "Password=password-2014;";

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(@"SET NOCOUNT ON ");
        //    sb.Append(@" ; ");
        //    sb.Append(@" IF OBJECT_ID('tempdb.dbo.#TempOrds2') IS NOT NULL   ");
        //    sb.Append(@" DROP TABLE tempdb.dbo.#TempOrds2    ");
        //    sb.Append(@" IF OBJECT_ID('tempdb.dbo.#TempMatchedOrders') IS NOT NULL   ");
        //    sb.Append(@" DROP TABLE  tempdb.dbo.#TempMatchedOrders   ");
        //    sb.Append(@" ;   ");
        //    sb.Append(@" SELECT DISTINCT");
        //    sb.Append(@"     OTWC.username,o.ParentOrdID,o.OrdStatus AS 'OrderStatus',o.TransactTime AS 'OrderDate',");
        //    sb.Append(@"     IsNull(a.AccountCode,'') AS 'WebID',isNull([OT-WebTrading].[dbo].FXfunc_GetCFESide(o.Side),'') AS 'Side',");
        //    sb.Append(@"     isNull([OT-WebTrading].[dbo].FXfunc_GetCFEBoard(SUBSTRING(o.Symbol,1,1)),'') AS 'Board',");
        //    sb.Append(@"     isNull([OT-WebTrading].[dbo].FXfunc_GetCFETerms(o.TimeInForce),'') AS Terms,IsNull(SUBSTRING(o.Symbol,2,10),'') AS 'Security',");
        //    sb.Append(@"     IsNull(o.Price,0) AS 'Price',	IsNull(o.OrderQty,0) AS 'Vol',IsNull(o.LeavesQty,0) AS 'PubVol',");
        //    sb.Append(@"     IsNull(o.CumQty,0) AS 'MatchedVol',IsNull(CONVERT(varchar(8),o.ExpireDate,112),'') AS 'Expiration',");
        //    sb.Append(@"     IsNull(a.AccountCode,'') AS 'AccountID',ISNULL(o.UnfilledAmount,0) AS 'UnfilledAmount'");
        //    sb.Append(@" INTO #TempOrds2");
        //    sb.Append(@" FROM [TEDS-FIX].[TEDS-FIX].dbo.fxOrderSnapshot  o INNER JOIN [TEDS-FIX].[TEDS-FIX].[dbo].fxOrderPartySnapshot p	on (o.parentordid = p.parentordid) ");
        //    sb.Append(@" INNER JOIN [TEDS-FIX].[TEDS-FIX].[dbo].Accounts a on (a.XchgAccountCode = p.ID) INNER JOIN [OT-WebComp].[dbo].tbl_ot_login OTWC on (OTWC.XchgAccountCode = p.ID)");
        //    sb.Append(@" WHERE ordstatus IN ('0','5') AND CONVERT(varchar(8),ExpireDate,112) >= CONVERT(VARCHAR(8),GETDATE(),112)");
        //    sb.Append(@" AND p.[Role] = 24 AND a.OnlineTrading = 1 ");
        //    sb.Append(@" ;   ");
        //    sb.Append(@" SELECT o.username AS 'USERID', o.WebID AS 'ACCOUNTCODE', o.ParentOrdID AS 'PARENTORDERID',");
        //    sb.Append(@"     o.OrderDate AS 'ORDERDATE', o.Side AS 'SIDE','MAIN' as BOARDLOT, o.[Security] AS 'STOCKCODE',");
        //    sb.Append(@"     o.[Price] AS 'PRICE', o.Vol AS 'QUANTITY', o.Terms AS 'TERM'");
        //    sb.Append(@" INTO #TempMatchedOrders");
        //    sb.Append(@" FROM #TempOrds2 o");
        //    sb.Append(@" INNER JOIN [OT-WebQuotes].dbo.QUOTESPH_SECURITIES_REV q on");
        //    sb.Append(@" ((o.[Security] = q.Seccode and o.Price >= q.LastPrice and o.[side] = 'B') or");
        //    sb.Append(@" (o.[Security] = q.Seccode and o.Price <= q.LastPrice and o.[side] = 'S'))");
        //    sb.Append(@" WHERE o.Board = 3");
        //    sb.Append(@" AND q.LastPrice <> 0");
        //    sb.Append(@" UNION ALL");
        //    sb.Append(@" SELECT	o.username AS 'USERID', o.WebID AS 'ACCOUNTCODE', o.ParentOrdID AS 'PARENTORDERID', o.OrderDate AS 'ORDERDATE',");
        //    sb.Append(@" o.Side AS 'SIDE', 'ODDLOT' as BOARDLOT, o.[Security] AS 'STOCKCODE', o.[Price] AS 'PRICE',");
        //    sb.Append(@" o.Vol AS 'QUANTITY'	, o.Terms AS 'TERM'");
        //    sb.Append(@" FROM #TempOrds2 o");
        //    sb.Append(@" INNER JOIN [OT-WebQuotes].dbo.QUOTESPH_SECURITIES_REV_ODDLOT q on");
        //    sb.Append(@" ((o.[Security] = q.Seccode and o.Price >= q.LastPrice and o.[side] = 'B') or");
        //    sb.Append(@" (o.[Security] = q.Seccode and o.Price <= q.LastPrice and o.[side] = 'S'))");
        //    sb.Append(@" WHERE o.Board = 4");
        //    sb.Append(@" AND q.LastPrice <> 0");
        //    sb.Append(@" ;  ");
        //    sb.Append(@" SELECT [USERID] AS 'USER ID',[ACCOUNTCODE] AS 'ACCOUNT CODE',[STOCKCODE] AS 'STOCKCODE',[BOARDLOT] AS BOARDLOT,");
        //    sb.Append(@" CASE WHEN SUM(CASE WHEN [SIDE] = 'B' THEN 1 ELSE 0 END) > 1 THEN ");
        //    sb.Append(@"     SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),QUANTITY) * CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END) /");
        //    sb.Append(@"     SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END)");
        //    sb.Append(@" ELSE SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END)");
        //    sb.Append(@" END");
        //    sb.Append(@" AS 'AVERAGE PRICE(BUY)',");
        //    sb.Append(@" CASE WHEN SUM(CASE WHEN [SIDE] = 'S' THEN 1 ELSE 0 END) > 1 THEN  ");
        //    sb.Append(@" SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),QUANTITY) * CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END) /");
        //    sb.Append(@" SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END)");
        //    sb.Append(@" ELSE");
        //    sb.Append(@" SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END)");
        //    sb.Append(@" END");
        //    sb.Append(@" AS 'AVERAGE PRICE(SELL)',");
        //    sb.Append(@" SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END) -");
        //    sb.Append(@" SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END)	");
        //    sb.Append(@" AS 'NET QUANTITY'");
        //    sb.Append(@" FROM #TempMatchedOrders");
        //    sb.Append(@" GROUP BY [USERID],[ACCOUNTCODE],[STOCKCODE],[BOARDLOT]");
        //    sb.Append(@" ORDER BY [USERID],[ACCOUNTCODE],[STOCKCODE],[BOARDLOT]");
        //    sb.Append(@" ;  ");
        //    sb.Append(@" UPDATE 	[TEDS-FIX].[TEDS-FIX].dbo.fxOrderSnapshot");
        //    sb.Append(@" SET	ExpireDate = GETDATE()");
        //    sb.Append(@" WHERE TimeInForce IN ('G','6') ");
        //    sb.Append(@" AND ParentOrdID");
        //    sb.Append(@" IN (SELECT PARENTORDERID FROM #TempMatchedOrders)");
        //    sb.Append(@" --SELECT CONVERT(VARCHAR,@@ROWCOUNT) AS 'Number of GTD Orders Modified'");


        //    sb = new StringBuilder();

        //    sb.Append(@"select * 	");
        //    sb.Append(@"FROM [TEDS-FIX].[TEDS-FIX].dbo.fxOrderSnapshot  o ");
        //    sb.Append(@"INNER JOIN [TEDS-FIX].[TEDS-FIX].[dbo].fxOrderPartySnapshot p	on (o.parentordid = p.parentordid) ");
        //    sb.Append(@"INNER JOIN [TEDS-FIX].[TEDS-FIX].[dbo].Accounts a on (a.XchgAccountCode = p.ID)    ");
        //    sb.Append(@"INNER JOIN [OT-WebComp].[dbo].tbl_ot_login OTWC on (OTWC.XchgAccountCode = p.ID)");



        //    string qString = sb.ToString();
        //    Console.WriteLine(qString);
        //    using (SqlConnection connection =
        //    new SqlConnection(connectionString))
        //    {

        //        // Create the Command and Parameter objects.
        //        SqlCommand command = new SqlCommand(qString, connection);
        //        Console.WriteLine("Connection succeeded");
        //        //command.Parameters.AddWithValue("@pricePoint", paramValue);

        //        // Open the connection in a try/catch block. 
        //        // Create and execute the DataReader, writing the result
        //        // set to the console window.
        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            if (reader != null)
        //                Console.WriteLine("not null");
        //            while (reader.Read())
        //            {
        //                int fieldCount = reader.FieldCount;
        //                for (int i = 0; i < fieldCount; i++ )
        //                {
        //                    Console.WriteLine(reader.GetName(i) + ": " + reader[i].ToString());
        //                }
        //            }
        //            reader.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }

        //        Summary s = null;
        //        //AverageCostCalculator ac = new AverageCostCalculator("BUY",s);
        //        //Console.WriteLine(ac.AvgCost);

        //        sdb.Test();

        //        Console.ReadLine();
        //    }

        //    //sdb.ProcessOrders(orders);
        //    Console.ReadKey();
        //}
        
    }

}

