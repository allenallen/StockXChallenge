SET NOCOUNT ON

--GO
/**DROP TEMP TABLES**/		
	IF OBJECT_ID('tempdb.dbo.#TempOrds2') IS NOT NULL
		DROP TABLE tempdb.dbo.#TempOrds2
	
	IF OBJECT_ID('tempdb.dbo.#TempMatchedOrders') IS NOT NULL
		DROP TABLE  tempdb.dbo.#TempMatchedOrders
/**END OF DROP TEMP TABLES**/	

--GO
/**RETRIEVE OPEN/AMENDED ORDERS**/	
	SELECT 	DISTINCT
			OTWC.username,
			o.ParentOrdID,
			o.OrdStatus AS "OrderStatus",
			o.TransactTime AS "OrderDate", 
			IsNull(a.AccountCode,'') AS "WebID", 
			isNull([OT-WebTrading].[dbo].FXfunc_GetCFESide(o.Side),'') AS "Side",
			isNull([OT-WebTrading].[dbo].FXfunc_GetCFEBoard(SUBSTRING(o.Symbol,1,1)),'') AS "Board",
			isNull([OT-WebTrading].[dbo].FXfunc_GetCFETerms(o.TimeInForce),'') AS Terms,
			IsNull(SUBSTRING(o.Symbol,2,10),'') AS "Security",
			IsNull(o.Price,0) AS "Price",
			IsNull(o.OrderQty,0) AS "Vol",
			IsNull(o.LeavesQty,0) AS "PubVol",
			IsNull(o.CumQty,0) AS "MatchedVol",
			IsNull(CONVERT(varchar(8),o.ExpireDate,112),'') AS "Expiration",
			IsNull(a.AccountCode,'') AS "AccountID",
			ISNULL(o.UnfilledAmount,0) AS "UnfilledAmount"
			
	INTO #TempOrds2
			
	FROM [TEDS-FIX].[TEDS-FIX].dbo.fxOrderSnapshot  o 
	INNER JOIN [TEDS-FIX].[TEDS-FIX].[dbo].fxOrderPartySnapshot p	on (o.parentordid = p.parentordid) 
	INNER JOIN [TEDS-FIX].[TEDS-FIX].[dbo].Accounts a on (a.XchgAccountCode = p.ID)    
	INNER JOIN [OT-WebComp].[dbo].tbl_ot_login OTWC on (OTWC.XchgAccountCode = p.ID)    

	WHERE ordstatus IN ('0','5')
	AND CONVERT(varchar(8),ExpireDate,112) >= CONVERT(VARCHAR(8),GETDATE(),112)
	AND p.[Role] = 24
	AND a.OnlineTrading = 1 
/**END OF RETRIEVAL**/	
--GO
/**REPORT 1: TRADE LIST**/				
	SELECT	o.username AS "USERID",
			o.WebID AS "ACCOUNTCODE",
			o.ParentOrdID AS "PARENTORDERID",
			o.OrderDate AS "ORDERDATE",
			o.Side AS "SIDE",
			'MAIN' as BOARDLOT,
			o.[Security] AS "STOCKCODE",	
			o.[Price] AS "PRICE",	
			o.Vol AS "QUANTITY",
			o.Terms AS "TERM"
	INTO 	#TempMatchedOrders
	FROM 	#TempOrds2 o
	INNER JOIN [OT-WebQuotes].dbo.QUOTESPH_SECURITIES_REV q on 
	(
		(o.[Security] = q.Seccode and o.Price >= q.LastPrice and o.[side] = 'B') 
			or 
		(o.[Security] = q.Seccode and o.Price <= q.LastPrice and o.[side] = 'S')
	)
	WHERE o.Board = 3
	AND q.LastPrice <> 0
		
	UNION ALL
		
	SELECT	o.username AS "USERID",
			o.WebID AS "ACCOUNTCODE",
			o.ParentOrdID AS "PARENTORDERID",
			o.OrderDate AS "ORDERDATE",
			o.Side AS "SIDE",
			'ODDLOT' as BOARDLOT,
			o.[Security] AS "STOCKCODE",	
			o.[Price] AS "PRICE",	
			o.Vol AS "QUANTITY"	,
			o.Terms AS "TERM"
	FROM 	#TempOrds2 o
	INNER JOIN [OT-WebQuotes].dbo.QUOTESPH_SECURITIES_REV_ODDLOT q on 
	(
		(o.[Security] = q.Seccode and o.Price >= q.LastPrice and o.[side] = 'B') 
			or 
		(o.[Security] = q.Seccode and o.Price <= q.LastPrice and o.[side] = 'S')
	)
	WHERE o.Board = 4
	AND q.LastPrice <> 0
		
	SELECT	USERID AS "USER ID",
			ACCOUNTCODE AS "ACCOUNT CODE",
			ORDERDATE AS "ORDER DATETIME",
			SIDE AS "SIDE",
			BOARDLOT AS BOARDLOT,
			STOCKCODE AS "STOCKCODE",	
			Price AS "PRICE",	
			QUANTITY AS "QUANTITY"
	FROM	#TempMatchedOrders
/**END OF REPORT 1**/				
		
--GO		
/**REPORT 2: SUMMARY RESULT**/		
/*
	SELECT	[USERID] AS "USER ID",
			[ACCOUNTCODE] AS "ACCOUNT CODE",
			[STOCKCODE] AS "STOCKCODE",	
			[BOARDLOT] AS BOARDLOT,		
				CASE WHEN SUM(CASE WHEN [SIDE] = 'B' THEN 1 ELSE 0 END) > 1 THEN  
					SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),QUANTITY) * CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END) /
					SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END)
				ELSE
					SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END)
				END				
			AS "AVERAGE PRICE(BUY)",						
				CASE WHEN SUM(CASE WHEN [SIDE] = 'S' THEN 1 ELSE 0 END) > 1 THEN  
					SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),QUANTITY) * CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END) /
					SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END)
				ELSE
					SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),PRICE) ELSE 0 END)
				END				
			AS "AVERAGE PRICE(SELL)",	
				SUM(CASE WHEN [SIDE] = 'B' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END) -
				SUM(CASE WHEN [SIDE] = 'S' THEN CONVERT(DECIMAL(19,9),QUANTITY) ELSE 0 END)				
			AS "NET QUANTITY"
	FROM #TempMatchedOrders
	GROUP BY [USERID],[ACCOUNTCODE],[STOCKCODE],[BOARDLOT]
	ORDER BY [USERID],[ACCOUNTCODE],[STOCKCODE],[BOARDLOT]
*/
/**END OF REPORT 2**/	

--GO
/**UPDATE EXPIRY DATE OF MATCHED GTD ORDERS**/
	UPDATE 	[TEDS-FIX].[TEDS-FIX].dbo.fxOrderSnapshot
	SET		ExpireDate = GETDATE()
	WHERE	TimeInForce IN ('G','6') 
		AND ParentOrdID 
	        IN (SELECT PARENTORDERID FROM #TempMatchedOrders)
			
--	SELECT CONVERT(VARCHAR,@@ROWCOUNT) AS "Number of GTD Orders Modified"
/**END OF UPDATE SCRIPT**/
