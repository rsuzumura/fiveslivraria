declare @o varchar(100); set @o = 'spCadastra_pessoa';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spCadastra_pessoa (
	@xml xml,
	@idCliente int output
) as
begin
	set @idCliente = 0;	
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
			@idCliente,
			x.n.value('cpf[1]','char(11)'),
			x.n.value('rg[1]','char(10)'),
			x.n.value('dtNascimento[1]','datetime'),
			x.n.value('nmMae[1]','varchar(200)')
		from
			@xml.nodes('/*[1]') x(n);
			
		insert into Endereco (
			dsEndereco,
			nrEndereco,
			compEndereco,
			cep,
			dsBairro,
			idMunicipio,
			idCliente
		)
		select
			x.n.value('dsEndereco[1]','varchar(200)'),
			x.n.value('nrEndereco[1]','varchar(10)'),
			x.n.value('compEndereco[1]','varchar(20)'),
			x.n.value('cep[1]','varchar(10)'),
			x.n.value('dsBairro[1]','varchar(100)'),
			x.n.value('idMunicipio[1]','int'),
			@idCliente
		from
			@xml.nodes('/*[1]/EnderecoCollection/Endereco') x(n);
		
		commit tran
	end try
	begin catch
		rollback tran
	end catch	
end