declare @o varchar(100); set @o = 'spLista_produtos';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_produtos (
	@categoria varchar(100) = null,
	@nmProduto varchar(100) = '%%',
	@pageindex int = 0,
	@pageSize int = 10,
	@totalRowCount int out
) as
begin
	select
		@totalRowCount = count(*)
	from
		Produtos
	where
		nmTitulo like @nmProduto;

	declare @startRow int, @endRow int;

	set @startRow = (@pageIndex * @pageSize) + 1;
	set @endRow   = @startRow + @pageSize - 1;

	with [Produto] as (
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
			--,nmImagem
			,row_number() over(order by idProduto) rownum
		from 
			Produtos	
		where
			nmTitulo like @nmProduto
	)
	select
		*
	from
		[Produto]
	where
		rownum between @startRow and @endRow
	for xml auto, elements, root('ListaProduto')
end