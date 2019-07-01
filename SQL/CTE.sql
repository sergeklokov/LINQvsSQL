use AdventureWorks2016CTP3;

select Name from People order by Name;

-- variouse Common Table Expression (CTE) demos
;with CTE_CELL_OWNERS
as (select distinct Name from People where CellPhoneID is not null)
select * from CTE_CELL_OWNERS

-- some bogus task, but two CTE in one SQL statement
;with 
	CTE_CELL_OWNERS
		as (select Name from People where CellPhoneID is not null),
	CTE_NOCELL
		as (select Name from People where CellPhoneID is null)
select * from CTE_CELL_OWNERS
union all -- or distinct, not all
select * from CTE_NOCELL
order by 1	
	 

-- recursive CTE example
;with CTE_NUMBERS as
	(select n=0
	union all
	select n+1 from CTE_NUMBERS
	where n+1 < 10)
select n from CTE_NUMBERS
