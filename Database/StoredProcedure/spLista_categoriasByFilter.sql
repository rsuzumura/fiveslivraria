declare @o varchar(100); set @o = 'spLista_categoriasByFilter';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_categoriasByFilter(
	@dsCategoria varchar(100) = null,
	@enabledOnly bit = 0,
	@pageIndex int = 0,
	@pageSize int = 10,
	@totalRowCount int output
) as
begin
	select
		@totalRowCount = count(*)
	from
		Categoria
	where
		(@dsCategoria is null or dsCategoria like @dsCategoria) and
		(@enabledOnly = 0 or stCategoria = @enabledOnly);

	declare @startRow int, @endRow int;

	set @startRow = (@pageIndex * @pageSize) + 1;
	set @endRow   = @startRow + @pageSize - 1;
	
	select
		[Categoria].*
	from (	
		select
			idCategoria,
			dsCategoria,
			stCategoria,
			row_number() over (order by dsCategoria) [rownum]
		from
			Categoria
		where
			(@dsCategoria is null or dsCategoria like @dsCategoria) and
			(@enabledOnly = 0 or stCategoria = @enabledOnly)
	) [Categoria]
	where
		rownum between @startRow and @endRow
	for xml auto, elements, root('ListaCategoria')
end