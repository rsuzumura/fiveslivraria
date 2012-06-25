declare @o varchar(100); set @o = 'spAltera_email';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spAltera_email (
	@username varchar(100),
	@email varchar(100)
) as
begin	
	update fiv_Logins set
		lgi_Email = @email
	where
		lgi_Username = @username;
end
go
