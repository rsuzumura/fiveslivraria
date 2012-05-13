declare @o varchar(100); set @o = 'spFinaliza_compra';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spFinaliza_compra (
	@dsPagamento varchar(100),
	@idUsuario smallint,
	@nrParcelas int = 0
) as
begin
	
	
	declare @idCliente int 
			,@tpPagamento int
			,@idPedido int
			
	select @idCliente = idCliente
	from Cliente 
	where idUsuario = @idUsuario
	
	select @tpPagamento = tpPagamento
	from Tipo_pagamento 
	where dsPagamento = @dsPagamento
	
	
	insert into Pedido(
		dtPedido
		,idCliente
		,idUsuario
		,dtEntregaPrevista
		,tpPagamento
		,nrParcelas)
		Values(
		GETDATE()
		,@idCliente
		,@idUsuario
		,DATEADD(DAY,5,GETDATE())
		,@tpPagamento
		,@nrParcelas
		)
	select @idPedido = @@IDENTITY
	
	
	insert into ItemPedido(
		idPedido
		,idProduto
		,qtdProduto
		,vlDesconto
		,vlFinal)
	select 
		@idPedido
		,idProduto
		,nrQtdProduto
		,0
		,vlFinal
	from Carrinho
	where dvStatus = 'C'
	and idCliente = @idCliente
	
	
	update Carrinho set dvStatus = 'P' where idCliente = @idCliente
	
	
end
