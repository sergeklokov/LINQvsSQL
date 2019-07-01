use AdventureWorks2016CTP3;

-- query for pagination
select * from 
(select ROW_NUMBER() OVER (ORDER BY NAME) RowNum, * from People) As NumberedPeople
where RowNum between 1 and 3 
order by RowNum;
