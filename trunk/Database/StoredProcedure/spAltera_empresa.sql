declare @o varchar(100); set @o = 'spAltera_empresa';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spAltera_empresa (
	@xml xml
) as
begin	
	begin try
		begin tran
		declare @idCliente int;
		
		select
			@idCliente = x.n.value('idCliente[1]','int')
		from
			@xml.nodes('/*[1]') x(n);
		
		update Cliente set
			nmCliente = x.n.value('nmCliente[1]','varchar(200)')
		from
			@xml.nodes('/*[1]') x(n)
		where
			idCliente = @idCliente;
		
		update Empresa set
			inscricaoEstadual = x.n.value('inscricaoEstadual[1]','varchar(20)'),
			inscricaoMunicipal = x.n.value('inscricaoMunicipal[1]','varchar(20)')			
		from
			@xml.nodes('/*[1]') x(n)
		where
			idCliente = @idCliente;
		
		
		delete from
			Endereco
		where
			idCliente = @idCliente;
				
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