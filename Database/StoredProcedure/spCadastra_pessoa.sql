declare @o varchar(100); set @o = 'spCadastra_pessoa';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spCadastra_pessoa (
	@xml xml,
	@success bit output
) as
begin
	declare @idCliente int;
	
	begin try
		begin tran
		insert into Cliente (
			nmCliente,
			idUsuario
		)
		select
			x.n.value('nmCliente[1]','varchar(200)'),
			x.n.value('idUsuario[1]','int')
		from
			@xml.nodes('/*[1]') x(n);
		
		set @idCliente = scope_identity();
		
		insert into Pessoa (
			idCliente,
			cpf,
			rg,
			dtNascimento,
			nmMae
		)
		select
			x.n.value('idCliente[1]','int'),
			x.n.value('cpf[1]','char(11)'),
			x.n.value('rg[1]','char(10)'),
			x.n.value('dtNascimento[1]','datetime'),
			x.n.value('nmMae[1]','varchar(200)')
		from
			@xml.nodes('/*[1]') x(n);
		set @success = 1
		commit tran
	end try
	begin catch
		rollback tran
		set @success = 0
	end catch	
end