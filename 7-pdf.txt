set serveroutput on

--package specification

create or replace package math_pkg as
	function square_num(p_num number) return number;
end math_pkg;
/

--package body

create or replace package body math_pkg as 
	function square_num(p_num number) return number is 
	begin
		return p_num * p_num;
	end;
end math_pkg;
/

--test the package

begin
 DBMS_OUTPUT.PUT_LINE('sqaure of 5:'||math_pkg.square_num(5));
end;
/

--predefined pakage

begin
DBMS_OUTPUT.PUT_LINE('random num:'||ROUND(DBMS_RANDOM.VALUE(1,50)));
DBMS_OUTPUT.PUT_LINE('random string: '||DBMS_RANDOM.STRING('U',8));
end;
/
