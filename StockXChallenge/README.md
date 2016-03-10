# StockXChallenge plan
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


