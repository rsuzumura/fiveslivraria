declare @o varchar(100); set @o = 'spLista_produtos';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_produtos (
	@categoria int = 0,
	@nmProduto varchar(100) = '%%',
	@pageindex int = 0,
	@pageSize int = 10,
	@totalRowCount int  = null out
) as
begin
	select
		@totalRowCount = count(*)
	from
		Produtos
	where
		nmTitulo like @nmProduto;
	
	select @totalRowCount = @totalRowCount/@pageSize + 1

	declare @startRow int, @endRow int;

	set @startRow = (@pageIndex * @pageSize) + 1;
	set @endRow   = @startRow + @pageSize - 1;

	select
		[Produto].*
	from (
		select 			
			 idProduto
			,nmTitulo
			,nmTituloOriginal
			,dsProduto
			,ISBN
			,dsAutores
			,nmEditora
			,nrAno
			,dsEdicao
			,qtdProduto
			,nmImagem
			,vlPreco
			,cat.dsCategoria [dsCategoria]
			,row_number() over(order by idProduto) rownum
		from 
			Produtos p
			left Join Categoria cat ON
			p.idCategoria = cat.idCategoria	
		where
			nmTitulo like @nmProduto
			and (@categoria = 0 or p.idCategoria = @categoria)
	) [Produto]
	where
		rownum between @startRow and @endRow
	for xml auto, elements, root('ListaProduto')
end
