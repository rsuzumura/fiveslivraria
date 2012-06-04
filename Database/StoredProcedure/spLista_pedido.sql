declare @o varchar(100); set @o = 'spLista_pedido';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_pedido (
	@idCliente int,
	@pageindex int,
	@pageSize int,
	@totalRowCount int out
) as
begin
	select
		@totalRowCount = count(*)
	from
		Pedido
	where
		idCliente = @idCliente;

	declare @startRow int, @endRow int;

	set @startRow = (@pageIndex * @pageSize) + 1;
	set @endRow   = @startRow + @pageSize - 1;

	select
		[Pedido].*
	from (
		select
			idPedido,
			idCliente,
			dtPedido,
			idUsuario,
			dtEntregaPrevista,
			dtEntregaReal,
			idEnderecoEntrega,
			idEnderecoCobranca,
			tpPagamento,
			nrParcelas
		from
			Pedido
		where
			idCliente = @idCliente
		) [Pedido]
	for xml auto, elements, root('ListaPedido')
end
