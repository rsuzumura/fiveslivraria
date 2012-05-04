declare @o varchar(100); set @o = 'spVerifica_email';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spVerifica_email (
	@dsLogin varchar(100),
	@email varchar(100),
	@result bit output
) as
begin
	if exists (select 1 from fiv_Logins where lgi_Email = @email and lgi_Username <> @dsLogin)
		set @result = 0
	else
		set @result = 1
end