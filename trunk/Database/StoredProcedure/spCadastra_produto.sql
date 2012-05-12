declare @o varchar(100); set @o = 'spCadastra_produto';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spCadastra_produto (
	@xml xml,
	@idProduto int output
) as
begin
	set xact_abort on;

	begin try
		begin transaction;

		insert into Produtos (
			nmTitulo,
			nmTituloOriginal,
			dsProduto,
			ISBN,
			dsAutores,
			nmEditora,
			nrAno,
			dsEdicao,
			qtdProduto,
			nmImagem,
			vlPreco,
			idCategoria)
		select
			x.n.value('nmTitulo[1]', 'varchar(200)'),
			x.n.value('nmTituloOriginal[1]', 'varchar(200)'),
			x.n.value('dsProduto[1]', 'varchar(4000)'),
			x.n.value('ISBN[1]', 'char(13)'),
			x.n.value('dsAutores[1]', 'varchar(1000)'),
			x.n.value('nmEditora[1]', 'varchar(100)'),
			x.n.value('nrAno[1]', 'int'),
			x.n.value('dsEdicao[1]', 'varchar(50)'),
			x.n.value('qtdProduto[1]', 'int'),
			x.n.value('nmImagem[1]', 'varchar(100)'),
			x.n.value('vlPreco[1]', 'decimal(10,2)'),
			x.n.value('idCategoria[1]', 'int')
		from
			@xml.nodes('/*[1]') x(n);

		set @idProduto = scope_identity();
		
		commit transaction;
	end try
	begin catch
		if (xact_state()) = -1
			rollback transaction;
		if (xact_state()) = 1
			commit transaction;
	end catch;
end
