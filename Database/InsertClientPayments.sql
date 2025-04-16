
INSERT INTO public."ClientPayments"("Id", "ClientId", "Dt", "Amount")
SELECT 1, 1, CAST('2022-01-03 17:24:00' AS timestamp without time zone), 100 UNION ALL
SELECT 2, 1, CAST('2022-01-05 17:24:14' AS timestamp without time zone), 200 UNION ALL
SELECT 3, 1, CAST('2022-01-05 18:23:34' AS timestamp without time zone), 250 UNION ALL
SELECT 4, 1, CAST('2022-01-07 10:12:38' AS timestamp without time zone), 50 UNION ALL
SELECT 5, 2, CAST('2022-01-05 17:24:14' AS timestamp without time zone), 278 UNION ALL
SELECT 6, 2, CAST('2022-01-10 12:39:29' AS timestamp without time zone), 300 

-- Test 1
select * from GetClientDaylyAmount(CAST('2022-01-02' AS DATE), CAST('2022-01-07' AS DATE), 1);

-- Test 2
select * from GetClientDaylyAmount(CAST('2022-01-04' AS DATE), CAST('2022-01-11' AS DATE), 2);
