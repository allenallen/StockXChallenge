# StockXChallenge plan

Filled Orders Report
USERID|ACCOUNTCODE|ORDERTIME|SIDE|STOCKCODE|PRICE|QUANTITY


Account Trade Summary Report
USERID|ACCOUNTCODE|STOCKCODE|AVGPRICE|QUANTITY* 

*Positive value for Net Buying, negative value for Net Selling

cash.txt
portfolio.com


pre req:
	1. create a table cash (auto table identity id must be present)
	2. update entity model from db 

to maintain cash balance:
1. initial load of cash
	a. read cash.txt
	b. get one line
	c. parse with vertical bar
	d. insert if not exist, or update if it is
		
		--create utils.cs
		--create read file method ->  string[]
		--create method that loops through the string[] and insert parsed data into database
			

Unit Testing.


util.calculateAverageCost(stockCode, quantity, price)



return avgcost


TestCalculateAverageCost()


for loop in combinations:

	avgCost = util.calculateAvgCost(stockCode, quantity, price)

		assert avgCost, knownAveCost(stockCode, quantity, price) 

			if not equal fail test


			def knownAveCost(knownAveCost):
			list = [stockCode, quantity, price, avgcost]

			  if stockCode, quantity, price == "given"

			  return avgcost
			     
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
<<<<<<< HEAD
=======




>>>>>>> e300db497d7efb1f775f653ec50d5860f42c9edd
