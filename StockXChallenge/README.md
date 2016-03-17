# StockXChallenge plan
todo:
1. create client.txt from cash (good)
format:
account, name, address, phone, commission, N, C,5, 11011205464539
OT9521-7;MARCO N. ACCAD;Unit 406 Danube Bldg., Riverfront Residences Caniogan, Pasig 1606 ;(0917)862-8504 631-8734;0.0025;N;C;5;11011205464539

1. create pledge.txt from client.txt
1. Leaderboard:
a. Get All stockx challenger participants. (possible all accounts that is in cash table)
b. then we compute the cash + equity = 
  sum of all stocks owned (get current closing price) * volume per account

  order by new column descending
todo:
1. we change the soa to one liner per account per stock date sorted. done
2. add email capability. 
3. add windows task.
4. move connection string to app config. done
5. back up the sdf. copy and rename to daily filename. done
6. ask techni for a client txt dump. 
7. back up uat server.

todo: (done all)
1. create a unit test for cash calculation. add at leat two stock codes to fully test it.
2. create CashHistory table

todo: (done all)
1. get list of matchedorders for the day
2. filter list per account and per stock
3. Lookup cash for account before loop. 
4. calculate amount (b is minus and s is plus)
5. add to cash
6. add cash to cashlist
7. save cashlist
8. make a backup of db as part of process.

todo:
1. create a unit test for cash calculation. add at leat two stock codes to fully test it.
2. wrong calculation. 
2.a first possible bug.  there is only one matched order for the account.
2.b. the calculation doesn't add or subtract from cash
2.c. inside the loop is getting not match date.  may or may not work:
                    List<Cash> accountCashList = cashList.Where(o => o.AccountCode == matchedOrder.AccountCode && o.LastUpdatedDate != matchedOrder.MatchDate).ToList();
					at least remove from loop so only one call.
2.d. make this return only cash and not list.  do the list.add outside of the function.
        private List<Cash> CalculateCash(List<MatchedOrder> matchedOrders)
2.e. maybe remove cash processing outside of the loop of avg cost calculation
2.f. saving the cash list updates the lastupdated date which bypasses the check and ignores the other stock.
2.g save only when all stocks are processed.


todo:
1. create cash table: use correct columns
1.a add dummy values
2.a Get latest cash per account
3. Look for each stock per account and find out if net selling, if yes, then add the net price (pos or neg)
4. Update cash position.
5. generate cash.txt with this format OT3530-2;4440.13;.0000;4440.13
 
 
 lookup cash using accountcode
 loop thru matchedorder(every account transaction per stock)
	if (BUY) ? CASH - (Price * Quantity) : CASH + (Price* Quantity)

todo:
1.get all records
2.sort by account then by date
3.write soa.txt file
2.            //A00001-1;01/15/2016;SI-1  07149;Sale of DMC 100000 shares @ 12.1200 
            //A01618-0;12/29/2015;BI-175968;Purchase of MER 3260 shares @ 317.4000


OUT RECEIPT NO. 32635 DATED 26 JAN. 2016 AND 
2,400 SHARES OF AC, 55,000 SHARES OF ALI, 1,200 SHARES OF GTCAP, 1,800 SHARES OF SM 


todo:done
1. Let's create 3 accounts in table.  With different cases:
2. Let's create a script that will get only the last record of the matched order per account per stock.  this step is right after all 
avg cost calculation is done.
2. We get a list of all matched orders for today.
2.a. Get all records sorted by account and stock and date.
2.a.a  Loop thru records
2.b. via LinQ, get only per stock per account.
2.c. Sort by date. Grab only the first record
2.d. append it to postcost. Save two files. One has today's date poscost-<date>.txt and one just latest poscost.txt


todo:
1. combine all test cases for db in one logic and workflow done.


todo:
1. create test cases:
   a. one record only same date
   a. two records same date
   a. three records same date
todo:
1. generate poscost
   a. get first record.
   b. get last periods record.

   a. run query via StockXDBController using MatchedOrder table
   b. save all records.
   c. get all records we just saved

   if first record is same as last record 
     then add first record to the list of just recently saved records and apply first record formula (compute avg cost first)
	 and apply 2nd record formula to the rest of the list
	 then save all items table
   if different:
       we use last record as first record added on the list then we don't apply first record formula
	   we use 2nd record formula all the remaining items in the list and first record will be un-touched
	   then save remaining items to table

   a.a.a first check if there is no record yet for account and stock
   b. if no record then save all three records and remember the first record as the first buy.
   if there is one record, we determine if it is part of the group and apply different formula.
   rest of formula to the remaining records.
   
   
    
   
   a.a select top 2 * from MatchedOrder order by date desc
   b. Loop thru using account and stock
   b.a transform List<MatchOrder> to List<ClientAverageCost>
   c.a. Process each line with average cost calculator
   c.c feed to calculator
   d.d transform List<ClientAverageCost> back to List<MatchOrder>
   e.b. save to table the update avg_cost
   f.c  (query table and make sure values match)
   g. append each line to a csv.
   h. format: A00001-1;BDO;5000;105.2281
   i.a account_code;stock_code;volume;avg_cost
2. generate soa
		-add description of transaction
		-"bought X shares at Y price"
3. validation
   a. add validation to filter input data.  throw an error if List is not two on the right level
   b. add validation before outputting data
todo:
1. create poscos table (portfolio balance) done
2. add sample files to the solution. client.txt, postcos, soa done


Filled Orders Report
USERID|ACCOUNTCODE|ORDERTIME|SIDE|STOCKCODE|PRICE|QUANTITY


Account Trade Summary Report
USERID|ACCOUNTCODE|STOCKCODE|AVGPRICE|QUANTITY* 

*Positive value for Net Buying, negative value for Net Selling

cash.txt
portfolio.com

to maintain cash balance:
pre req:
1. create tables: (auto table identity id must be present) done
	a. ClientAverageCost
	b. MatchedOrder
	c. Summay
2. update entity model from db 
1. initial load of cash 
	a. read cash.txt
	b. get one line
	c. parse with vertical bar
	d. insert if not exist, or update if it is

2. We process the filled orders query to 10.11.15.21 server.
	a. run query via StockXDBController
	b. get one line from reader
	c. save each line using date, volume, stock, account code composite key
	d. update 


3. Process each filled orders by creating a soa:
	a. run query via StockXDBController
		-add description of transaction
		-"bought X shares at Y price"
	b. get one line from reader
	c. append each line to a csv.
	d. format is this: example of format.

4. We process the summary orders query to 10.11.15.21 server.
	a. run query via StockXDBController
	b. get one line from reader
	c. save each line using date, UserId, AccountCode, StockCode, BoardLot, AvePriceBuy, AvePriceSell, NetQuantity.
	d. Use date, UserId, AccountCode, StockCode as composite key 
	e. 

5. Process each summary orders by creating a poscost:
	a. run query via StockXDBController
	b. get one line from reader
	c. update balance in portfolio ( we might need a portfolio table. use same columns as poscost) 
	d. create a new csv
	c. append each line to a csv.
	d. format is this: example of format


2. (deprecated) We process the filled orders text file.
	a. read filledorders.txt
	b. get one line from array
	c. parse with vertical bar or comma
	d. calculate the avg cost and calculate the cash position
		i. Use same formula from excel
		ii. Use unit testing to make sure our calculation is correct
	e. create cash.txt
	e. load cash.txt and run cash function to update cash balance
	f. save cash.txt to server folder
3. Use GIT to update code


