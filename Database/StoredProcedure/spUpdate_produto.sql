declare @o varchar(100); set @o = 'spUpdate_produto';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spUpdate_produto (
	@xml xml
) as
begin
	update Produtos set
		nmTitulo = x.n.value('nmTitulo[1]', 'varchar(200)'),
		nmTituloOriginal = x.n.value('nmTituloOriginal[1]', 'varchar(200)'),
		dsProduto = x.n.value('dsProduto[1]', 'varchar(4000)'),
		ISBN = x.n.value('ISBN[1]', 'char(13)'),
		dsAutores = x.n.value('dsAutores[1]', 'varchar(1000)'),
		nmEditora = x.n.value('nmEditora[1]', 'varchar(100)'),
		nrAno = x.n.value('nrAno[1]', 'int'),
		dsEdicao = x.n.value('dsEdicao[1]', 'varchar(50)'),
		qtdProduto = x.n.value('qtdProduto[1]', 'int'),
		nmImagem = x.n.value('nmImagem[1]', 'varchar(100)'),
		vlPreco = x.n.value('vlPreco[1]', 'decimal(10,2)'),
		idCategoria = x.n.value('idCategoria[1]', 'int')
	from
		@xml.nodes('/*[1]') x(n)
	where
		idProduto = x.n.value('idProduto[1]','int');
end