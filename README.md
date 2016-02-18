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
	3. Formulas:
		a. Gross: Cost * Volume
		b. Commission: if(Cost * Volume * .0025) < 20) return 20
						else return (Cost * Volume * .0025)
		c. VAT12%: Commission * .12
		d. Sales Tax: Gross * .005
		e. PCD/SCCP: Gross * .0001
		f. Buy Price: Gross * Commission * Vat12 * PCD/SCCP
		g. Sell Price: Gross - (Commission + Vat12 + Sales Tax + PCD/SCCP)
		h. Net Price: if (BUY) return Buy Price; 
						else if (SELL) return Sell PRICE|QUANTITY		
							else if (IN) return .01
		i. SUM of net Price: if (firstTransaction) return Net Price 
								else 
									if(SELL) return previousAverageCost * Net Volume
									else return previous SUM of net Price + current Net Price
		j. Net Volume: 	if (firstTransaction) return if (SELL) 0 : Volume
						else 
							if (SELL): return previous Net volume + -(current Volume)
							else return previous Net volume + current Column
		k. Average Cost: 	if Net Volume == 0: return 	0
							else SUM of net Price / Net Volume
		
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


UPDATE HERE
