declare @o varchar(100); set @o = 'spLista_cliente';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spLista_cliente (
	@nmCliente varchar(100) = null,
	@pageindex int,
	@pageSize int,
	@totalRowCount int out
) as
begin

	select
		@totalRowCount = count(*)
	from
		Cliente
	where
		(@nmCliente is null or nmCliente like @nmCliente);

	declare @startRow int, @endRow int;

	set @startRow = (@pageIndex * @pageSize) + 1;
	set @endRow   = @startRow + @pageSize - 1;
	
	select
		[Cliente].*
	from (
		select
			idCliente,
			nmCliente,
			idUsuario,
			row_number() over(order by nmCliente) [rownum]
		from
			Cliente
		where
			(@nmCliente is null or nmCliente like @nmCliente)
	) [Cliente]	
	where
		rownum between @startRow and @endRow
	for xml auto, elements, root('ListaCliente')	
end
