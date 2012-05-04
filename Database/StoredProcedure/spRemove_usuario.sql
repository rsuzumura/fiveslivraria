declare @o varchar(100); set @o = 'spRemove_usuario';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spRemove_usuario (
	@dsLogin varchar(100),
	@result bit output
) as
begin
	set xact_abort on;

	begin try
		begin transaction;
		
		delete
			Usuarios
		where
			dsLogin = @dsLogin;
			
		delete
			fiv_Logins_Roles
		where
			lro_Username = @dsLogin;

		delete
			fiv_Logins
		where
			lgi_Username = @dsLogin;
		set @result = 1

		commit transaction;
	end try
	begin catch
		set @result = 0
		rollback transaction;		
	end catch;
		
end