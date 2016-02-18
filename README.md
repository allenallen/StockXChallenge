# StockXChallenge plan

Filled Orders Report
USERID|ACCOUNTCODE|ORDERTIME|SIDE|STOCKCODE|PRICE|QUANTITY


Account Trade Summary Report
USERID|ACCOUNTCODE|STOCKCODE|AVGPRICE|QUANTITY* 

*Positive value for Net Buying, negative value for Net Selling

cash.txt
portfolio.com

to maintain cash balance:
pre req:
1. create a table cash (auto table identity id must be present)
2. update entity model from db 
1. initial load of cash
	a. read cash.txt
	b. get one line
	c. parse with vertical bar
	d. insert if not exist, or update if it is
2. We process the filled orders text file.
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


