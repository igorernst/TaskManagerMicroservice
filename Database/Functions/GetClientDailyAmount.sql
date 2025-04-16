CREATE OR REPLACE FUNCTION GetClientDailyAmount(
    start_date DATE,
    end_date DATE,
	ClientId INT
)
RETURNS TABLE(Dt DATE, Amount money) AS $$
BEGIN
    RETURN QUERY 
select i::date, COALESCE(SUM(C."Amount"), CAST(0 AS MONEY)) as Amount
FROM generate_series(start_date, end_date, '1 day'::interval) i 
LEFT JOIN "ClientPayments" C ON DATE(C."Dt") = i.i::date AND C."ClientId" = ClientId
GROUP BY i.i::date
ORDER BY i.i::date
;
END; $$ 
LANGUAGE 'plpgsql';
