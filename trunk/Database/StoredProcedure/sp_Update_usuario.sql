declare @o varchar(100); set @o = 'spUpdate_usuario';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spUpdate_usuario (
	@xml xml
) as
begin
	begin try
		begin tran
			--Atualização do Email e do bloqueio
			update fiv_Logins set
				lgi_Email		= x.n.value('Email[1]', 'varchar(100)'),
				lgi_IsLockedOut = x.n.value('IsLocked[1]', 'bit')
			from
				@xml.nodes('/*[1]') x(n)
			where
				lgi_Username = x.n.value('dsLogin[1]', 'varchar(100)');
			
			-- Atualização da permissão
			update fiv_Logins_Roles set
				lro_Rolename = x.n.value('Email[1]', 'varchar(100)')
			from
				@xml.nodes('/*[1]') x(n)
			where
				lro_Username = x.n.value('dsLogin[1]', 'varchar(100)');
		
		commit tran
	end try
	begin catch
		rollback tran
	end catch;		
end