
-- Выборка всех полей
SELECT * 
FROM Sellers;

-- Выборка конкретных полей 
SELECT
	FirstName
	, LastName
FROM Sellers;

-- Выбор с условием
SELECT *
FROM Reviews
WHERE Rating > 1;

-- Сортировка результатов
SELECT *
FROM Sellers
ORDER BY 
	LastName ASC
	, FirstName ASC;

-- Выборка с несколькими условиями
SELECT 
    ReceiptID
    , ReceiptNumber
    , TotalAmount
    , PaymentType
FROM Receipts
WHERE 
	TotalAmount > 1000 
    AND PaymentType = 'Card'
ORDER BY TotalAmount DESC;


-- Изменение данных
UPDATE Sellers
SET IsActive = 1
WHERE SellerID = 5;

UPDATE ContactInfo
SET Phone = '+79998887766',
    Email = 'new6email@mail.ru'
WHERE SellerID = 3;

SELECT *
FROM ContactInfo
WHERE SellerID = 3;

UPDATE Receipts
SET TotalAmount = TotalAmount * 0.9
WHERE SaleDate < '2024-01-16';

SELECT * FROM Receipts;

-- Удаление данных
DELETE 
FROM Reviews
WHERE Rating <= 2;

SELECT *
FROM Reviews 
WHERE Rating <= 2;


-- Выборка с группировкой
SELECT 
    s.ShopName
    , COUNT(r.ReceiptID) AS NumberOfReceipts
    , SUM(r.TotalAmount) AS TotalSales
    , AVG(r.TotalAmount) AS AverageReceipt
FROM Shops s
	LEFT JOIN Receipts r ON s.ShopID = r.ShopID
GROUP BY
	s.ShopID
	, s.ShopName
ORDER BY TotalSales DESC;

SELECT 
    CAST(SaleDate AS DATE) AS SaleDay
    , COUNT(*) AS ReceiptsCount
    , SUM(TotalAmount) AS DailyTotal
FROM Receipts
GROUP BY CAST(SaleDate AS DATE)
ORDER BY SaleDay DESC;


-- Выборка из нескольких таблиц (пересечение)
SELECT
	r.ReceiptNumber
    , r.TotalAmount
    , r.SaleDate
    , s.ShopName
    , sel.LastName + ' ' + sel.FirstName AS SellerName
FROM Receipts r
	INNER JOIN Shops s ON r.ShopID = s.ShopID
	INNER JOIN Sellers sel ON r.SellerID = sel.SellerID;

-- Выборка из нескольких таблиц (левое)
SELECT 
    sel.LastName + ' ' + sel.FirstName AS SellerName
    , ci.Phone
    , ci.Email
    , ci.Address
FROM Sellers sel
	LEFT JOIN ContactInfo ci ON sel.SellerID = ci.SellerID
WHERE sel.IsActive = 1
ORDER BY sel.LastName;

-- Выборка из нескольких таблиц (правое)
SELECT 
    sel.LastName + ' ' + sel.FirstName AS SellerName
    , ci.Phone
    , ci.Email
FROM Sellers sel
	RIGHT JOIN ContactInfo ci ON sel.SellerID = ci.SellerID;

-- Выборка из нескольких таблиц (полное)
SELECT 
    sel.LastName + ' ' + sel.FirstName AS SellerName
    , ci.Phone
    , ci.Email
FROM Sellers sel
	FULL OUTER JOIN ContactInfo ci ON sel.SellerID = ci.SellerID;
