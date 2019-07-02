use AdventureWorks2016CTP3;

select * from People order by Name;
select * from CellPhone;

select Name, Number from People l inner join CellPhone p on l.CellPhoneID = p.CellPhoneID  -- people with phones , and phones
select Name, Number from People l full join CellPhone p on l.CellPhoneID = p.CellPhoneID
select Name, Number  from People l full join CellPhone p on l.CellPhoneID = p.CellPhoneID where p.CellPhoneID is null or l.PersonID is null -- full excPeopleng join

select *  from People l, CellPhone p -- cartesian join (multiplication)
select Name, Number  from People l, CellPhone p order by 1, 2 -- cartesian join (multiplication)

select Name, Number from People l left outer join CellPhone p on l.CellPhoneID = p.CellPhoneID -- all people and phone # if owe
select Name, Number from People l left outer join CellPhone p on l.CellPhoneID = p.CellPhoneID where p.CellPhoneID IS NOT NULL	-- people with CellPhone
select Name, Number from People l left outer join CellPhone p on l.CellPhoneID = p.CellPhoneID where p.CellPhoneID IS NULL		-- people with no CellPhone

-- CellPhones used by more than 1 person
select p.Number, count(l.CellPhoneID) Abonents
from People l left join CellPhone p on l.CellPhoneID = p.CellPhoneID
group by p.Number, l.CellPhoneID
having count(l.CellPhoneID) > 1