declare @o varchar(100); set @o = 'spCadastra_empresa';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spCadastra_empresa (
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
		
		insert into Empresa (
			idCliente,
			nmRazaoSocial,
			cnpj,
			inscricaoEstadual,
			inscricaoMunicipal
		)
		select
			@idCliente,
			x.n.value('nmRazaoSocial[1]','varchar(200)'),
			x.n.value('cnpj[1]','varchar(14)'),
			x.n.value('inscricaoEstadual[1]','varchar(20)'),
			x.n.value('inscricaoMunicipal[1]','varchar(20)')
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