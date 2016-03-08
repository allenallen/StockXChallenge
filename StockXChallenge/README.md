# StockXChallenge plan

todo:
1. create poscos table (portfolio balance)
2. add sample files to the solution. client.txt, postcos, ledger 


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


3. Process each filled orders by creating a ledger:
	a. run query via StockXDBController
	b. get one line from reader
	c. append each line to a csv.
	d. format is this: example of format.

4. We process the summary orders query to 10.11.15.21 server.
	a. run query via StockXDBController
	b. get one line from reader
	c. save each line using date, UserId, AccountCode, StockCode, BoardLot, AvePriceBuy, AvePriceSell, NetQuantity.
	d. Use date, UserId, AccountCode, StockCode as composite key 
	e. 

5. Process each summary orders by creating a postcost:
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


